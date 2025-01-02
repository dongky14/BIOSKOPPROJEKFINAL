using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Login
{
    public partial class FILM1 : Form
    {
        public FILM1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

    
    

        private void button3_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
            // Buka kembali Form3 (beranda)
            Menu form3 = new Menu(isAdmin:false);
            form3.Show();
            this.Close(); // Tutup Form4
        }

     

        private void button2_Click_1(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
            BOOKING detailSpiderman = new BOOKING();
            detailSpiderman.Show();
            this.Hide(); // Sembunyikan F
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string f = "D:\\PEMROGRAMAN LANJUT\\UTS\\video\\THE AMAZING SPIDER-MAN 3D - Official Trailer.mp4";
            axWindowsMediaPlayer1.URL = f;
            axWindowsMediaPlayer1.Ctlcontrols.play(); // Mulai pemutaran video
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
