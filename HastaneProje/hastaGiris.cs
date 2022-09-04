using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HastaneProje
{
    public partial class hastaGiris : Form
    {
        public hastaGiris()
        {
            InitializeComponent();
        }
        sqlbaglanti con = new sqlbaglanti();
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            hastaKayit hastaKayitForm = new hastaKayit();
            hastaKayitForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komutHastaGiris = new SqlCommand("Select * from Tbl_Hastalar where hastaTC=@hastaTC and hastaSifre=@hastaSifre",con.baglanti());
            komutHastaGiris.Parameters.AddWithValue("@hastaTC", maskedTextBox1.Text);
            komutHastaGiris.Parameters.AddWithValue("@hastaSifre", maskedTextBox2.Text);
            SqlDataReader DRHastaGiris = komutHastaGiris.ExecuteReader();
            if(DRHastaGiris.Read())
            {
                MessageBox.Show("Giriş İşlemi Başarılı");
                hastaDetay hastaDetayForm = new hastaDetay();
                hastaDetayForm.hastaTC = maskedTextBox1.Text;
                hastaDetayForm.hastaAdSoyad = DRHastaGiris[1] + " " + DRHastaGiris[2];
                hastaDetayForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Lütfen bilgilerinizi doğru giriniz.");
            }
        }


        private void hastaGiris_Load(object sender, EventArgs e)
        {

        }
    }
}
