using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string f = "D:\\PEMROGRAMAN LANJUT\\UTS\\video\\THE NORTHMAN - Official Trailer - Only In Theaters April 22.mp4";
            axWindowsMediaPlayer1.URL = f;
            axWindowsMediaPlayer1.Ctlcontrols.play(); // Mulai pemutaran video
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Pause video jika slide berubah
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

   
        private void button2_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
            BOOKING detailSpiderman = new BOOKING();
            detailSpiderman.Show();
            this.Hide(); // Sembunyikan Form3
        }

        private void button5_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
            // Buka kembali Form3 (beranda)
            Menu form3 = new Menu(isAdmin: false);
            form3.Show();
            this.Hide(); // Tutup Form7
        }
    }
}
