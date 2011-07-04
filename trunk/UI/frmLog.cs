using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuickBooks.Util;
using System.IO;

namespace QuickBooks.UI
{
    public partial class frmLog : Form
    {
        ILogger _logger;
 
        public frmLog(ILogger logger)
        {
            InitializeComponent();

            _logger = logger;
        }

        private void frmLog_Load(object sender, EventArgs e)
        {
            txtLog.Text = _logger.GetLogText();
            txtLog.Select(txtLog.Text.Length - 1, 1);
            txtLog.ScrollToCaret();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            var resp = fbd.ShowDialog();

            if (resp != DialogResult.OK)
                return;

            _logger.Log("Exporting log...");
            string log = _logger.GetLogText();
            string loc = fbd.SelectedPath + "\\" + "QB_InvoiceEntry_Log.txt";
            File.WriteAllText(loc, log);

            MessageBox.Show("File written to: " + loc, "Log",    MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
