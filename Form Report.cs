using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lks_Desktop
{
    public partial class Form_Report : Form
    {
        Function func = new Function();
        public Form_Report()
        {
            InitializeComponent();
        }

        void Data()
        {
            dgv_Report.DataSource = func.ShowData("SELECT Tgl_Transaksi, Total_Bayar FROM Tbl_Transaksi WHERE Tgl_Transaksi BETWEEN '" + dateTimePicker1.Text + "' AND '" + dateTimePicker2.Text + "'");
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            dgv_Report.Rows.Clear();
            Data();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            
        }

        private void btnKelolaUser_Click(object sender, EventArgs e)
        {
            new Form_Kelola_User().Show();
            this.Hide();
        }

        private void btnKelolaObat_Click(object sender, EventArgs e)
        {
            new Form_Kelola_Obat().Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            new Form_Login().Show();
            this.Hide();
        }
    }
}
