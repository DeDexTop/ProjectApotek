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
    public partial class Form_Kelola_Obat : Form
    {
        Function func = new Function();
        int id;
        public Form_Kelola_Obat()
        {
            InitializeComponent();
        }
        new void Show()
        {
            dgv_Obat.DataSource = func.ShowData("SELECT * FROM Tbl_Obat");
        }
        void Clear()
        {
            txtKodeObat.Text = "";
            txtNamaObat.Text = "";
            DatePicker.Text = "1 / 1 / 2022";
            txtJumlah.Text = "";
            txtHarga.Text = "";
            txtCari.Text = "";
        }

        private void Form_Kelola_Obat_Load(object sender, EventArgs e)
        {
            Show();
        }

        private void dgv_Obat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToInt32(dgv_Obat.Rows[e.RowIndex].Cells[1].Value);
            txtKodeObat.Text = dgv_Obat.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtNamaObat.Text = dgv_Obat.Rows[e.RowIndex].Cells[2].Value.ToString();
            DatePicker.Text = dgv_Obat.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtJumlah.Text = dgv_Obat.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtHarga.Text = dgv_Obat.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if(txtKodeObat.Text == "" || txtNamaObat.Text == "" || txtJumlah.Text == ""  || txtHarga.Text == "")
            {
                MessageBox.Show("Semua kolom data harus di isi!");
            }
            else
            {
                func.Command("INSERT INTO Tbl_Obat ([Kode_Obat], [Nama_Obat], [Expired_Date], [Jumlah], [Harga]) VALUES ('" + txtKodeObat.Text + "', '" + txtNamaObat.Text + "', '" + DatePicker.Text + "', '" + txtJumlah.Text + "', '" + txtHarga.Text + "')");

                Clear();
                Show();

                MessageBox.Show("Data berhasil ditambahkan");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtKodeObat.Text == "" || txtNamaObat.Text == "" || txtJumlah.Text == "" || txtHarga.Text == "")
            {
                MessageBox.Show("Semua kolom data harus di isi!");
            }
            else
            {
                func.Command("UPDATE Tbl_Obat SET Kode_Obat = '" + txtKodeObat.Text + "', Nama_Obat = '" + txtNamaObat.Text + "', Expired_Date = '" + DatePicker.Text + "', Jumlah = '" + txtJumlah.Text + "', Harga = '" + txtHarga.Text + "' WHERE Id_Obat = '" + id + "'");

                Clear();
                Show();

                MessageBox.Show("Data berhasil diubah");
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            func.Command("DELETE FROM Tbl_Obat WHERE Id_Obat = '" + id + "'");

            Clear();
            Show();

            MessageBox.Show("Data berhasil dihapus");
        }

        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            dgv_Obat.DataSource = func.ShowData("SELECT * FROM Tbl_Obat WHERE Nama_Obat LIKE '" + txtCari.Text + "%'");
        }

        private void btnKelolaUser_Click(object sender, EventArgs e)
        {
            new Form_Kelola_User().Show();
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
