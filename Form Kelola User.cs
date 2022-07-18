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
    public partial class Form_Kelola_User : Form
    {
        Function func = new Function();
        string id;
        public Form_Kelola_User()
        {
            InitializeComponent();
        }

        new void Show()
        {
            dgv_User.DataSource = func.ShowData("SELECT * FROM Tbl_User2");
        }
        void Clear()
        {
            cbx_TipeUser.Text = "";
            txtAlamat.Text = "";
            txtCari.Text = "";
            txtNama.Text = "";
            txtPassword.Text = "";
            txtUserName.Text = "";
            txtTelpon.Text = "";
        }

        private void Form_Kelola_User_Load(object sender, EventArgs e)
        {
            Show();
        }
        private void dgv_User_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = dgv_User.Rows[e.RowIndex].Cells[0].Value.ToString();
            cbx_TipeUser.Text = dgv_User.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtNama.Text = dgv_User.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtAlamat.Text = dgv_User.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtTelpon.Text = dgv_User.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtUserName.Text = dgv_User.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtPassword.Text = dgv_User.Rows[e.RowIndex].Cells[6].Value.ToString();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if(cbx_TipeUser.Text == "" || txtAlamat.Text == "" || txtNama.Text == "" || txtPassword.Text == "" || txtTelpon.Text == "" || txtUserName.Text == "")
            {
                MessageBox.Show("Semua kolom data harus di isi!");
            }
            else
            {
                func.Command("INSERT INTO Tbl_User2 ([Tipe_User], [Nama_User], [Alamat], [Telpon], [Username], [Password]) VALUES ('" + cbx_TipeUser.Text + "', '" + txtNama.Text + "', '" + txtAlamat.Text + "', '" + txtTelpon.Text + "', '" + txtUserName.Text + "', '" + txtPassword.Text + "')");

                Clear();
                Show();

                MessageBox.Show("Data Berhasil Di Tambahkan");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (cbx_TipeUser.Text == "" || txtAlamat.Text == "" || txtNama.Text == "" || txtPassword.Text == "" || txtTelpon.Text == "" || txtUserName.Text == "")
            {
                MessageBox.Show("Semua kolom data harus di isi!");
            }
            else
            {
                func.Command("UPDATE Tbl_User2 SET Tipe_User = '" + cbx_TipeUser.Text + "', Nama_User = '" + txtNama.Text + "', Alamat = '" + txtAlamat.Text + "', Telpon = '" + txtTelpon.Text + "', Username = '" + txtUserName.Text + "', Password = '" + txtPassword.Text + "' WHERE Id_User = '" + id + "'");

                Clear();
                Show();

                MessageBox.Show("Data Berhasil Di Ubah");
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            func.Command("DELETE FROM Tbl_User2 WHERE User_Id = '" + id + "'");

            Clear();
            Show();

            MessageBox.Show("Data Berhasil Di Hapus");
        }

        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            dgv_User.DataSource = func.ShowData("SELECT * FROM Tbl_User WHERE Nama_User LIKE '" + txtCari.Text + "%'");
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
