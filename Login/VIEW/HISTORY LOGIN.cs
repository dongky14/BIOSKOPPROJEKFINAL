using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Login.Login;
using System.IO;
using System.Xml.Linq;
using System.Data.SqlClient;
using ENTITY_HistoryLogger = Login.MODEL.ENTITY.HistoryLogger;
using Login_HistoryLogger = Login.Login.HistoryLogger;




namespace Login.VIEW
{
    public partial class HISTORY_LOGIN : Form
    {
        public HISTORY_LOGIN()
        {
            InitializeComponent();
            LoadLoginHistory();
        }
        public void LoadLoginHistory()
        {
            try
            {
                lvwUser.Items.Clear();
                var history = HistoryLogger.LoadHistory();

                // Periksa apakah admin sudah ada di history
                bool adminExists = history.Any(entry => entry[0] == UserData.AdminUsername);

                // Jika belum ada, tambahkan admin ke history
                if (!adminExists)
                {
                    history.Add(new string[]
{
                    UserData.AdminUsername,
                    UserData.AdminPassword,
                    "Admin",
                    "-",
                    "-",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                });


                    ENTITY_HistoryLogger.SaveHistory(history); // Menggunakan HistoryLogger dari Login.MODEL.ENTITY
                   // Menggunakan HistoryLogger dari Login.Login

                }

                // Tampilkan semua data di ListView
                foreach (var entry in history)
                {
                    if (entry.Length >= 6) // Pastikan memiliki minimal 6 elemen
                    {
                        var item = new ListViewItem(entry[0]);
                        item.SubItems.Add(entry[1]);
                        item.SubItems.Add(entry[2]);
                        item.SubItems.Add(entry[3]);
                        item.SubItems.Add(entry[4]);
                        item.SubItems.Add(entry[5]);
                        lvwUser.Items.Add(item);
                    }
                }
                }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat data history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtNama.Text.ToLower(); // Assuming `txtSearch` is the search TextBox.

            foreach (ListViewItem item in lvwUser.Items)
            {
                // Check if any subitem contains the search query (case-insensitive)
                bool matches = false;
                foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
                {
                    if (subItem.Text.ToLower().Contains(searchQuery))
                    {
                        matches = true;
                        break;
                    }
                }

                // Show or hide the item based on the match
                item.BackColor = matches ? Color.White : Color.LightGray; // Optional visual cue
                item.ForeColor = matches ? Color.Black : Color.DarkGray; // Optional visual cue
                txtNama.Clear();
            }
        }

        public static void SaveHistory(List<string[]> history)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("login_history.txt"))
                {
                    foreach (var entry in history)
                    {
                        writer.WriteLine(string.Join(",", entry));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHapusHistory_Click(object sender, EventArgs e)
        {
            if (lvwUser.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in lvwUser.SelectedItems)
                {
                    lvwUser.Items.Remove(item);
                }

                UpdateLoginHistory(); // Save changes to the file.
                MessageBox.Show("Selected login history has been cleared!", "Success");
            }
            else
            {
                MessageBox.Show("Please select an item to delete.", "Error");
            }
        }

        private void HISTORY_LOGIN_Load(object sender, EventArgs e)
        {
            // Atur ListView ke mode Details
            lvwUser.Columns.Clear();
            lvwUser.View = View.Details;
            lvwUser.FullRowSelect = true;
            lvwUser.GridLines = true;

            // Tambahkan kolom ke ListView
            lvwUser.Columns.Add("Username", 200, HorizontalAlignment.Left);
            lvwUser.Columns.Add("Password", 200, HorizontalAlignment.Left);
            lvwUser.Columns.Add("Nama", 250, HorizontalAlignment.Left);
            lvwUser.Columns.Add("Phone", 150, HorizontalAlignment.Left);
            lvwUser.Columns.Add("TTL", 150, HorizontalAlignment.Left);
            lvwUser.Columns.Add("Login Time", 200, HorizontalAlignment.Left);

            // Panggil fungsi untuk memuat riwayat login
            LoadLoginHistory();
        }

        public void UpdateLoginHistory()
        {
            // Ambil semua data dari ListView dan simpan kembali ke file
            List<string[]> history = new List<string[]>();

            foreach (ListViewItem item in lvwUser.Items)
            {
                if (item.SubItems.Count >= 6) // Ensure at least 6 subitems exist.
                {
                    string[] entry = new string[item.SubItems.Count];
                    for (int i = 0; i < item.SubItems.Count; i++)
                    {
                        entry[i] = item.SubItems[i].Text ?? string.Empty; // Default to empty string if null.
                    }
                    history.Add(entry);
                }
            }
            // Simpan kembali ke file
            SaveHistory(history);
        }
        public void AddNewUserToListView(string username, string password, string name, string phone, string TTL)
        {
            ListViewItem newItem = new ListViewItem(username);
            newItem.SubItems.Add(password);
            newItem.SubItems.Add(name);
            newItem.SubItems.Add(phone);
            newItem.SubItems.Add(TTL);
            newItem.SubItems.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            lvwUser.Items.Add(newItem);

            // Perbarui file history
            UpdateLoginHistory();
        }


        private void LoadUsers()
        {
            lvwUser.Items.Clear();

            using (SqlConnection conn = new SqlConnection("your_connection_string"))
            {
                string query = "SELECT Username, Password, Name, Phone, DOB FROM Users";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["Username"].ToString());
                    item.SubItems.Add(reader["Password"].ToString());
                    item.SubItems.Add(reader["Name"].ToString());
                    item.SubItems.Add(reader["Phone"].ToString());
                    item.SubItems.Add(DateTime.Parse(reader["DOB"].ToString()).ToString("yyyy-MM-dd"));
                    lvwUser.Items.Add(item);
                }
            }
        }

        private void lvwUser_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            if (lvwUser.SelectedItems.Count > 0)
            {
                // Ambil data dari item yang dipilih
                ListViewItem selectedItem = lvwUser.SelectedItems[0];

                string username = selectedItem.SubItems[0].Text;
                string password = selectedItem.SubItems[1].Text;
                string name = selectedItem.SubItems[2].Text;
                string phone = selectedItem.SubItems[3].Text;
                string dob = selectedItem.SubItems[4].Text;

                // Buka form UPDATE_AKUN dan kirimkan data
                UPDATE_AKUN updateAkunForm = new UPDATE_AKUN(username, password, name, phone, dob);
                updateAkunForm.ShowDialog();

                // Perbarui ListView setelah data diubah
                LoadLoginHistory();
            }
            else
            {
                MessageBox.Show("Pilih data terlebih dahulu untuk diupdate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
