using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lks_Desktop
{
    public partial class Admin_Form : Form
    {
        Function func = new Function();
        public Admin_Form()
        {
            InitializeComponent();
        }
        new void Show()
        {
            dgv_Log.DataSource = func.ShowData("SELECT Tbl_Log.Id_Log, Tbl_User2.Username, Tbl_Log.Waktu, Tbl_Log.Aktifitas FROM Tbl_Log JOIN Tbl_User2 ON Tbl_Log.Id_User = Tbl_User2.Id_User");
        }

        private void Admin_Form_Load(object sender, EventArgs e)
        {
            Show();
            Refresh();
            Show();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            
            dgv_Log.DataSource = func.ShowData("SELECT Tbl_Log.Id_Log, Tbl_User2.Username, Tbl_Log.Waktu, Tbl_Log.Aktifitas FROM Tbl_Log JOIN Tbl_User2 ON Tbl_Log.Id_User = Tbl_User2.Id_User WHERE Tbl_Log.Waktu = '" + dateTimePicker1.Text + "'");
            
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

        private void btnLaporan_Click(object sender, EventArgs e)
        {
            new Form_Report().Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            new Form_Login().Show();
            this.Hide();
        }
    }
}
