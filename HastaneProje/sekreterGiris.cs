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
    public partial class sekreterGiris : Form
    {
        public sekreterGiris()
        {
            InitializeComponent();
        }
        sqlbaglanti con = new sqlbaglanti();
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand sekreterGiris = new SqlCommand("Select * from Tbl_Sekreterler where sekreterTC=@p1 and sekreterSifre=@p2",con.baglanti());
            sekreterGiris.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
            sekreterGiris.Parameters.AddWithValue("@p2", maskedTextBox2.Text);
            SqlDataReader DRsekreterGiris = sekreterGiris.ExecuteReader();
            if(DRsekreterGiris.Read())
            {
                MessageBox.Show("Giriş Başarılı");
                sekreterDetay sekreterDetayForm = new sekreterDetay();
                sekreterDetayForm.sekreterTC = maskedTextBox1.Text;
                sekreterDetayForm.Show();
                
                this.Hide();
            }
            else
            {
                MessageBox.Show("Bilgilerinizi doğru giriniz.","Giriş Hatası",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
