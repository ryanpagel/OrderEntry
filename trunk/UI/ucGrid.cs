using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuickBooks.BusObj;
using QuickBooks.DataAccess;

namespace QuickBooks.UI
{
    public partial class ucGrid : UserControl
    {
        public ucGrid()
        {
            InitializeComponent();
            gvMain.AutoGenerateColumns = false;
        }

        public PendingOrderSaveLocation SaveLocation { get; set; }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                gvMain.Rows[e.RowIndex].Selected = true;
                cmsGrid.Show(Cursor.Position);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(gvMain.SelectedRows.Count != 1)
                return;

            string key = gvMain.SelectedRows[0].Cells["FileKey"].Value.ToString();
            string name = gvMain.SelectedRows[0].Cells["FullName"].Value.ToString();
            if (DeleteFileKey != null)
                DeleteFileKey(key,name);
        }

        public event Action<string, string> DeleteFileKey;

        public void SetDatasource(List<PendingOrderFile> pofList)
        {
            gvMain.DataSource = new SortableBindingList<PendingOrderFile>(pofList);
        }

        private void gvMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(gvMain.SelectedRows.Count !=1)
                return;

            string key = gvMain.SelectedRows[0].Cells["FileKey"].Value.ToString();
            if (OpenOrderByKey != null)
                OpenOrderByKey(key);
        }

        public event Action<string> OpenOrderByKey;

        public int Rows
        {
            get { return gvMain.Rows.Count; }
        }

        private void ucGrid_Load(object sender, EventArgs e)
        {
            if (this.SaveLocation == PendingOrderSaveLocation.LeftPanel)
                lblTitle.Text = "Contacts";
            else if (this.SaveLocation == PendingOrderSaveLocation.RightPanel)
                lblTitle.Text = "Pending Order";
            else if (this.SaveLocation == PendingOrderSaveLocation.Swatch)
                lblTitle.Text = "Swatch Order";
        }

    }
}
