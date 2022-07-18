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
    public partial class Form_Kasir : Form
    {
        Function func = new Function();
        int harga;
        string Id_Obat, Id_Resep;
         
        public Form_Kasir()
        {
            InitializeComponent();
            labelKasir.Text = ClassPegawai.NamaPegawai;
            labelDate.Text = DateTime.Now.ToShortDateString();
            labelTime.Text = DateTime.Now.ToLongTimeString();
        }
        
        new void Show()
        {
            DataRowCollection row = func.GetData("SELECT Tbl_Resep.Id_Resep, Tbl_Resep.No_Resep, Tbl_Resep.Tgl_Resep, Tbl_Resep.Nama_Dokter, Tbl_Resep.Nama_Pasien, Tbl_Obat.Nama_Obat, Tbl_Obat.Harga, Tbl_Resep.Jumlah_ObatDibeli FROM Tbl_Resep JOIN Tbl_Obat ON Tbl_Resep.Id_Obat = Tbl_Obat.Id_Obat");
            foreach (DataRow dr in row)
            {
                cbx_NamaObat.Items.Add(dr["Nama_Obat"].ToString());
                cbx_NoResep.Items.Add(dr["No_Resep"].ToString());

                Id_Resep = dr["Id_Resep"].ToString();
            }
            btnSave.Enabled = false;
        }
        void Label()
        {
            int total = 0;
            for(int i = 0; i < dgv_Reserp.Rows.Count; i++)
            {
                total += Convert.ToInt32(dgv_Reserp.Rows[i].Cells[8].Value);
                LabelTotal.Text = total.ToString();
                harga = total;
            }
        }
        void Clear()
        {
            txtNamaPasien.Text = "";
            txtNamaDokter.Text = "";
            txtHarga.Text = "";
            txtQuantity.Text = "";
            cbx_NamaObat.Text = "";
            cbx_NoResep.Text = "";
            cbx_Resep.Text = "Resep";
            dtp_TglResep.Text = "1/1/2020";
        }

        private void Form_Kasir_Load(object sender, EventArgs e)
        {
            Show();
        }

        private void cbx_Resep_TextChanged(object sender, EventArgs e)
        {
            if (cbx_Resep.Text == "Non Resep")
            {
                cbx_NoResep.Enabled = false;
                cbx_NoResep.Text = "-";
                dtp_TglResep.Enabled = false;
                txtNamaPasien.Enabled = false;
                txtNamaPasien.Text = "-";
                txtNamaDokter.Enabled = false;
                txtNamaDokter.Text = "-";
                txtHarga.Enabled = false;
            }
        }

        private void cbx_NoResep_TextChanged(object sender, EventArgs e)
        {
            DataRowCollection col = func.GetData("SELECT Tbl_Resep.No_Resep, Tbl_Resep.Tgl_Resep, Tbl_Resep.Nama_Dokter, Tbl_Resep.Nama_Pasien, Tbl_Obat.Nama_Obat, Tbl_Obat.Harga, Tbl_Resep.Jumlah_ObatDibeli FROM Tbl_Resep JOIN Tbl_Obat ON Tbl_Resep.Id_Obat = Tbl_Obat.Id_Obat WHERE No_Resep = '" + cbx_NoResep.Text + "'");
            foreach (DataRow dr in col)
            {
                dtp_TglResep.Text = dr["Tgl_Resep"].ToString();
                txtNamaPasien.Text = dr["Nama_Pasien"].ToString();
                txtNamaDokter.Text = dr["Nama_Dokter"].ToString();
                cbx_NamaObat.Text = dr["Nama_Obat"].ToString();
                txtHarga.Text = dr["Harga"].ToString();
                txtQuantity.Text = dr["Jumlah_ObatDIbeli"].ToString();
            }
        }
        private void cbx_NamaObat_TextChanged(object sender, EventArgs e)
        {
            DataRowCollection col = func.GetData("SELECT Id_Obat, Harga FROM Tbl_Obat WHERE Nama_Obat = '" + cbx_NamaObat.Text + "'");
            foreach(DataRow dr in col)
            {
                Id_Obat = dr["Id_Obat"].ToString();
                txtHarga.Text = dr["Harga"].ToString();
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            dgv_Reserp.Rows.Add(cbx_Resep.Text, cbx_NoResep.Text, dtp_TglResep.Text, txtNamaPasien.Text, txtNamaDokter.Text, cbx_NamaObat.Text, txtHarga.Text, txtQuantity.Text, Convert.ToInt32(txtHarga.Text) * Convert.ToInt32(txtQuantity.Text));
            Label();
            Clear();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            dgv_Reserp.Rows.Clear();
            Label();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Convert.ToInt32(txtBayar.Text) < harga)
            {
                MessageBox.Show("Uang yang di masukan kurang!");
            }
            else
            {
                MessageBox.Show("Transaksi berhasil");

                int kembalian = Convert.ToInt32(txtBayar.Text) - harga;
                labelKembalian.Text = kembalian.ToString();

                btnSave.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            for(int i=0;i<dgv_Reserp.Rows.Count-1;i++)
            {
                func.Command("INSERT INTO Tbl_Transaksi ([No_Transaksi], [Tgl_Transaksi], [Total_Bayar], [Id_User], [Id_Obat], [Id_Resep]) VALUES ('" + dtp_NoTransaksi.Text + "', getDate(), '" + Convert.ToInt32(LabelTotal.Text) + "', '" + ClassPegawai.IdPegawai + "', '" + Id_Obat + "', '" + Id_Resep + "')");
            }
            dgv_Reserp.Rows.Clear();
            Label();

            MessageBox.Show("Data Tersimpan");
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            new Form_Login().Show();
            this.Hide();
        }
    }
}
