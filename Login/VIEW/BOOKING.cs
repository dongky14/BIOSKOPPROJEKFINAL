using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Login.CONTROLLER;
using Login.MODEL.CONTEXT;

using System.Data.SQLite;

namespace Login
{
    public partial class BOOKING : Form
    {

        private BookingController _bookingController;
        // Dictionary untuk menyimpan pasangan film dan studio
        private Dictionary<string, string> movieStudioMapping = new Dictionary<string, string>
    {
        { "The Amazing : SPIDER-MAN", "STUDIO 1" },
        { "Artemis Fowl", "STUDIO 2" },
        { "Marvel Guardian of Galaxy", "STUDIO 3" },
        { "The Northman", "STUDIO 4" }, 
        { "Spiderman Far From Home", "STUDIO 1" },
        { "Starwars The Rise Of Skywalker", "STUDIO 2" },
        { "Black Panther", "STUDIO 3" },
        { "Mufasa: The Lion King", "STUDIO 4" }
    };

        // Dictionary untuk menyimpan showtime berdasarkan studio
        private Dictionary<string, string[]> studioShowTimesMapping = new Dictionary<string, string[]>
    {
       { "STUDIO 1", new string[] { "SENIN 12:30 PM", "SENIN 15:30 PM", "SENIN 19:00 PM", "SELASA 7:30 AM", "RABU 10:30 AM", "KAMIS 12:30 PM", "JUMAT 18:30 PM" } },
        { "STUDIO 2", new string[] { "SENIN 13:30 PM", "SELASA 16:30 PM", "RABU 20:00 PM", "KAMIS 9:00 AM", "JUMAT 14:00 PM", "SABTU 12:00 PM", "MINGGU 18:30 PM" } },
        { "STUDIO 3", new string[] { "SENIN 14:00 PM", "SELASA 17:00 PM", "RABU 20:30 PM", "KAMIS 8:00 AM", "JUMAT 11:30 AM", "SABTU 14:30 PM", "MINGGU 16:00 PM" } },
        { "STUDIO 4", new string[] { "SENIN 11:30 AM", "SELASA 14:00 PM", "RABU 17:30 PM", "KAMIS 10:00 AM", "JUMAT 13:00 PM", "SABTU 16:00 PM", "MINGGU 20:30 PM" } }
    };

        private Dictionary<string, Dictionary<string, List<string>>> bookedSeats = new Dictionary<string, Dictionary<string, List<string>>>();
        private int maxSelectableSeats = 0;
        private List<Button> selectedSeats = new List<Button>();

        public BOOKING()
        {
            InitializeComponent();
            _bookingController = new BookingController(); // Tambahkan ini
        }



        private void Form8_Load(object sender, EventArgs e)
        {// Existing setup code...

            // Load booked seats for the selected movie and showtime
            string selectedMovie = CmbMovieTitle.SelectedItem?.ToString();
            string selectedShowTime = cmbShowTime.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedMovie) && !string.IsNullOrEmpty(selectedShowTime))
            {
                SeatBooking seatBooking = SeatBookingManager.GetSeatBooking(selectedMovie, selectedShowTime);

                // Loop through all seat buttons to check if they are booked
                foreach (Control control in this.Controls)
                {
                    if (control is Button && control.Name.StartsWith("button"))
                    {
                        string seatNumber = control.Text;

                        // If the seat is already booked, set it to unavailable (e.g., grey color)
                        if (seatBooking.IsSeatBooked(seatNumber))
                        {
                            control.BackColor = Color.Red; // Indicate that the seat is booked
                            control.Enabled = false; // Optionally, disable the seat button to prevent further selection
                        }
                    }
                }
            }
            // Isi ComboBox dengan daftar film
            CmbMovieTitle.Items.AddRange(movieStudioMapping.Keys.ToArray());
            cmbTicket.Items.AddRange(new string[] { "1", "2", "3", "4", "5", "6" });

            // Set default selected index for showtime ComboBox to avoid it being empty
            cmbShowTime.SelectedIndex = -1;

            // Event handler untuk ComboBox MovieTitle
            CmbMovieTitle.SelectedIndexChanged += CmbMovieTitle_SelectedIndexChanged;

            // Event handler untuk ComboBox Ticket
            cmbTicket.SelectedIndexChanged += CmbTicket_SelectedIndexChanged;

