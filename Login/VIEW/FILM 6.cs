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
    public partial class FILM_6 : Form
    {
        public FILM_6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string f = "D:\\PEMROGRAMAN LANJUT\\UTS\\video\\Star Wars_ The Rise of Skywalker _ Final Trailer.mp4";
            axWindowsMediaPlayer1.URL = f;
            axWindowsMediaPlayer1.Ctlcontrols.play(); // Mulai pemutaran video
        }

   
        
        private void button2_Click_1(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
            BOOKING detailSpiderman = new BOOKING();
            detailSpiderman.Show();
            this.Hide(); // Sembunyikan Form3
        }

        private void button5_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
            Menu form3 = new Menu(isAdmin: false);
            form3.Show();
            this.Hide(); // Tutup Form6
        }
    }
}
