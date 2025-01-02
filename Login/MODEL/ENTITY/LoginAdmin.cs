using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
  public class LoginAdmin
    {
        // Hardcode kredensial admin
        public static string AdminUsername = "admin";
        public static string AdminPassword = "admin123";

        public static string CurrentUser { get; set; }

        public static bool IsAdmin { get; set; } // Variabel statis untuk status admin
    }
}
