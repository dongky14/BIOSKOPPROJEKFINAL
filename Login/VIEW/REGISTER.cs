using Login.VIEW;
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
    public partial class REGISTER : Form
    {
        public REGISTER()
        {
            InitializeComponent();
            textBox4.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox3.Text; // Username
            string password = textBox4.Text; // Password
            string name = textBox1.Text + " " + textBox2.Text; // Nama (FirstName + LastName)
            string phone = textBox5.Text; // Telepon
            string TTL = dateTimePicker1.Value.ToString("yyyy-MM-dd"); // Tanggal Lahir

            // Validasi input
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username and Password cannot be empty!", "Error");
                return;
            }

            // Periksa apakah username sudah terdaftar
            if (UserData.users.ContainsKey(username))
            {
                MessageBox.Show("Username already exists!", "Error");
            }
            else
            {
                // Tambahkan pengguna ke dictionary UserData
                UserData.users.Add(username, (password, name, phone, TTL));
                UserData.SaveUserData(); // Simpan data pengguna

                // Tambahkan data pengguna baru ke HISTORY_LOGIN jika form tersebut terbuka
                var historyLoginForm = Application.OpenForms["HISTORY_LOGIN"] as HISTORY_LOGIN;
                if (historyLoginForm != null)
                {
                    // Tambahkan pengguna ke ListView di HISTORY_LOGIN
                    historyLoginForm.AddNewUserToListView(username, password, name, phone, TTL);
                }

                // Tampilkan pesan sukses
                MessageBox.Show("Registration successful!", "Success");

                // Tampilkan form Login dan sembunyikan Register
                Login loginForm = new Login();
                loginForm.Show();
                this.Hide();
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Tampilkan atau sembunyikan password berdasarkan status checkbox
            if (checkBox1.Checked)
            {
                textBox4.PasswordChar = '\0'; // Tampilkan teks password
            }
            else
            {
                textBox4.PasswordChar = '*'; // Sembunyikan password
            }
        }

        private void REGISTER_Load(object sender, EventArgs e)
        {

        }
    }
}
