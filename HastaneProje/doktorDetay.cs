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
    public partial class doktorDetay : Form
    {
        public doktorDetay()
        {
            InitializeComponent();
        }

        sqlbaglanti con = new sqlbaglanti();
        public string doktorID,doktorTC, doktorAd, doktorSoyad;

        private void doktorDetay_Load(object sender, EventArgs e)
        {
            label3.Text = doktorAd + " " + doktorSoyad;
            label4.Text = doktorTC;




            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataTable DT = new DataTable();
            SqlDataAdapter DAHastaRandevuBilgi = new SqlDataAdapter("select a.hastaID, b.randevuTarih,b.randevuSaat,b.randevuHastaSikayet from Tbl_Hastalar a,Tbl_Randevular b where a.hastaID=b.randevuHastaID and randevuDoktorID=" + doktorID + " and randevuDurum=" + 1 + " and randevuGecmisDurum="+0, con.baglanti()); ;
            DAHastaRandevuBilgi.Fill(DT);

            dataGridView1.DataSource = DT;
            dataGridView1.Columns["hastaID"].Visible = false;



        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string secilenID = dataGridView1.CurrentRow.Cells["hastaID"].Value.ToString();
            SqlCommand komutHastaBilgi = new SqlCommand("select hastaAd,hastaSoyad,hastaTC from Tbl_hastalar where hastaID="+secilenID,con.baglanti());
            SqlDataReader DRHastaBilgi = komutHastaBilgi.ExecuteReader();
            while(DRHastaBilgi.Read())
            {
                label7.Text = DRHastaBilgi[0].ToString()+" "+DRHastaBilgi[1].ToString();
                label8.Text = DRHastaBilgi[2].ToString();
            }
            textBox1.Text = dataGridView1.CurrentRow.Cells["randevuHastaSikayet"].Value.ToString();

           
        }
       
        private void button4_Click(object sender, EventArgs e)
        {
            string randevuID;
            string randevuHastaID = dataGridView1.CurrentRow.Cells["hastaID"].Value.ToString();
            string randevuHastaTarih = dataGridView1.CurrentRow.Cells["randevuTarih"].Value.ToString();
            string randevuHastaSaat = dataGridView1.CurrentRow.Cells["randevuSaat"].Value.ToString();
            string randevuHastaSikayet = dataGridView1.CurrentRow.Cells["randevuHastaSikayet"].Value.ToString();
            SqlCommand hastaRandevuID = new SqlCommand("Select randevuID from Tbl_Randevular where randevuHastaID=@randevuHastaID and randevuTarih=@randevuTarih and randevuSaat=@randevuSaat and randevuHastaSikayet=@randevuHastaSikayet", con.baglanti());
            hastaRandevuID.Parameters.AddWithValue("@randevuHastaID", randevuHastaID);
            hastaRandevuID.Parameters.AddWithValue("@randevuTarih", randevuHastaTarih);
            hastaRandevuID.Parameters.AddWithValue("@randevuSaat", randevuHastaSaat);
            hastaRandevuID.Parameters.AddWithValue("@randevuHastaSikayet", randevuHastaSikayet);
            SqlDataReader DRHastaRandevuID = hastaRandevuID.ExecuteReader();
            if(DRHastaRandevuID.Read())
            {
                randevuID = DRHastaRandevuID[0].ToString();
                if (randevuID != null)
                {
                    SqlCommand hastaRandevuBitir = new SqlCommand("UPDATE Tbl_Randevular set randevuRecete=@randevuRecete, randevuGecmisDurum=@randevuGecmisDurum where randevuID=" + randevuID, con.baglanti());
                    hastaRandevuBitir.Parameters.AddWithValue("@randevuRecete", textBox1.Text);
                    hastaRandevuBitir.Parameters.AddWithValue("@randevuGecmisDurum", "True");
                    hastaRandevuBitir.ExecuteNonQuery();
                    con.baglanti().Close();
                    MessageBox.Show("BAŞARILI");
                }
                MessageBox.Show("BBBB");
            }
                
                
           
                
            



        }
    }
}
