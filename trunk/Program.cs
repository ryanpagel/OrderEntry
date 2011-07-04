using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using StructureMap;
using StructureMap.Configuration.DSL;
using QuickBooks.Util;
using QuickBooks.DataAccess;
using System.IO;

namespace QuickBooks.UI
{
    static class Program
    {
        static bool _error = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Settings settings = InitializeSettings();
            InitializeStructureMap(settings);

            ILogger logger = null;
            try
            {
                logger = ObjectFactory.GetInstance<ILogger>();
            }
            catch (Exception ex)
            {
                string path = Environment.CurrentDirectory + "\\Log\\QBLog.txt";
                File.AppendAllText(path, Environment.NewLine + "Error getting an instance of the logger from object factory" + Environment.NewLine  + ex.Message + Environment.NewLine + ex.StackTrace);
                return;
            }
            
            if (_error)
                return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            try
            {
                Application.Run(ObjectFactory.GetInstance<frmMain>());
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error and the program will close. View the log for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                logger.LogException("Error in creating frmMain", ex);
            }
        }



        static void InitializeStructureMap(Settings settings)
        {
            try
            {
                ObjectFactory.Initialize(x =>
                    {
                        x.For<ISettings>()
                            .Singleton()
                            .Use(settings);

                        x.For<ILogger>()
                            .Singleton()
                            .Use<Logger>();

                        x.For<IQBRepository>().Use<QBRepository>();

                        x.For<IFileSystemRepository>().Use<FileSystemRepository>();

                        x.For<IEncryption>().Use<Encryption>();

                    }
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error initializing StructureMap.  Program will close.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _error = true;
                return;
            }
        }

        private static Settings InitializeSettings()
        {
            Settings settings = null;
            string path = "";

            try
            {
                path = Environment.CurrentDirectory + "\\AppData\\app.cfg";
                settings = new Settings();
                settings.Load(path);
            }
            catch (Exception ex)
            {

                MessageBox.Show("An error occurred while reading settings from disk at " + path
                            + Environment.NewLine + Environment.NewLine +
                            "Program will close.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _error = true;
            }

            return settings;
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            if (e.Exception is QuickBooksException)
            {
                QuickBooksException qbe = (QuickBooksException)e.Exception;
                MessageBox.Show(qbe.DisplayMessage, qbe.Caption, qbe.Buttons, qbe.Icon);
            }
            else
            {
                var logger = ObjectFactory.GetInstance<ILogger>();
                logger.LogException("The following exception was caught in the global handler.", e.Exception);
            }
        }


    }
}
