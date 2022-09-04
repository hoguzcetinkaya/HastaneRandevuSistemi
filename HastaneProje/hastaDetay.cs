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
using System.Collections;
namespace HastaneProje
{
    public partial class hastaDetay : Form
    {
        public hastaDetay()
        {
            InitializeComponent();
        }
        sqlbaglanti con = new sqlbaglanti();

        public string hastaTC;
        public string hastaAdSoyad;//2 sütun birleşik
        private void hastaDetay_Load(object sender, EventArgs e)
        {
            comboBox3.Items.Add("09:00"); comboBox3.Items.Add("10:00"); comboBox3.Items.Add("11:00");
            comboBox3.Items.Add("13:00"); comboBox3.Items.Add("14:00"); comboBox3.Items.Add("15:00");
            comboBox3.Items.Add("16:00");


            label6.Text = hastaAdSoyad;
            label7.Text = hastaTC;

            SqlCommand komutBransListele = new SqlCommand("Select bransAd from Tbl_Branslar", con.baglanti());
            SqlDataReader DRBransListele = komutBransListele.ExecuteReader();
            while (DRBransListele.Read())
            {
                comboBox1.Items.Add(DRBransListele[0]);
            }

            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataTable DT = new DataTable();
            SqlDataAdapter DARandevuListele = new SqlDataAdapter("Select a.doktorAd,a.doktorSoyad,b.randevuTarih,b.randevuSaat from Tbl_Doktorlar a,Tbl_Randevular b where a.doktorID=b.randevuDoktorID and randevuDurum="+1, con.baglanti());
            DARandevuListele.Fill(DT);
            dataGridView1.DataSource = DT;




        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            int secilenBransID = comboBox1.SelectedIndex + 1;
            SqlCommand komutBransDoktor = new SqlCommand("Select doktorAd,doktorSoyad,doktorBrans from Tbl_Doktorlar where doktorBransID=" + secilenBransID, con.baglanti());
            SqlDataReader DRBransDoktor = komutBransDoktor.ExecuteReader();
            while (DRBransDoktor.Read())
            {
                comboBox2.Items.Add(DRBransDoktor[0] + " " + DRBransDoktor[1]);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {

            int secilenBransID = comboBox1.SelectedIndex + 1;//bransıd bulundu
            string[] adSoyadParcala;
            adSoyadParcala = comboBox2.Text.Split(' ');
            string doktorAD = adSoyadParcala[0];
            string doktorSoyad = adSoyadParcala[1];
            SqlCommand komutRandevuDoktorID = new SqlCommand("Select * from Tbl_Doktorlar where doktorAd=@doktorAd and doktorSoyad=@doktorSoyad and doktorBransID=@doktorBransID", con.baglanti());
            komutRandevuDoktorID.Parameters.AddWithValue("@doktorAd", doktorAD);
            komutRandevuDoktorID.Parameters.AddWithValue("@doktorSoyad", doktorSoyad);
            komutRandevuDoktorID.Parameters.AddWithValue("@doktorBransID", secilenBransID);
            SqlDataReader DRRandevuDoktorID = komutRandevuDoktorID.ExecuteReader();
            if (DRRandevuDoktorID.Read())
            {
                string doktorID;
                doktorID = DRRandevuDoktorID[0].ToString();
                string doktorBransID = DRRandevuDoktorID[5].ToString();
                SqlCommand komutRandevuHastaID = new SqlCommand("Select * from Tbl_Hastalar where hastaTC=" + label7.Text, con.baglanti());
                SqlDataReader DRRandevuHastaID = komutRandevuHastaID.ExecuteReader();
                if (DRRandevuHastaID.Read())
                {
                    string randevuHastaID = DRRandevuHastaID[0].ToString();
                    SqlCommand komutRandevuOlustur = new SqlCommand("INSERT INTO Tbl_Randevular (randevuTarih,randevuSaat,randevuBransID,randevuDoktorID,randevuDurum,randevuHastaID,randevuHastaSikayet) values (@randevuTarih,@randevuSaat,@randevuBransID,@randevuDoktorID,@randevuDurum,@randevuHastaID,@randevuHastaSikayet)", con.baglanti());
                    komutRandevuOlustur.Parameters.AddWithValue("@randevuTarih", dateTimePicker1.Text);
                    komutRandevuOlustur.Parameters.AddWithValue("@randevuSaat", comboBox3.Text);
                    komutRandevuOlustur.Parameters.AddWithValue("@randevuBransID", secilenBransID);
                    komutRandevuOlustur.Parameters.AddWithValue("@randevuDoktorID", doktorID);
                    komutRandevuOlustur.Parameters.AddWithValue("@randevuDurum", 0);
                    komutRandevuOlustur.Parameters.AddWithValue("@randevuHastaID", randevuHastaID);
                    komutRandevuOlustur.Parameters.AddWithValue("@randevuHastaSikayet", textBox1.Text);
                    komutRandevuOlustur.ExecuteNonQuery();
                    con.baglanti().Close();
                    MessageBox.Show("Randevunuz başarıyla oluşturulmuştur. Lütfen en az 30dk önce hastanede olunuz.");

                    comboBox1.Text = ""; comboBox2.Text = ""; comboBox3.Text = "";
                    textBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("Randevu Oluşturulamadı");
                }
               
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            comboBox3.Text = "";
            comboBox3.Items.Add("09:00"); comboBox3.Items.Add("10:00"); comboBox3.Items.Add("11:00");
            comboBox3.Items.Add("13:00"); comboBox3.Items.Add("14:00"); comboBox3.Items.Add("15:00");
            comboBox3.Items.Add("16:00");

            int secilenBransID = comboBox1.SelectedIndex + 1;//bransıd bulundu
            //doktorıd bulmak için yapılan işlemler
            string[] adSoyadParcala;
            adSoyadParcala = comboBox2.Text.Split(' ');
            string doktorAD = adSoyadParcala[0];
            string doktorSoyad = adSoyadParcala[1];
            SqlCommand komutRandevuDoktorID = new SqlCommand("Select * from Tbl_Doktorlar where doktorAd=@doktorAd and doktorSoyad=@doktorSoyad and doktorBransID=@doktorBransID", con.baglanti());
            komutRandevuDoktorID.Parameters.AddWithValue("@doktorAd", doktorAD);
            komutRandevuDoktorID.Parameters.AddWithValue("@doktorSoyad", doktorSoyad);
            komutRandevuDoktorID.Parameters.AddWithValue("@doktorBransID", secilenBransID);
            SqlDataReader DRRandevuDoktorID = komutRandevuDoktorID.ExecuteReader();
            if (DRRandevuDoktorID.Read())
            {
                ArrayList randevuTarihleri = new ArrayList();//doktora ait randevuların tarihini tutmak için dizi
                ArrayList randevuSaatleri = new ArrayList();
                string doktorID = DRRandevuDoktorID[0].ToString();//doktorıd bulmak için yapılan işlemler bitiş

                // doktora ait randevu bilgilerinin öğrenilmesi için sorgu
                SqlCommand doktorRandevuBilgileri = new SqlCommand("select * from Tbl_Randevular where randevuDoktorID=" + doktorID, con.baglanti());
                SqlDataReader DRDoktorRandevuBilgileri = doktorRandevuBilgileri.ExecuteReader();

                while (DRDoktorRandevuBilgileri.Read())
                {
                    randevuTarihleri.Add(DRDoktorRandevuBilgileri[1]); // doktorun randevularının olduğu tarihi tutan diziye ekleme
                    randevuSaatleri.Add(DRDoktorRandevuBilgileri[2]);


                }
                /* for(int i = 0; i < randevuTarihleri.Count; i++)
                 {
                     MessageBox.Show(randevuTarihleri[i].ToString());
                 }*/
                int counter = 0;
                int g = 0;
                foreach (var tarih in randevuTarihleri) // eklenen tarihleri sırasıyla çalıştırma
                {
                   

                    if (tarih.ToString() == dateTimePicker1.Text && g!=1)
                    {
                        g = 1;
                        SqlCommand komutRandevuSaat = new SqlCommand("select randevuSaat from Tbl_Randevular where randevuTarih=@randevuTarih", con.baglanti());
                        komutRandevuSaat.Parameters.AddWithValue("@randevuTarih", dateTimePicker1.Text);
                        SqlDataReader DRRandevuSaat = komutRandevuSaat.ExecuteReader();
                        while (DRRandevuSaat.Read())
                        {
                            counter++;
                            MessageBox.Show(DRRandevuSaat[0].ToString());
                            comboBox3.Items.Remove(DRRandevuSaat[0]);
                        }
                        
                    }
                }

               
                //SqlCommand komutRandevuSaat = new SqlCommand("select randevuSaat from Tbl_Randevular where randevuTarih=@randevuTarih", con.baglanti());

                //komutRandevuSaat.Parameters.AddWithValue("@randevuTarih", "7 Eylül 2022 Çarşamba");

                //SqlDataReader DRRandevuSaat = komutRandevuSaat.ExecuteReader();


                // 
                // string[] datepp = dateTimePicker1.Value.ToString().Split(' ');

                //if(datepp[0] == "7.09.2022")
                // {
                //     for (int i = 0; i < randevuSaatleri.Count; i++)
                //     {
                //         MessageBox.Show(randevuSaatleri[i].ToString());
                //     }
                // }


                //SqlCommand komutRandevuSaatKaldir = new SqlCommand("select randevuSaat from Tbl_Randevular where randevuTarih="+dateTimePicker1.Text+" ORDER BY randevuSaat",con.baglanti());

                //SqlDataReader DRRandevuSaatKaldir = komutRandevuSaatKaldir.ExecuteReader();
                //while (DRRandevuSaatKaldir.Read())
                //{
                //    MessageBox.Show(DRRandevuSaatKaldir[0].ToString());
                //}
                ////comboBox3.Items.Add("09:00"); comboBox3.Items.Add("10:00"); comboBox3.Items.Add("11:00");
                ////comboBox3.Items.Add("13:00"); comboBox3.Items.Add("14:00"); comboBox3.Items.Add("15:00");
                ////comboBox3.Items.Add("16:00");

            }

            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataTable DT = new DataTable();
            SqlDataAdapter DARandevuListele = new SqlDataAdapter("Select a.doktorAd,a.doktorSoyad,b.randevuTarih,b.randevuSaat from Tbl_Doktorlar a,Tbl_Randevular b where a.doktorID=b.randevuDoktorID and randevuDurum="+1, con.baglanti());
            DARandevuListele.Fill(DT);
            dataGridView1.DataSource = DT;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 anaEkran = new Form1();
            anaEkran.Show();
            this.Hide();
        }
    }
}
    

