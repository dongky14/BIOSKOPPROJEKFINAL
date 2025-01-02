using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Login
{
    public class Film
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Duration { get; set; }
        public Image Poster { get; set; } // Image for the film's poster
        public bool IsUpdated { get; set; }
    }
}
