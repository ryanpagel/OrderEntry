﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace QuickBooks.Util
{
    public class Logger : QuickBooks.Util.ILogger 
    {
        string _fullPath = string.Empty;
        int _maxLogSizeBytes = 0;

        public Logger(ISettings settings)
        {
            _fullPath = settings.LogFilePath;
            _maxLogSizeBytes = settings.MaxLogSizeBytes;
            settings.SettingsReconfigured += new Action<Settings>(settings_SettingsReconfigured);
        }

        void settings_SettingsReconfigured(Settings obj)
        {
            _fullPath = obj.LogFilePath;
            _maxLogSizeBytes = obj.MaxLogSizeBytes;
        }

        string ExceptionToStringRecursive(Exception ex, string sException)
        {
            sException += Environment.NewLine + Environment.NewLine +
                "Error Message: " + ex.Message + Environment.NewLine +
                "Stack Trace: " + ex.StackTrace + Environment.NewLine +
                "Source: " + ex.Source + Environment.NewLine +
                "Target Site: " + ex.TargetSite;

            if (ex.InnerException != null)
                sException+= ExceptionToStringRecursive(ex.InnerException, sException);

            return sException;
        }


        public void LogException(string msg, Exception ex)
        {
            string finalLogString = (msg + ExceptionToStringRecursive(ex, ""));

            this.Log(finalLogString);
        }

        public void LogException(Exception ex)
        {
            this.Log(ex.Message);
        }

        public void Log(string text)
        {
            string logText = string.Empty;

            var bytes = File.ReadAllBytes(_fullPath);

            int byteCount = bytes.GetUpperBound(0);


            if (byteCount > _maxLogSizeBytes)
            {
                int startSaveIndex = (int)(byteCount * .75);
                Encoding enc = Encoding.UTF8;
                string savedPortionOfLog = enc.GetString(bytes, startSaveIndex, byteCount - startSaveIndex);

                File.WriteAllText(_fullPath, "--------------" + Environment.NewLine
                    + savedPortionOfLog + Environment.NewLine + Environment.NewLine 
                    + DateTime.Now.ToString() + " - " + "Rolling log file was cleared");
                File.AppendAllText(_fullPath, Environment.NewLine + Environment.NewLine + text);
            }
            else
            {
                File.AppendAllText(_fullPath, Environment.NewLine +
                                                Environment.NewLine +
                                                "---------------" + Environment.NewLine +
                                                DateTime.Now.ToString() + Environment.NewLine +
                                                Environment.NewLine +
                                                text);
            }
        }
        
        public string GetLogText()
        {
            return File.ReadAllText(_fullPath);
        }
        

        

    }
}
