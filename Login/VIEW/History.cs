using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Login.CONTROLLER;
using System.Data.SqlClient;
using Login.MODEL.CONTEXT;
using Login.MODEL.ENTITY;

namespace Login
{
    public partial class History : Form
    {
        private BookingController _history;


        public History()
        {
            InitializeComponent();
            _history = new BookingController(); // Tambahkan inisialisasi
        }

        public void AddItemToListView(ListViewItem item)
        {
            if (!GlobalHistory.HistoryItems.Any(existingItem =>
                existingItem.Text == item.Text &&
                existingItem.SubItems[1].Text == item.SubItems[1].Text &&
                existingItem.SubItems[2].Text == item.SubItems[2].Text &&
                existingItem.SubItems[3].Text == item.SubItems[3].Text))
            {
                GlobalHistory.HistoryItems.Add(item);
                lvwHistory.Items.Add((ListViewItem)item.Clone());
                UpdateTotalPendapatan();
            }
        }

        private void History_Load(object sender, EventArgs e)
        {

            lvwHistory.Columns.Clear();
            lvwHistory.View = View.Details;
            lvwHistory.FullRowSelect = true;
            lvwHistory.GridLines = true;

            lvwHistory.Columns.Add("No", 50);          // Indeks 0
            lvwHistory.Columns.Add("Movie Title", 300); // Indeks 1
            lvwHistory.Columns.Add("Studio", 100);      // Indeks 2
            lvwHistory.Columns.Add("Show Time", 200);   // Indeks 3
            lvwHistory.Columns.Add("Date", 200);        // Indeks 4
            lvwHistory.Columns.Add("Seat Number", 200); // Indeks 5
            lvwHistory.Columns.Add("Ticket Count", 200);// Indeks 6
            lvwHistory.Columns.Add("Total Price", 300); // Indeks 7

            lblTotal.Font = new Font("Times New Roman", 16, FontStyle.Bold);

            LoadHistory();
            UpdateTotalPendapatan();
        }

        private void LoadHistory()
        {
            lvwHistory.Items.Clear();

            // Ambil daftar bookings dari controller
            var bookings = _history.GetAll();

            // Konversi list bookings ke DataTable
            DataTable bookingsTable = ConvertToDataTable(bookings);

            int index = 1;  // Kolom "No" untuk nomor urut

            // Loop melalui setiap baris dalam DataTable dan tambahkan ke ListView
            foreach (DataRow row in bookingsTable.Rows)
            {
                var item = new ListViewItem(index.ToString());  // Tambahkan nomor urut di kolom "No"

                // Tambahkan subitems sesuai urutan kolom
                item.SubItems.Add(row["MovieTitle"].ToString());
                item.SubItems.Add(row["Studio"].ToString());
                item.SubItems.Add(row["ShowTime"].ToString());
                item.SubItems.Add(row["Date"].ToString());
                item.SubItems.Add(row["SeatNumber"].ToString());
                item.SubItems.Add(row["TicketCount"].ToString());

                // Pastikan TotalPrice diformat sebagai mata uang
                if (decimal.TryParse(row["TotalPrice"].ToString(), out decimal totalPrice))
                {
                    item.SubItems.Add(totalPrice.ToString("C0"));  // Format sebagai mata uang
                }
                else
                {
                    item.SubItems.Add("Rp 0");  // Nilai default jika parsing gagal
                }

                // Tambahkan item ke ListView
                lvwHistory.Items.Add(item);

                index++;  // Increment nomor urut
            }

            // Perbarui total pendapatan
            UpdateTotalPendapatan();
        }

        private DataTable ConvertToDataTable(List<Booking> bookings)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("MovieTitle", typeof(string));
            dt.Columns.Add("Studio", typeof(string));
            dt.Columns.Add("ShowTime", typeof(string));
            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("SeatNumber", typeof(string));
            dt.Columns.Add("TicketCount", typeof(int));
            dt.Columns.Add("TotalPrice", typeof(decimal));  // Pastikan tipe data decimal

            foreach (var booking in bookings)
            {
                dt.Rows.Add(booking.Id, booking.MovieTitle, booking.Studio, booking.ShowTime, booking.Date, booking.SeatNumber, booking.TicketCount, booking.TotalPrice);
            }

            return dt;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            BOOKING menubooking = new BOOKING();
            menubooking.Show();
            this.Hide();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtNama.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                LoadHistory();
                return;
            }

            var filteredItems = lvwHistory.Items.Cast<ListViewItem>()
                .Where(item => item.SubItems.Cast<ListViewItem.ListViewSubItem>()
                                             .Any(subItem => subItem.Text.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0))
                .ToList();

            lvwHistory.Items.Clear();

            foreach (var item in filteredItems)
            {
                lvwHistory.Items.Add((ListViewItem)item.Clone());
            }

            if (filteredItems.Count == 0)
            {
                MessageBox.Show("Tidak ada data yang ditemukan!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateTotalPendapatan()
        {
            decimal totalPendapatan = 0;

            foreach (ListViewItem item in lvwHistory.Items)
            {
                if (item.SubItems.Count > 7) // Pastikan ada cukup subitem
                {
                    string totalPriceText = item.SubItems[7].Text.Replace("Rp", "").Replace(".", "").Trim();

                    if (decimal.TryParse(totalPriceText, out decimal totalPrice))
                    {
                        totalPendapatan += totalPrice;
                    }
                    else
                    {
                        Console.WriteLine($"Gagal mengonversi total price: {totalPriceText}");
                    }
                }
                else
                {
                    Console.WriteLine("Jumlah subitem tidak mencukupi.");
                }
            }

            // Tampilkan total pendapatan dalam format mata uang
            lblTotal.Text = $"Total Pendapatan: Rp {totalPendapatan:N0}";
        }

        private void btnHapusHistory_Click_1(object sender, EventArgs e)
        {
            if (lvwHistory.SelectedItems.Count == 0)
            {
                MessageBox.Show("Pilih item untuk dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (ListViewItem selectedItem in lvwHistory.SelectedItems)
            {
                // Hapus dari database berdasarkan Id
                int id = int.Parse(selectedItem.Text);
                int isDeleted = _history.Remove(id);

                if (isDeleted > 0)
                {
                    lvwHistory.Items.Remove(selectedItem);
                }
                else
                {
                    MessageBox.Show($"Gagal menghapus booking dengan Id {id}.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            MessageBox.Show("Item berhasil dihapus.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            UpdateTotalPendapatan();
        }

        private void lvwHistory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtNama_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
