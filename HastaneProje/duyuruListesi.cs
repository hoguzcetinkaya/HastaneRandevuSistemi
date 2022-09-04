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
    public partial class duyuruListesi : Form
    {
        public duyuruListesi()
        {
            InitializeComponent();
        }

        sqlbaglanti con = new sqlbaglanti();

        public string doktorID;
        public string sekreterAdSoyad;
        public string sekreterID;
        private void duyuruListesi_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataTable DT = new DataTable();
            SqlDataAdapter DADuyurular = new SqlDataAdapter("SELECT Tbl_Doktorlar.doktorAd, Tbl_Doktorlar.doktorSoyad,Tbl_Duyurular.duyuruMesaj,Tbl_Sekreterler.sekreterAdSoyad FROM Tbl_Doktorlar INNER JOIN Tbl_Duyurular ON Tbl_Duyurular.duyuruDoktorID = Tbl_Doktorlar.doktorID and Tbl_Duyurular.durum = 0 INNER JOIN Tbl_Sekreterler ON Tbl_Sekreterler.sekreterID = Tbl_Duyurular.duyuruSekreterID", con.baglanti());
            DADuyurular.Fill(DT);
            dataGridView1.DataSource = DT;
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip dt_menu_Duyurular = new System.Windows.Forms.ContextMenuStrip();
                int position = dataGridView1.HitTest(e.X, e.Y).RowIndex;

                if (position >= 0)
                {
                    dt_menu_Duyurular.Items.Add("Yenile").Name = "Yenile";

                }
                dt_menu_Duyurular.Show(dataGridView1, new Point(e.X, e.Y));
                dt_menu_Duyurular.ItemClicked += Dt_menu_Duyurular_ItemClicked;
                
            }
        }

        private void Dt_menu_Duyurular_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //throw new NotImplementedException();

            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataTable DT = new DataTable();
            SqlDataAdapter DADuyurular = new SqlDataAdapter("SELECT Tbl_Doktorlar.doktorAd, Tbl_Doktorlar.doktorSoyad,Tbl_Duyurular.duyuruMesaj,Tbl_Sekreterler.sekreterAdSoyad FROM Tbl_Doktorlar INNER JOIN Tbl_Duyurular ON Tbl_Duyurular.duyuruDoktorID = Tbl_Doktorlar.doktorID and Tbl_Duyurular.durum = 0 INNER JOIN Tbl_Sekreterler ON Tbl_Sekreterler.sekreterID = Tbl_Duyurular.duyuruSekreterID", con.baglanti());
            DADuyurular.Fill(DT);
            dataGridView1.DataSource = DT;
        }
    }
}
