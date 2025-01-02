using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class Login : Form
    {
        // Dictionary untuk menyimpan user yang telah terdaftar
        public static Dictionary<string, string> users = new Dictionary<string, string>();

        // Tambahkan admin sebagai default user
        private const string adminUsername = "admin";
        private const string adminPassword = "admin123";


        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Memuat data pengguna saat form login dibuka
            UserData.LoadUserData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (username == UserData.AdminUsername && password == UserData.AdminPassword)
            {
                MessageBox.Show("Login as Admin successful!", "Success");
                Menu menuForm = new Menu(true);
                menuForm.Show();
                this.Hide();
            }
            else if (UserData.users.ContainsKey(username) && UserData.users[username].Password == password)
            {
                MessageBox.Show("Login successful!", "Success");

                // Add user data to history
                var userData = UserData.users[username];
                HistoryLogger.AddToHistory(username, userData.Password, userData.Name, userData.Phone, userData.DOB);

                Menu menuForm = new Menu(false);
                menuForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password!", "Error");
            }
        
    }
        public static class HistoryLogger
        {
            private static string historyFilePath = "login_history.txt";

            public static void AddToHistory(string username, string password, string name, string phone, string dob)
            {
                using (StreamWriter writer = new StreamWriter(historyFilePath, true))
                {
                    writer.WriteLine($"{username}:{password}:{name}:{phone}:{dob}:{DateTime.Now}");
                }
            }

            public static List<string[]> LoadHistory()
            {
                var history = new List<string[]>();
                if (File.Exists(historyFilePath))
                {
                    foreach (var line in File.ReadAllLines(historyFilePath))
                    {
                        history.Add(line.Split(':'));
                    }
                }
                return history;
            }
        }
     

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Tampilkan atau sembunyikan password berdasarkan status checkbox
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0'; // Tampilkan teks password
            }
            else
            {
                textBox2.PasswordChar = '*'; // Sembunyikan password
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }



        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            REGISTER registerForm = new REGISTER();
            registerForm.Show();
            this.Hide(); // Sembunyikan form Login
        }
    }
}
