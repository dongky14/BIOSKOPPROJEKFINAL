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
    public partial class EDIT_FILM4 : Form
    {        private Film film;

        public EDIT_FILM4(Film film)
        {
            InitializeComponent();
            this.film = film;

            // Populate the form fields with the film's data
            txtJudul.Text = film.Title;
            txtGenre.Text = film.Genre;
            txtDurasi.Text = film.Duration;
            txtSutradara.Text = film.Director;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Set the selected image as the film's poster
                film.Poster = Image.FromFile(openFileDialog.FileName);

                // Display the selected image in the PictureBox
                pictureBox1.Image = film.Poster;

                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void EDIT_FILM4_Load(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Ambil data film yang diedit
            Film updatedFilm = new Film
            {
                Title = txtJudul.Text,
                Genre = txtGenre.Text,
                Duration = txtDurasi.Text,
                Director = txtSutradara.Text, // Sutradara sebagai kunci
                Poster = pictureBox1.Image // Ambil gambar dari PictureBox
            };

            // Referensi ke form utama (Menu)
            Menu mainMenu = Application.OpenForms.OfType<Menu>().FirstOrDefault();
            if (mainMenu != null)
            {
                mainMenu.UpdateFilmDetailsFromEdit(updatedFilm);
            }
            else
            {
                MessageBox.Show("Gagal memperbarui film!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
