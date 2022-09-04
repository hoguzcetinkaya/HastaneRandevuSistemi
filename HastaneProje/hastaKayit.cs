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
    public partial class hastaKayit : Form
    {
        public hastaKayit()
        {
            InitializeComponent();
        }

        sqlbaglanti con = new sqlbaglanti();
        private void hastaKayit_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            hastaGiris hastaGirisForm = new hastaGiris();
            // KAYIT VAR MI KONTROL ETME
            SqlCommand komutHastaKontrol = new SqlCommand("Select hastaAd from Tbl_Hastalar where hastaTC=" + maskedTextBox1.Text, con.baglanti());
            SqlDataReader DRHastaKontrol = komutHastaKontrol.ExecuteReader();
            if(DRHastaKontrol.Read())//TC VAR İSE MESAJ VER YOKSA ELSE ÇALIŞTIR
            {
                MessageBox.Show(DRHastaKontrol[0].ToString()+" Zaten kayıtlısınız lütfen giriş yapınız.");
                hastaGirisForm.Show();
                this.Hide();
            }
            else
            {
                //HASTA EKLEME
                SqlCommand komutHastaKayit = new SqlCommand("INSERT INTO Tbl_Hastalar (hastaAd,hastaSoyad,hastaTC,hastaTEL,hastaSifre,hastaCinsiyet) values(@hastaAd,@hastaSoyad,@hastaTC,@hastaTEL,@hastaSifre,@hastaCinsiyet)",con.baglanti());
                komutHastaKayit.Parameters.AddWithValue("@hastaAd", textBox1.Text);
                komutHastaKayit.Parameters.AddWithValue("@hastaSoyad", textBox2.Text);
                komutHastaKayit.Parameters.AddWithValue("@hastaTC", maskedTextBox1.Text);
                komutHastaKayit.Parameters.AddWithValue("@hastaTEL", maskedTextBox2.Text);
                komutHastaKayit.Parameters.AddWithValue("@hastaSifre", textBox3.Text);
                komutHastaKayit.Parameters.AddWithValue("@hastaCinsiyet", comboBox1.Text);
                komutHastaKayit.ExecuteNonQuery();
                con.baglanti().Close();

                MessageBox.Show("Kayıt işleminiz başarıyla tamamlanmıştır Lütfen giriş yapınız.");
                
                hastaGirisForm.Show();
                this.Hide();
            }




        }

  
    }
}
