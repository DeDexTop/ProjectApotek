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
    public partial class Form_Resep_Obat : Form
    {
        Function func = new Function();
        int id, Id_Obat;
        public Form_Resep_Obat()
        {
            InitializeComponent();
        }
        
        new void Show()
        {
            dgv_Reserp.DataSource = func.ShowData("SELECT * FROM Tbl_Resep");
        }
        void Clear()
        {
            txtNoResep.Text = "";
            dtp_TglResep.Text = "1/1/2020";
            txtNamaPasien.Text = "";
            txtNamaDokter.Text = "";
            cbx_NamaObat.Text = "";
            txtQuantity.Text = "";
            txtCari.Text = "";
        }

        private void Form_Resep_Obat_Load(object sender, EventArgs e)
        {
            Show();
            DataRowCollection col = func.GetData("SELECT Id_Obat, Nama_Obat FROM Tbl_Obat WHERE Jumlah != 0");
            foreach(DataRow row in col)
            {
                cbx_NamaObat.Items.Add(row["Nama_Obat"].ToString());
            }
            btnEdit.Visible = false;
            btnHapus.Visible = false;
        }

        private void dgv_Reserp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToInt32(dgv_Reserp.Rows[e.RowIndex].Cells[0].Value);
            txtNoResep.Text = dgv_Reserp.Rows[e.RowIndex].Cells[1].Value.ToString();
            dtp_TglResep.Text = dgv_Reserp.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtNamaPasien.Text = dgv_Reserp.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtNamaDokter.Text = dgv_Reserp.Rows[e.RowIndex].Cells[4].Value.ToString();
            cbx_NamaObat.Text = dgv_Reserp.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtQuantity.Text = dgv_Reserp.Rows[e.RowIndex].Cells[6].Value.ToString();

            btnEdit.Visible = true;
            btnHapus.Visible = true;
            btnTambah.Visible = false;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if(txtNoResep.Text == "" || txtNamaPasien.Text == "" || txtNamaDokter.Text == "" || cbx_NamaObat.Text == "" || txtQuantity.Text == "")
            {
                MessageBox.Show("Semua kolom harus di isi!");
            }
            else
            {
                func.Command("INSERT INTO Tbl_Resep ([No_Resep], [Tgl_Resep], [Nama_Dokter], [Nama_Pasien], [Nama_ObatDibeli], [Jumlah_ObatDibeli], [Id_Obat]) VALUES ('" + txtNoResep.Text + "', '" + dtp_TglResep.Text + "', '" + txtNamaDokter.Text + "', '" + txtNamaPasien.Text + "', '" + cbx_NamaObat.Text + "', '" + txtQuantity.Text + "', '" + Id_Obat + "')");

                Clear();
                Show();

                MessageBox.Show("Data Berhasil Ditambakan");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtNoResep.Text == "" || txtNamaPasien.Text == "" || txtNamaDokter.Text == "" || cbx_NamaObat.Text == "" || txtQuantity.Text == "")
            {
                MessageBox.Show("Semua kolom harus di isi!");
            }
            else
            {
                func.Command("UPDATE Tbl_Resep SET No_Resep = '" + txtNoResep.Text + "', Tgl_Resep = '" + dtp_TglResep.Text + "', Nama_Dokter = '" + txtNamaDokter.Text + "', Nama_Pasien = '" + txtNamaPasien.Text + "', Nama_ObatDibeli = '" + cbx_NamaObat.Text + "', Jumlah_ObatDibeli = '" + txtQuantity.Text + "' WHERE Id_Resep = '" + id + "'");

                Clear();
                Show();

                MessageBox.Show("Data Berhasil Diubah");

                btnEdit.Visible = false;
                btnHapus.Visible = false;
                btnTambah.Visible = true;
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            func.Command("DELETE FROM Tbl_Resep WHERE Id_Resep = '" + id + "'");

            Clear();
            Show();

            MessageBox.Show("Data Berhasil Dihapus");

            btnEdit.Visible = false;
            btnHapus.Visible = false;
            btnTambah.Visible = true;
        }

        private void cbx_NamaObat_TextChanged(object sender, EventArgs e)
        {
            DataRowCollection col = func.GetData("SELECT Id_Obat, Nama_Obat FROM Tbl_Obat WHERE Nama_Obat = '" + cbx_NamaObat.Text + "'");
            foreach (DataRow row in col)
            {
                Id_Obat = Convert.ToInt32(row["Id_Obat"]);
            }
        }

        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            dgv_Reserp.DataSource = func.ShowData("SELECT * FROM Tbl_Resep WHERE Nama_Pasien OR Nama_Dokter LIKE '" + txtCari.Text + "%'");
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            new Form_Login().Show();
            this.Hide();
        }
    }
}
