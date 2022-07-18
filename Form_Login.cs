using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lks_Desktop
{
    public partial class Form_Login : Form
    {
        Function func = new Function();
        public static string url = @"Data Source=localhost;Initial Catalog=apotek;Integrated Security=True";
        SqlConnection con = new SqlConnection(url);
        int id;
        public Form_Login()
        {
            InitializeComponent();
        }

        void Login()
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Id_User, Tipe_User, Nama_User FROM Tbl_User2 WHERE Username = '" + txtUsername.Text + "' AND Password = '" + txtPassword.Text + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        ClassPegawai.NamaPegawai = dr["Nama_User"].ToString();
                        ClassPegawai.IdPegawai = dr["Id_User"].ToString();
                        id = Convert.ToInt32(dr["Id_User"]);

                        func.Command("INSERT INTO Tbl_Log ([Aktifitas], [Waktu], [Id_User]) VALUES ('Login', getDate(), '" + id + "')");

                        if (dr["Tipe_User"].ToString() == "Admin")
                        {
                            new Admin_Form().Show();
                            this.Hide();
                        }
                        else if(dr["Tipe_User"].ToString() == "Apoteker")
                        {
                            new Form_Resep_Obat().Show();
                            this.Hide();
                        }
                        else if(dr["Tipe_User"].ToString() == "Kasir")
                        {
                            new Form_Kasir().Show();
                            this.Hide();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Maaf Data Tidak Valid");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text == ""  || txtPassword.Text == "")
            {
                MessageBox.Show("Kolom username dan pasword harus di isi!");
            }
            else
            {
                Login();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
        }
    }
}
