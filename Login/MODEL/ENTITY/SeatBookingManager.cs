using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    public static class SeatBookingManager
    {
        // Dictionary to store booked seats for each movie and showtime
        private static Dictionary<string, Dictionary<string, SeatBooking>> seatBookings = new Dictionary<string, Dictionary<string, SeatBooking>>();

        // Get or create a SeatBooking instance for a specific movie and showtime
        public static SeatBooking GetSeatBooking(string movieTitle, string showTime)
        {
            if (!seatBookings.ContainsKey(movieTitle))
            {
                seatBookings[movieTitle] = new Dictionary<string, SeatBooking>();
            }

            if (!seatBookings[movieTitle].ContainsKey(showTime))
            {
                seatBookings[movieTitle][showTime] = new SeatBooking(movieTitle, showTime);
            }

            return seatBookings[movieTitle][showTime];
        }
    }

}
