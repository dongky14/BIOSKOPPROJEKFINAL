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

namespace Login
{
    public partial class UPDATE_AKUN : Form
    {

        private string originalUsername;

        public UPDATE_AKUN(string username, string password, string name, string phone, string dob)
        {
            InitializeComponent();

            // Set data ke TextBox
            txtUsername.Text = username;
            txtPassword.Text = password;
            txtName.Text = name;
            txtPhone.Text = phone;
            txtDOB.Text = dob;

            // Simpan username asli untuk pencocokan data saat update
            originalUsername = username;
        }



        private void UPDATE_AKUN_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            try
            {
                var history = HistoryLogger.LoadHistory();

                // Update data
                bool dataUpdated = false;
                foreach (var entry in history)
                {
                    if (entry[0] == originalUsername) // Cocokkan username asli
                    {
                        entry[0] = txtUsername.Text;    // Update username
                        entry[1] = txtPassword.Text;   // Update password
                        entry[2] = txtName.Text;       // Update name
                        entry[3] = txtPhone.Text;      // Update phone
                        entry[4] = txtDOB.Text;        // Update DOB
                        dataUpdated = true;
                        break;
                    }
                }

                if (dataUpdated)
                {
                    // Simpan perubahan
                    HistoryLogger.SaveHistory(history);

                    // Update data admin jika username/password admin diubah
                    if (originalUsername == UserData.AdminUsername)
                    {
                        UserData.AdminUsername = txtUsername.Text;
                        UserData.AdminPassword = txtPassword.Text;
                    }

                    // Tampilkan pesan berhasil
                    MessageBox.Show("Data berhasil diupdate!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh ListView di form sebelumnya
                    var parentForm = Application.OpenForms["HISTORY_LOGIN"] as HISTORY_LOGIN;
                    parentForm?.LoadLoginHistory();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Data tidak ditemukan untuk diupdate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat menyimpan data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}

