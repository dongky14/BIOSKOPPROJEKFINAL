using Login.MODEL.ENTITY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Login.VIEW;
using Login.CONTROLLER;
using System.Data.SQLite;

namespace Login
{
    public partial class UPDATE_AKUN : Form
    {
     
        private string originalUsername; // Untuk menyimpan username asli
        private LoginController loginController;

        public UPDATE_AKUN(string username, string password, string name, string phone, string DOB, LoginController loginController)
        {
            InitializeComponent();

            // Set instance LoginController
            this.loginController = loginController;

            // Isi kontrol dengan data yang diterima
            originalUsername = username;
            txtUsername.Text = username;
            txtPassword.Text = password;
            txtName.Text = name;
            txtPhone.Text = phone;
            txtDOB.Text = DOB;


        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            // Ambil data baru dari kontrol
            string newUsername = txtUsername.Text;
            string newPassword = txtPassword.Text;
            string newName = txtName.Text;
            string newPhone = txtPhone.Text;
            string newDOB= txtDOB.Text;

            // Validasi input
            if (string.IsNullOrEmpty(newUsername) || string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Username and Password cannot be empty!", "Error");
                return;
            }// Panggil metode UpdateUser di LoginController
            bool success = loginController.UpdateUser(newUsername, newPassword, newName, newPhone, newDOB);

            if (success)
            {
                MessageBox.Show("Account updated successfully!", "Success");
                this.DialogResult = DialogResult.OK; // Beri tanda bahwa data berhasil diperbarui
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to update account.", "Error");
            }

            // Perbarui data di file riwayat
            var history = HistoryLogger.LoadHistory();

            for (int i = 0; i < history.Count; i++)
            {
                if (history[i][0] == originalUsername) // Cari berdasarkan username asli
                {
                    // Perbarui data
                    history[i] = new string[] { newUsername, newPassword, newName, newPhone, newDOB, DateTime.Now.ToString() };
                    break;
                }
            }

            // Simpan kembali ke file
            HistoryLogger.SaveHistory(history);

            MessageBox.Show("Account updated successfully!", "Success");
            this.DialogResult = DialogResult.OK; // Beri tanda bahwa data berhasil diperbarui
            this.Close();
        }


    }
}

