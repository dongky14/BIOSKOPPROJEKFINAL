using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.MODEL.ENTITY
{
    public class Booking
    {
        public int Id { get; set; } // Booking ID
        public string Username { get; set; }
        public string MovieTitle { get; set; } // Title of the movie
        public string Studio { get; set; } // Studio where the movie is shown
        public string ShowTime { get; set; } // Showtime of the movie
        public string Date { get; set; } // Date of the booking
        public string PaymentMethod { get; set; } // Date of the booking
        public string SeatNumber { get; set; } // Selected seat number(s)
        public int TicketCount { get; set; } // Number of tickets booked
        public double TotalPrice { get; set; } // Total price of the booking

        // Constructor for creating a new booking entity
    

        // Default constructor (if needed)
        public Booking() { }
    }
}
