using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.MODEL
{

    public class User
    {
   
            public int ID { get; set; }  // Ensure this property exists
            public string Username { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public string DOB { get; set; }
        

    }

}
