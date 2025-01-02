using Login.MODEL;
using Login.MODEL.REPOSITORY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.CONTROLLER
{
    public class LoginController
    {
        private LoginRepository loginRepository;

        public LoginController()
        {
            loginRepository = new LoginRepository();
        }

        // Fungsi untuk validasi login
        public bool ValidateLogin(string username, string password)
        {
            // Memanggil method dari LoginRepository untuk memvalidasi login
            return loginRepository.ValidateLogin(username, password);
        }

        // Fungsi untuk register user baru
        public bool RegisterUser(string username, string password, string name, string phone, string dob)
        {
            // Memanggil method dari LoginRepository untuk register pengguna baru
            return loginRepository.RegisterUser(username, password, name, phone, dob);
        }

        // Fungsi untuk mengambil data pengguna
        public User GetUserData(string username)
        {
            return loginRepository.GetUserData(username);
        }

        // Fungsi untuk update data pengguna
        public bool UpdateUser(string username, string password, string name, string phone, string dob)
        {
            // Memanggil method dari LoginRepository untuk update data pengguna
            return loginRepository.UpdateUser(username, password, name, phone, dob);
        }

        // Fungsi untuk menghapus data pengguna
        public bool DeleteUser(string username)
        {
            // Memanggil method dari LoginRepository untuk menghapus data pengguna
            return loginRepository.DeleteUser(username);
        }
    }
}
