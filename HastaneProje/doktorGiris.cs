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
    public partial class doktorGiris : Form
    {
        public doktorGiris()
        {
            InitializeComponent();
        }
        sqlbaglanti con = new sqlbaglanti();
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komutDoktorGiris = new SqlCommand("Select * from Tbl_Doktorlar where doktorTC=@doktorTC and doktorSifre=@doktorSifre", con.baglanti());
            komutDoktorGiris.Parameters.AddWithValue("@doktorTC", maskedTextBox1.Text);
            komutDoktorGiris.Parameters.AddWithValue("@doktorSifre", maskedTextBox2.Text);
            SqlDataReader DRDoktorGiris = komutDoktorGiris.ExecuteReader();
            if(DRDoktorGiris.Read())
            {
                MessageBox.Show("Giriş İşlemi Başarılı");
                doktorDetay doktorDetayForm = new doktorDetay();
                doktorDetayForm.doktorTC = maskedTextBox1.Text;
                doktorDetayForm.doktorID = DRDoktorGiris[0].ToString();
                doktorDetayForm.doktorAd = DRDoktorGiris[1].ToString();
                doktorDetayForm.doktorSoyad = DRDoktorGiris[2].ToString();
                doktorDetayForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Lütfen bilgilerinizi doğru giriniz.");
            }
        }
    }
}
