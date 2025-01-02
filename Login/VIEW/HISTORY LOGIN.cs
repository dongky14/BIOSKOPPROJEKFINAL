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
using Login.CONTROLLER;

using Login.MODEL.CONTEXT;

using System.Data.SQLite;


namespace Login.VIEW
{
    public partial class HISTORY_LOGIN : Form
    {
         private LoginController loginController;

        public HISTORY_LOGIN(ApplicationDbContext context)
        {
            InitializeComponent();
            // Initialize loginController with the passed context
            loginController = new LoginController(context);
            LoadLoginHistory();
        }
        private void LoadLoginHistory()
        {
            lvwUser.Items.Clear();

            // Tambahkan data admin ke ListView secara manual
            var adminItem = new ListViewItem("admin"); // Username
            adminItem.SubItems.Add("admin123"); // Password
            adminItem.SubItems.Add("Admin"); // Name
            adminItem.SubItems.Add("N/A"); // Phone
            adminItem.SubItems.Add("N/A"); // DOB
            adminItem.SubItems.Add(DateTime.Now.ToString()); // Login Time
            lvwUser.Items.Add(adminItem);

            try
            {
                // Fetch users from the controller
                var users = loginController.GetUserData();

                foreach (var user in users)
                {
                    var listViewItem = new ListViewItem(user.Username);
                    listViewItem.SubItems.Add(user.Password);
                    listViewItem.SubItems.Add(user.Name);
                    listViewItem.SubItems.Add(user.Phone);
                    listViewItem.SubItems.Add(user.DOB);
                    listViewItem.SubItems.Add(DateTime.Now.ToString()); // Example login time
                    lvwUser.Items.Add(listViewItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading login history: {ex.Message}", "Error");
            }
          
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
          
        }


        private void btnHapusHistory_Click(object sender, EventArgs e)
        {
            if (lvwUser.SelectedItems.Count > 0) // Periksa apakah ada item yang dipilih
            {
                // Ambil data dari file history
                var history = HistoryLogger.LoadHistory();

                // Loop melalui setiap item yang dipilih di ListView
                foreach (ListViewItem selectedItem in lvwUser.SelectedItems)
                {
                    string username = selectedItem.SubItems[0].Text;
                    string password = selectedItem.SubItems[1].Text;
                    string name = selectedItem.SubItems[2].Text;
                    string phone = selectedItem.SubItems[3].Text;
                    string dob = selectedItem.SubItems[4].Text;
                    // Hapus user melalui controller
                    if (loginController.DeleteUser(username))
                    {
                        lvwUser.Items.Remove(selectedItem); // Hapus dari ListView
                    }
                    else
                    {
                        MessageBox.Show($"Failed to delete user {username}.", "Error");
                    }
                    // Cari dan hapus data dari list history
                    history.RemoveAll(entry =>
                        entry.Length >= 6 &&
                        entry[0] == username &&
                        entry[1] == password &&
                        entry[2] == name &&
                        entry[3] == phone &&
                        entry[4] == dob
                    );
                }

             MessageBox.Show("Please select an item to delete.", "Warning");
            }
        }

        private void HISTORY_LOGIN_Load(object sender, EventArgs e)
        {
            // Atur ListView ke mode Details dan tambahkan kolom
            lvwUser.View = View.Details;
            lvwUser.FullRowSelect = true;

            lvwUser.Columns.Add("Username", 100, HorizontalAlignment.Left);
            lvwUser.Columns.Add("Password", 100, HorizontalAlignment.Left);
            lvwUser.Columns.Add("Name", 150, HorizontalAlignment.Left);
            lvwUser.Columns.Add("Phone", 100, HorizontalAlignment.Left);
            lvwUser.Columns.Add("DOB", 100, HorizontalAlignment.Left);
            lvwUser.Columns.Add("Login Time", 150, HorizontalAlignment.Left);

            LoadLoginHistory(); // Memuat riwayat login
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            // Pastikan ada item yang dipilih
            if (lvwUser.SelectedItems.Count > 0)
            {
                var selectedItem = lvwUser.SelectedItems[0];

                // Ambil data dari item yang dipilih
                string username = selectedItem.SubItems[0].Text;
                string password = selectedItem.SubItems[1].Text;
                string name = selectedItem.SubItems[2].Text;
                string phone = selectedItem.SubItems[3].Text;
                string dob = selectedItem.SubItems[4].Text;

                // Tampilkan form UpdateAkun dan kirim data pengguna
                LoginController loginController = new LoginController(new ApplicationDbContext());

                // Kirim loginController ke konstruktor
                UPDATE_AKUN updateForm = new UPDATE_AKUN(username, password, name, phone, dob, loginController);

                if (updateForm.ShowDialog() == DialogResult.OK)
                {
                    // Jika data berhasil diperbarui, refresh ListView
                    LoadLoginHistory();
                }
            }
            else
            {
                MessageBox.Show("Please select an account to update.", "Warning");
            }
        }

        private void lvwUser_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
