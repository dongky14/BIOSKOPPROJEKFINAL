using Login.CONTROLLER;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Login.MODEL.CONTEXT;

using System.Data.SQLite;

namespace Login
{
    public partial class REGISTER : Form
    {
        private LoginController loginController;

        // Constructor that accepts ApplicationDbContext
        public REGISTER(ApplicationDbContext context)
        {
            InitializeComponent();
            textBox4.PasswordChar = '*';

            // Initialize loginController with the passed context
            loginController = new LoginController(context);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Username = textBox3.Text; // Username
            string Password = textBox4.Text; // Password
            string Name = textBox1.Text + " " + textBox2.Text; // Nama (FirstName + LastName)
            string Phone = textBox5.Text; // Telepon
            string DOB = dateTimePicker1.Value.ToString("yyyy-MM-dd"); // Tanggal Lahir

            // Validasi input
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Username and Password cannot be empty!", "Error");
                return;
            }  // Use LoginController to register user
            bool registrationSuccess = loginController.RegisterUser(Username, Password, Name, Phone, DOB);

            if (registrationSuccess)
            {
                MessageBox.Show("Registration successful!", "Success");
                Login loginForm = new Login();
                loginForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username already exists or other error.", "Error");
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
    }
}