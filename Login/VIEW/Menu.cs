using Login.MODEL.CONTEXT;
using Login.VIEW;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Login
{

    public partial class Menu : Form
    {
        private List<Film> films; // Koleksi untuk menyimpan data film

        bool sidebarexpand;


        private bool isAdmin; // Variabel untuk menyimpan status admin

        public Menu(bool isAdmin = true) // Tambahkan nilai default
        {
            InitializeComponent();
            this.isAdmin = isAdmin; // Tetapkan status admin

            // Gunakan FilmManager untuk mendapatkan data film
            films = FilmManager.Films;

            // Perbarui tampilan berdasarkan data film
            foreach (var film in films)
            {
                UpdateFilmUI(film);
            }
            // Hapus inisialisasi default jika sudah menggunakan FilmManager
            if (FilmManager.Films.Count == 0) // Hanya inisialisasi jika belum ada data
            {
                FilmManager.Films.AddRange(new List<Film>
        {
            new Film
            {
                Title = "Spiderman",
                Genre = "Action",
                Director = "Jon Watts",
                Duration = "2h 30m",
                Poster = pictureBoxSpiderman.Image
            },
            new Film
            {
                Title = "Guardian of the Galaxy",
                Genre = "Action/Adventure",
                Director = "James Gunn",
                Duration = "2h 2m",
                Poster = pictureBoxGuardian.Image
            },
               new Film
    {
                 Title = "The Northman",
                 Genre = "Action/Adventure",
                 Director = "Robert Eggers",
                Duration = "2h 17m",
                 Poster = pictureBoxNorthman.Image // Pastikan gambar sudah diatur di PictureBox
    },
                new Film
    {
                 Title = "Artemis Fowl",
                 Genre = "Adventure/Fantasy",
                Director = "Kenneth Branagh",
                Duration = "1h 55m",
                Poster = pictureBoxFowl.Image // Pastikan gambar sudah diatur di PictureBox
            }

        });
            }

            // Gunakan data dari FilmManager
            films = FilmManager.Films;
        }



        // In EDIT_FILM.cs



        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarexpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width <= sidebar.MinimumSize.Width)
                {
                    sidebarexpand = false;
                    sidebarTimer.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width >= sidebar.MaximumSize.Width)
                {
                    sidebarexpand = true;
                    sidebarTimer.Stop();
                }
            }

        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.TabStop = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            btnEditFilm.FlatStyle = FlatStyle.Flat;
            btnEditFilm.FlatAppearance.BorderSize = 0;
            btnEditFilm.TabStop = false;
            EDIT_FILM editFilmForm = new EDIT_FILM(); // Kirim referensi form Menu
            editFilmForm.Show();


        }
        private Film FindFilmByDirector(string director)
        {
            return films.FirstOrDefault(f => f.Director.Equals(director, StringComparison.OrdinalIgnoreCase));
        }
        public void UpdateFilmDetailsFromEdit(Film updatedFilm)
        {
            // Cari film berdasarkan sutradara
            var film = FindFilmByDirector(updatedFilm.Director);
            if (film != null)
            {
                // Perbarui data film di koleksi
                film.Title = updatedFilm.Title;
                film.Genre = updatedFilm.Genre;
                film.Duration = updatedFilm.Duration;
                film.Poster = updatedFilm.Poster;

                // Perbarui tampilan di UI
                UpdateFilmUI(film);

                // Tampilkan pesan sukses
                MessageBox.Show("Film berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Film dengan sutradara tersebut tidak ditemukan!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateFilmUI(Film film)
        {
            if (film.Director == "Jon Watts")
            {
                pictureBoxSpiderman.Image = film.Poster;
                lblFilmTitle.Text = film.Title;
                lblFilmGenre.Text = film.Genre;
                lblFilmDuration.Text = film.Duration;
                lblSutradara.Text = film.Director;
            }
            else if (film.Director == "James Gunn")
            {
                pictureBoxGuardian.Image = film.Poster;
                lblJudulGuardian.Text = film.Title;
                lblGenreGuardian.Text = film.Genre;
                lblDurasiGuardian.Text = film.Duration;
                lblSutradaraGuardian.Text = film.Director;
            }
            else if (film.Director == "Robert Eggers")
            {
                pictureBoxNorthman.Image = film.Poster;
                lblJudulNorthman.Text = film.Title;
                lblGenreNorthman.Text = film.Genre;
                lblDurasiNorthman.Text = film.Duration;
                lblSutradaraNorthman.Text = film.Director;
            }
            else if (film.Director == "Kenneth Branagh")
            {
                pictureBoxFowl.Image = film.Poster;
                lblJudulFowl.Text = film.Title;
                lblGenreFowl.Text = film.Genre;
                lblDurasiFowl.Text = film.Duration;
                lblSutradaraFowl.Text = film.Director;
            }
        }

        private void btnTransaksi_Click(object sender, EventArgs e)
        {

            btnHistory.FlatStyle = FlatStyle.Flat;
            btnHistory.FlatAppearance.BorderSize = 0;
            btnHistory.TabStop = false;

            ListViewItem listItem = new ListViewItem(OrderData.MovieTitle);
            listItem.SubItems.Add(OrderData.Studio);
            listItem.SubItems.Add(OrderData.ShowTime);
            listItem.SubItems.Add(OrderData.Date);
            listItem.SubItems.Add(OrderData.SeatNumber);
            listItem.SubItems.Add(OrderData.TicketCount.ToString());
            listItem.SubItems.Add(OrderData.TotalPrice);

            // Buka Form History dan kirim data ke ListView
            History historyForm = new History();
            historyForm.AddItemToListView(listItem);
            historyForm.Show();

        }

        private void Menu_Load(object sender, EventArgs e)
        {
            // Sembunyikan tombol jika bukan admin
            if (!isAdmin)
            {
                btnHistory.Visible = false; // Sembunyikan tombol History
                btnEditFilm.Visible = false;
                btnAkunHistory.Visible = false;// Sembunyikan tombol Edit Film
            }
            // Perbarui tampilan UI berdasarkan data terbaru
            foreach (var film in FilmManager.Films)
            {
                UpdateFilmUI(film);
            }
        }

        private void pictureBoxSpiderman_Click(object sender, EventArgs e)
        {
            var film = films.FirstOrDefault(f => f.Director == "Jon Watts");
            if (film != null && film.IsUpdated)
            {
                // Buka form baru untuk detail film yang diperbarui
                FILM_5 detailForm = new FILM_5();
                detailForm.Show();
            }
            else
            {
                // Buka form default
                FILM1 detailSpider = new FILM1();
                detailSpider.Show();
            }
            this.Hide();
        }



        private void pictureBoxNorthman_Click(object sender, EventArgs e)
        {
            var film = films.FirstOrDefault(f => f.Director == "Robert Eggers");
            if (film != null && film.IsUpdated)
            {
                // Buka form baru untuk detail film yang diperbarui
                FILM_7 detailForm = new FILM_7();
                detailForm.Show();
            }
            else
            {
                // Buka form default
                Form7 detailNorthman = new Form7();
                detailNorthman.Show();
            }
            this.Hide();

            // Sembunyikan Form3
        }

        private void pictureBoxGuardian_Click(object sender, EventArgs e)
        {
            var film = films.FirstOrDefault(f => f.Director == "James Gunn");
            if (film != null && film.IsUpdated)
            {
                // Buka form baru untuk detail film yang diperbarui
                FILM_6 detailForm = new FILM_6();
                detailForm.Show();
            }
            else
            {
                // Buka form default
                Form5 detailGuardian = new Form5();
                detailGuardian.Show();
            }
            this.Hide();
        }

        private void pictureBoxFowl_Click(object sender, EventArgs e)
        {
            var film = films.FirstOrDefault(f => f.Director == "Kenneth Branagh");
            if (film != null && film.IsUpdated)
            {
                // Buka form baru untuk detail film yang diperbarui
                FILM_8 detailForm = new FILM_8();
                detailForm.Show();
            }
            else
            {
                // Buka form default
                Form6 detailAF = new Form6();
                detailAF.Show();
            }
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            UserData.SaveUserData(); // Simpan data pengguna sebelum logout
            LoginAdmin.IsAdmin = false;
            Login loginForm = new Login();
            loginForm.Show();
            this.Close();
        }

        private void lblFilmTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnPIlihFilm1_Click(object sender, EventArgs e)
        {
            // Membuat instance baru dari form FILM_5
            FILM_5 film5Form = new FILM_5();

            // Menampilkan form FILM_5
            film5Form.Show();

            // Menyembunyikan form Menu saat FILM_5 ditampilkan (opsional)
            this.Hide();
        }

        private void btnPilihFilm3_Click(object sender, EventArgs e)
        {
            // Membuat instance baru dari form FILM_5
            FILM_7 film5Form = new FILM_7();

            // Menampilkan form FILM_5
            film5Form.Show();

            // Menyembunyikan form Menu saat FILM_5 ditampilkan (opsional)
            this.Hide();
        }

        private void btnPilihFilm2_Click(object sender, EventArgs e)
        {
            // Membuat instance baru dari form FILM_5
            FILM_6 film5Form = new FILM_6();

            // Menampilkan form FILM_5
            film5Form.Show();

            // Menyembunyikan form Menu saat FILM_5 ditampilkan (opsional)
            this.Hide();
        }

        private void btnPiihFIlm4_Click(object sender, EventArgs e)
        {
            // Membuat instance baru dari form FILM_5
            FILM_8 film5Form = new FILM_8();

            // Menampilkan form FILM_5
            film5Form.Show();

            // Menyembunyikan form Menu saat FILM_5 ditampilkan (opsional)
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            // Create an instance of HISTORY_LOGIN and pass the context
            HISTORY_LOGIN historyLoginForm = new HISTORY_LOGIN(context);

            // Show the form
            historyLoginForm.Show();
        }
    }
}