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
    public partial class sekreterDetay : Form
    {
        public sekreterDetay()
        {
            InitializeComponent();
        }

        sqlbaglanti con = new sqlbaglanti();

        public string sekreterTC;

        duyuruListesi duyuruListesiForm = new duyuruListesi();
        doktorBilgiPaneli doktorBilgiForm = new doktorBilgiPaneli();
        string sekreterID;
        private void sekreterDetay_Load(object sender, EventArgs e)
        {
            // SEKRETER TC
            label4.Text = sekreterTC;

            // SEKRETER ADSOYAD
            SqlCommand komutSekreterAdSoyad = new SqlCommand("Select sekreterID,sekreterAdSoyad from Tbl_Sekreterler where sekreterTC="+label4.Text,con.baglanti());
            SqlDataReader DRSekreterAdSoyad = komutSekreterAdSoyad.ExecuteReader();
            if(DRSekreterAdSoyad.Read())
            {
                label3.Text = DRSekreterAdSoyad[1].ToString();
                duyuruListesiForm.sekreterAdSoyad = label3.Text;
                doktorBilgiForm.sekreterID = DRSekreterAdSoyad[0].ToString();
                sekreterID = DRSekreterAdSoyad[0].ToString();
                duyuruListesiForm.sekreterID = DRSekreterAdSoyad[0].ToString();
            }



            //Doktorları Datagridde gösterme
            this.dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataTable DT = new DataTable();
            SqlDataAdapter DADoktorDG = new SqlDataAdapter("Select doktorAd,doktorSoyad,doktorBrans from Tbl_Doktorlar where sekreterID="+sekreterID,con.baglanti());
            DADoktorDG.Fill(DT);
            dataGridView2.DataSource = DT;

            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataTable DT2 = new DataTable();
            SqlDataAdapter DARandevuListele = new SqlDataAdapter("Select b.randevuID,a.doktorAd,a.doktorSoyad,b.randevuTarih,b.randevuSaat,b.randevuDurum from Tbl_Doktorlar a,Tbl_Randevular b where a.doktorID=b.randevuDoktorID and randevuDurum=" + 0, con.baglanti());
            DARandevuListele.Fill(DT2);
            dataGridView1.DataSource = DT2;


            // duyurular
            SqlCommand komutBransListele = new SqlCommand("Select bransAd from Tbl_Branslar", con.baglanti());
            SqlDataReader DRBransListele = komutBransListele.ExecuteReader();
            while (DRBransListele.Read())
            {
                comboBox1.Items.Add(DRBransListele[0]);
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            
            doktorBilgiForm.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;//seçilenin 0.indexsini al yani ip adres
            int siralama = secilen + 1;
            //datagridview'deki verileri çekme işlemi
            label8.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult onayla = new DialogResult();
            onayla = MessageBox.Show("Onaylamak istiyor musunuz ?","ONAYLA",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if(onayla==DialogResult.Yes)
            {
                SqlCommand randevuOnayla = new SqlCommand("UPDATE Tbl_Randevular set randevuDurum=@randevuDurum where randevuID=" + label8.Text, con.baglanti()) ;
                randevuOnayla.Parameters.AddWithValue("@randevuDurum","True");
                randevuOnayla.ExecuteNonQuery();
                con.baglanti().Close();

                this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                DataTable DT2 = new DataTable();
                SqlDataAdapter DARandevuListele = new SqlDataAdapter("Select b.randevuID,a.doktorAd,a.doktorSoyad,b.randevuTarih,b.randevuSaat,b.randevuDurum from Tbl_Doktorlar a,Tbl_Randevular b where a.doktorID=b.randevuDoktorID and randevuDurum=" + 0, con.baglanti());
                DARandevuListele.Fill(DT2);
                dataGridView1.DataSource = DT2;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult iptalEt = new DialogResult();
            iptalEt = MessageBox.Show("Onaylamak istiyor musunuz ?", "ONAYLA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (iptalEt == DialogResult.Yes)
            {
                SqlCommand randevuOnayla = new SqlCommand("DELETE from Tbl_Randevular where randevuID=" + label8.Text, con.baglanti());
                randevuOnayla.ExecuteNonQuery();
                con.baglanti().Close();

                this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                DataTable DT2 = new DataTable();
                SqlDataAdapter DARandevuListele = new SqlDataAdapter("Select b.randevuID,a.doktorAd,a.doktorSoyad,b.randevuTarih,b.randevuSaat,b.randevuDurum from Tbl_Doktorlar a,Tbl_Randevular b where a.doktorID=b.randevuDoktorID and randevuDurum=" + 0, con.baglanti());
                DARandevuListele.Fill(DT2);
                dataGridView1.DataSource = DT2;

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 anaEkran = new Form1();
            anaEkran.Show();
            this.Hide();
        }

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip dt_menu = new System.Windows.Forms.ContextMenuStrip();
                int position = dataGridView2.HitTest(e.X, e.Y).RowIndex;

                if (position >= 0)
                {
                    dt_menu.Items.Add("Yenile").Name = "Yenile";

                }
                dt_menu.Show(dataGridView2, new Point(e.X, e.Y));

                dt_menu.ItemClicked += Dt_menu_ItemClicked;
            }
        }

        private void Dt_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // throw new NotImplementedException();

            

            if(e.ClickedItem.Name.ToString()=="Yenile")
            {
                this.dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                DataTable DT = new DataTable();
                SqlDataAdapter DADoktorDG = new SqlDataAdapter("Select doktorAd,doktorSoyad,doktorBrans from Tbl_Doktorlar", con.baglanti());
                DADoktorDG.Fill(DT);
                dataGridView2.DataSource = DT;
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip dt_menu_Randevular = new System.Windows.Forms.ContextMenuStrip();
                int position = dataGridView1.HitTest(e.X, e.Y).RowIndex;

                if (position >= 0)
                {
                    dt_menu_Randevular.Items.Add("Yenile").Name = "Yenile";

                }
                dt_menu_Randevular.Show(dataGridView1, new Point(e.X, e.Y));

                dt_menu_Randevular.ItemClicked += Dt_menu_Randevular_ItemClicked;
            }
        }

        private void Dt_menu_Randevular_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // throw new NotImplementedException();

            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataTable DT2 = new DataTable();
            SqlDataAdapter DARandevuListele = new SqlDataAdapter("Select b.randevuID,a.doktorAd,a.doktorSoyad,b.randevuTarih,b.randevuSaat,b.randevuDurum from Tbl_Doktorlar a,Tbl_Randevular b where a.doktorID=b.randevuDoktorID and randevuDurum=" + 0, con.baglanti());
            DARandevuListele.Fill(DT2);
            dataGridView1.DataSource = DT2;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //duyuru
            comboBox2.Items.Clear();
            int secilenBransID = comboBox1.SelectedIndex + 1;
            label11.Text = secilenBransID.ToString();
            SqlCommand komutBransDoktorEkle = new SqlCommand("Select doktorAd,doktorSoyad,doktorBrans from Tbl_Doktorlar where doktorBransID=" + secilenBransID, con.baglanti());
            SqlDataReader DRBransDoktorEkle = komutBransDoktorEkle.ExecuteReader();
            while (DRBransDoktorEkle.Read())
            {
                comboBox2.Items.Add(DRBransDoktorEkle[0] + " " + DRBransDoktorEkle[1]);
            }
            comboBox2.Text = "";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand sekreterID = new SqlCommand("select * from Tbl_Sekreterler where sekreterTC="+label4.Text,con.baglanti());
            SqlDataReader DRSekreterID = sekreterID.ExecuteReader();
            if (DRSekreterID.Read())
            {
                label16.Text = DRSekreterID[0].ToString();
                
            }

            if(comboBox2.Text!="")
            {
                string[] adSoyadParcala;
                adSoyadParcala = comboBox2.Text.Split(' ');
                string doktorAD = adSoyadParcala[0];
                string doktorSoyad = adSoyadParcala[1];
                SqlCommand komutBransDoktor = new SqlCommand("Select doktorID from Tbl_Doktorlar where doktorBransID=@doktorBransID and doktorAd=@doktorAd and doktorSoyad=@doktorSoyad", con.baglanti());
                komutBransDoktor.Parameters.AddWithValue("@doktorBransID", label11.Text);
                komutBransDoktor.Parameters.AddWithValue("@doktorAd", doktorAD);
                komutBransDoktor.Parameters.AddWithValue("@doktorSoyad", doktorSoyad);
                SqlDataReader DRBransDoktor = komutBransDoktor.ExecuteReader();
                if (DRBransDoktor.Read())
                {
                    label12.Text = DRBransDoktor[0].ToString();
                    duyuruListesiForm.doktorID = label12.Text;
                    SqlCommand duyuruOlustur = new SqlCommand("INSERT INTO Tbl_Duyurular (duyuruMesaj,duyuruSekreterID,duyuruDoktorID) values (@duyuruMesaj,@duyuruSekreterID,@duyuruDoktorID)",con.baglanti());
                    duyuruOlustur.Parameters.AddWithValue("@duyuruMesaj", textBox1.Text);
                    duyuruOlustur.Parameters.AddWithValue("@duyuruSekreterID", label16.Text);
                    duyuruOlustur.Parameters.AddWithValue("@duyuruDoktorID",label12.Text);
                    duyuruOlustur.ExecuteNonQuery();
                    con.baglanti().Close();
                    MessageBox.Show("Duyuru oluşturuldu.");

                }
            }
            else
            {
                MessageBox.Show("Doktor seçimi yapmadınız ya da branşta doktor bulunmamaktadır.");
            }
            //SqlCommand komutBransID = new SqlCommand("Select * from Tbl_Branslar where bransAd=@bransAd", con.baglanti());
            //komutBransID.Parameters.AddWithValue("@bransAd",comboBox1.Text);
            //SqlDataReader DRBransID = komutBransID.ExecuteReader();
            //if (DRBransID.Read())
            //{
            //    label12.Text = DRBransID[0].ToString();
            //}
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            duyuruListesiForm.Show();
        }
    }
}
