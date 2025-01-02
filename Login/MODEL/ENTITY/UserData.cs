using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    public static class UserData
    {
        public static Dictionary<string, (string Password, string Name, string Phone, string DOB)> users = new Dictionary<string, (string, string, string, string)>();

        public static string AdminUsername = "admin";
        public static string AdminPassword = "admin123";

        public static void LoadUserData()
        {
            if (File.Exists("users.txt"))
            {
                foreach (var line in File.ReadAllLines("users.txt"))
                {
                    var parts = line.Split(':');
                    if (parts.Length == 5)
                    {
                        users[parts[0]] = (parts[1], parts[2], parts[3], parts[4]);
                    }
                }
            }
        }

        public static void SaveUserData()
        {
            using (StreamWriter writer = new StreamWriter("users.txt"))
            {
                foreach (var user in users)
                {
                    writer.WriteLine($"{user.Key}:{user.Value.Password}:{user.Value.Name}:{user.Value.Phone}:{user.Value.DOB}");
                }
            }
        }
    }
}
