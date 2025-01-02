using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.MODEL
{
    public static class UserData
    {
        public static string AdminUsername { get; set; } = "admin";
        public static string AdminPassword { get; set; } = "admin123";

        public static Dictionary<string, User> users = new Dictionary<string, User>();

        public static void LoadUserData()
        {
            // Tambahkan pengguna default di sini jika diperlukan.
        }
    }

    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string DOB { get; set; }
    }

}
