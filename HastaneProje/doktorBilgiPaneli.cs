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
    public partial class doktorBilgiPaneli : Form
    {
        public doktorBilgiPaneli()
        {
            InitializeComponent();
        }
        sqlbaglanti con = new sqlbaglanti();
        public string sekreterID;
        private void doktorBilgiPaneli_Load(object sender, EventArgs e)
        {
            SqlCommand komutBransListele = new SqlCommand("Select bransAd from Tbl_Branslar", con.baglanti());
            SqlDataReader DRBransListele = komutBransListele.ExecuteReader();
            while (DRBransListele.Read())
            {
                comboBox1.Items.Add(DRBransListele[0]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            doktorGiris doktorGirisForm = new doktorGiris();
            SqlCommand komutDoktorKontrol = new SqlCommand("Select * from Tbl_Doktorlar where doktorTC=" + maskedTextBox1.Text, con.baglanti());
            SqlDataReader DRDoktorKontrol = komutDoktorKontrol.ExecuteReader();
            if (DRDoktorKontrol.Read())//TC VAR İSE MESAJ VER YOKSA ELSE ÇALIŞTIR
            {
                MessageBox.Show(DRDoktorKontrol[1].ToString() + " Zaten kayıtlısınız lütfen giriş yapınız.");
                doktorGirisForm.Show();
                
            }
            else
            { // DOKTOR KAYIT
                SqlCommand komutDoktorEkle = new SqlCommand("INSERT INTO Tbl_Doktorlar (doktorAd,doktorSoyad,doktorTC,doktorBrans,doktorBransID,doktorSifre,sekreterID) values(@doktorAd,@doktorSoyad,@doktorTC,@doktorBrans,@doktorBransID,@doktorSifre,@sekreterID)", con.baglanti());
                komutDoktorEkle.Parameters.AddWithValue("@doktorAd", textBox1.Text);
                komutDoktorEkle.Parameters.AddWithValue("@doktorSoyad", textBox2.Text);
                komutDoktorEkle.Parameters.AddWithValue("@doktorTC", maskedTextBox1.Text);
                komutDoktorEkle.Parameters.AddWithValue("@doktorBrans", comboBox1.Text);
                int bransID = comboBox1.SelectedIndex + 1;
                komutDoktorEkle.Parameters.AddWithValue("@doktorBransID", bransID);
                komutDoktorEkle.Parameters.AddWithValue("@doktorSifre", textBox3.Text);
                komutDoktorEkle.Parameters.AddWithValue("@sekreterID", sekreterID);


                komutDoktorEkle.ExecuteNonQuery();
                con.baglanti().Close();

                MessageBox.Show("Kayıt işleminiz başarıyla tamamlanmıştır Lütfen giriş yapınız.");



            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sekreterDetay sekreterDetay = new sekreterDetay();
            sekreterDetay.Show();
            this.Hide();
        }
    }
}