            // Loop untuk setup seat buttons
            foreach (Control control in this.Controls)
            {
                if (control is Button && control.Name.StartsWith("button"))
                {
                    control.Click += new EventHandler(Seat_Click);
                    control.BackColor = Color.Gray; // Set warna default (belum dipilih)
                }
            }
        }

        private void CmbTicket_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Pastikan nilai tiket valid dan simpan ke maxSelectableSeats
            if (int.TryParse(cmbTicket.SelectedItem?.ToString(), out int ticketCount))
            {
                maxSelectableSeats = ticketCount;

                // Reset pilihan kursi jika jumlah kursi yang dipilih melebihi tiket
                while (selectedSeats.Count > maxSelectableSeats)
                {
                    Button seatToDeselect = selectedSeats.Last();
                    seatToDeselect.BackColor = Color.Gray;
                    selectedSeats.Remove(seatToDeselect);
                }

                UpdateSeatNumberTextBox();
            }
        }

        private void Seat_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                string seatNumber = clickedButton.Text; // Get the seat number
                string selectedMovie = CmbMovieTitle.SelectedItem?.ToString();
                string selectedShowTime = cmbShowTime.SelectedItem?.ToString();

                // Check if movie and showtime are selected
                if (string.IsNullOrEmpty(selectedMovie) || string.IsNullOrEmpty(selectedShowTime))
                {
                    MessageBox.Show("Please select a movie and showtime first.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Retrieve the SeatBooking object for this movie and showtime
                SeatBooking seatBooking = SeatBookingManager.GetSeatBooking(selectedMovie, selectedShowTime);

                // Check if the seat is already booked
                if (seatBooking.IsSeatBooked(seatNumber))
                {
                    MessageBox.Show($"Sorry, seat {seatNumber} is already booked for this showtime.", "Seat Already Booked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (clickedButton.BackColor == Color.Green) // If seat is already selected
                {
                    clickedButton.BackColor = Color.Gray; // Reset to default color
                    selectedSeats.Remove(clickedButton); // Remove from selected seats list
                    seatBooking.CancelSeat(seatNumber); // Remove from booked seats
                }
                else
                {
                    if (selectedSeats.Count < maxSelectableSeats) // Check if still can select seats
                    {
                        clickedButton.BackColor = Color.Green; // Mark seat as selected
                        selectedSeats.Add(clickedButton); // Add to selected seats list
                        seatBooking.BookSeat(seatNumber); // Add to booked seats
                    }
                    else
                    {
                        MessageBox.Show($"You can only select up to {maxSelectableSeats} seat(s).", "Seat Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                UpdateSeatNumberTextBox();
            }
        }


        private void UpdateSeatNumberTextBox()
        {
            // Update TextBox dengan daftar kursi yang dipilih, dipisahkan dengan koma
            txtSeatNumber.Text = string.Join(", ", selectedSeats.Select(s => s.Text));
        }

        private void CmbMovieTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Event handler jika film dipilih
            if (movieStudioMapping.TryGetValue(CmbMovieTitle.SelectedItem.ToString(), out string studio))
            {
                txtStudio.Text = studio;

                // Update ComboBox ShowTime sesuai dengan studio yang dipilih
                if (studioShowTimesMapping.ContainsKey(studio))
                {
                    cmbShowTime.Items.Clear();
                    cmbShowTime.Items.AddRange(studioShowTimesMapping[studio]);
                }
            }
        }
        private int CalculateTicketPrice(DateTime selectedDate)
        {
            int weekdayPrice = 30000;
            int weekendPrice = 40000;

            if (selectedDate.DayOfWeek == DayOfWeek.Saturday || selectedDate.DayOfWeek == DayOfWeek.Sunday)
            {
                return weekendPrice;
            }
            else
            {
                return weekdayPrice;
            }
        }
        private int GenerateUniqueId()
        {
            // Example logic: Use current timestamp or other logic to generate a unique ID
            return DateTime.Now.GetHashCode(); // This will return a unique integer based on the current time
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_bookingController == null)
            {
                _bookingController = new BookingController();
            }
            // Validasi input
            if (CmbMovieTitle.SelectedItem == null || string.IsNullOrEmpty(txtSeatNumber.Text) || cmbShowTime.SelectedItem == null || cmbTicket.SelectedItem == null)
            {
                MessageBox.Show("Please complete all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // Hitung harga tiket
            DateTime selectedDate = DatePicker.Value;
            int ticketPrice = CalculateTicketPrice(selectedDate);
            int ticketCount = int.Parse(cmbTicket.SelectedItem.ToString());
            int totalPrice = ticketPrice * ticketCount;
            int id = GenerateUniqueId();

            // Simpan data booking ke database termasuk payment method
            bool success = _bookingController.Add(
                CmbMovieTitle.SelectedItem.ToString(),
                txtStudio.Text,
                cmbShowTime.SelectedItem.ToString(),
                selectedDate.ToString("yyyy-MM-dd"),
                txtSeatNumber.Text,
                ticketCount,
                totalPrice,
                cmbPaymentMethod.SelectedItem.ToString() // Add payment method
            );

            // Simpan data ke OrderData (jika diperlukan)
            OrderData.MovieTitle = CmbMovieTitle.SelectedItem.ToString();
            OrderData.Studio = txtStudio.Text;
            OrderData.ShowTime = cmbShowTime.SelectedItem.ToString();
            OrderData.Date = selectedDate.ToString("dd/MM/yyyy");
            OrderData.SeatNumber = txtSeatNumber.Text;
            OrderData.PaymentMethod = "QRIS";
            OrderData.TicketCount = ticketCount;
            OrderData.TotalPrice = $"Rp {totalPrice:N0}";

            // Buat ListViewItem
            ListViewItem listItem = new ListViewItem(CmbMovieTitle.SelectedItem.ToString());
            listItem.SubItems.Add(txtStudio.Text);
            listItem.SubItems.Add(cmbShowTime.SelectedItem.ToString());
            listItem.SubItems.Add(selectedDate.ToString("dd/MM/yyyy"));
            listItem.SubItems.Add(txtSeatNumber.Text);
            listItem.SubItems.Add(ticketCount.ToString());
            listItem.SubItems.Add($"Rp {totalPrice:N0}");

            // Tambahkan data ke GlobalHistory melalui History form
            History historyForm = new History();
            historyForm.AddItemToListView(listItem);

            if (success)
            {
                MessageBox.Show("Booking successfully added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


            } // Redirect ke form lain jika diperlukan
            DetailTicket detailTicket = new DetailTicket(isAdmin: true);
            detailTicket.Show();
            this.Hide();

        }

        private void button75_Click(object sender, EventArgs e)
        {
            Menu form3 = new Menu(isAdmin: false);
            form3.Show();
            this.Hide(); // Tutup Fo
        }

        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtStudio_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBoxSpiderman_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxFowl_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxGuardian_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxNorthman_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}