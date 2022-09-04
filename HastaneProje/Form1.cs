using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneProje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hastaGiris hastaGirisForm = new hastaGiris();
            hastaGirisForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            doktorGiris doktorGirisForm = new doktorGiris();
            doktorGirisForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sekreterGiris sekreterGirisForm = new sekreterGiris();
            sekreterGirisForm.Show();
            this.Hide();
        }
    }
}
