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
    public partial class EDIT_FILM : Form
    {
         
       

        public EDIT_FILM()
        {
            InitializeComponent();
       
        }

      

        private void button3_Click(object sender, EventArgs e)
        {
            // Create a Film object (e.g., Spiderman film)
            Film guardianFilm = new Film
            {
                Title = "Guardian of the Galaxy",
                Genre = "Action/Adventure",
                Director = "James Gunn",
                Duration = "2h 2m",
                Poster = pictureBoxGuardian.Image // Set initial poster image
            };

            // Pass the Film object to the EDIT_FILM_1 form
            EDIT_FLM_2 edit1 = new EDIT_FLM_2(guardianFilm);
            edit1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Create a Film object (e.g., Spiderman film)
           
              // Button untuk Northman
            Film northmanFilm = new Film
            {
                Title = "The Northman",
                Genre = "Action/Adventure",
                Director = "Robert Eggers", // Perbaikan: Sutradara sesuai
                Duration = "2h 17m", // Perbaikan: Durasi sesuai
                Poster = pictureBoxNorthman.Image
            };
            

            // Pass the Film object to the EDIT_FILM_1 form
            EDIT_FILM_3 edit1 = new EDIT_FILM_3(northmanFilm);
            edit1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create a Film object (e.g., Spiderman film)
            Film artemisFilm = new Film
            {
                Title = "Artemis Fowl",
                Genre = "Adventure/Fantasy",
                Director = "Kenneth Branagh", // Perbaikan: Sutradara sesuai
                Duration = "1h 55m", // Perbaikan: Durasi sesuai
                Poster = pictureBoxFowl.Image
            };

            // Pass the Film object to the EDIT_FILM_1 form
            EDIT_FILM4 edit1 = new EDIT_FILM4(artemisFilm);
            edit1.Show();
            this.Hide();
        }

        private void btnFilm1_Click(object sender, EventArgs e)
        {
            // Create a Film object (e.g., Spiderman film)
            Film spidermanFilm = new Film
            {
                Title = "Spiderman",
                Genre = "Action",
                Director = "Jon Watts",
                Duration = "2h 30m",
                Poster = pictureBoxSpiderman.Image // Set initial poster image
            };

            // Pass the Film object to the EDIT_FILM_1 form
            EDIT_FILM_1 edit1 = new EDIT_FILM_1(spidermanFilm);
            edit1.Show();
            this.Hide();
        }

        private void pictureBoxFowl_Click(object sender, EventArgs e)
        {

        }
    }
}
