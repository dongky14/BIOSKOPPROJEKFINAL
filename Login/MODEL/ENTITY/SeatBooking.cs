using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    public class SeatBooking
    {
        public string MovieTitle { get; set; }
        public string ShowTime { get; set; }
        public List<string> BookedSeats { get; set; }

        public SeatBooking(string movieTitle, string showTime)
        {
            MovieTitle = movieTitle;
            ShowTime = showTime;
            BookedSeats = new List<string>();
        }

        // Add a seat to the booked list
        public void BookSeat(string seatNumber)
        {
            if (!BookedSeats.Contains(seatNumber))
            {
                BookedSeats.Add(seatNumber);
            }
        }

        // Check if a seat is already booked
        public bool IsSeatBooked(string seatNumber)
        {
            return BookedSeats.Contains(seatNumber);
        }

        // Remove a seat from the booked list
        public void CancelSeat(string seatNumber)
        {
            BookedSeats.Remove(seatNumber);
        }
    }


}
