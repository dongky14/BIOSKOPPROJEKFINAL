using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using Login.MODEL.CONTEXT;
using Login.MODEL.ENTITY;
using System.Data.Common;
using System.Drawing;

namespace Login.Model.Repository
{
    public class BookingRepository
    {
        // Declare the connection object
        private SQLiteConnection _conn;

        // Constructor that accepts DbContext (ApplicationDbContext)
        public BookingRepository(ApplicationDbContext context)
        {
            // Initialize the connection object
            _conn = context.Conn;
        }

        // Method to Add a new Booking
        public int Add(Booking booking)
        {
            int result = 0;

            // Declare SQL query for inserting a booking record
            string sql = @"INSERT INTO Booking (MovieTitle, Studio, ShowTime, Date, SeatNumber, TicketCount, TotalPrice, PaymentMethod)
                           VALUES (@MovieTitle, @Studio, @ShowTime, @Date, @SeatNumber, @TicketCount, @TotalPrice, @PaymentMethod)";

            // Create the command object inside a using block
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                // Register parameters and set their values
                cmd.Parameters.AddWithValue("@MovieTitle", booking.MovieTitle);
                cmd.Parameters.AddWithValue("@Studio", booking.Studio);
                cmd.Parameters.AddWithValue("@ShowTime", booking.ShowTime);
                cmd.Parameters.AddWithValue("@Date", booking.Date);
                cmd.Parameters.AddWithValue("@SeatNumber", booking.SeatNumber);
                cmd.Parameters.AddWithValue("@TicketCount", booking.TicketCount);
                cmd.Parameters.AddWithValue("@TotalPrice", booking.TotalPrice);
                cmd.Parameters.AddWithValue("@PaymentMethod", booking.PaymentMethod);


                try
                {
                    // Execute the INSERT command and store the result
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Add error: {0}", ex.Message);
                }
            }

            return result;
        }

        // Method to Delete a Booking by its ID
        public int Delete(int bookingId)
        {
            int result = 0;

            // Declare SQL query for deleting a booking
            string sql = @"DELETE FROM Booking WHERE Id = @bookingId";

            // Create the command object inside a using block
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                // Register the parameter and set its value
                cmd.Parameters.AddWithValue("@bookingId", bookingId);

                try
                {
                    // Execute the DELETE command and store the result
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Delete error: {0}", ex.Message);
                }
            }

            return result;
        }

        // Method to Read all Bookings
        public List<Booking> ReadAll()
        {
            List<Booking> bookings = new List<Booking>();

            try
            {
                // Declare SQL query for selecting all bookings
                string sql = @"SELECT Id, MovieTitle, Studio, ShowTime, Date, SeatNumber, TicketCount, TotalPrice
                               FROM Booking
                               ORDER BY MovieTitle";

                // Create the command object inside a using block
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    // Create the data reader inside a using block
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        // Loop through each record in the result set
                        while (dtr.Read())
                        {
                            // Convert the result set row to a Booking object
                            Booking booking = new Booking
                            {
                                Id = Convert.ToInt32(dtr["Id"]),
                                MovieTitle = dtr["MovieTitle"].ToString(),
                                Studio = dtr["Studio"].ToString(),
                                ShowTime = dtr["ShowTime"].ToString(),
                                Date = dtr["Date"].ToString(),
                                SeatNumber = dtr["SeatNumber"].ToString(),
                                TicketCount = Convert.ToInt32(dtr["TicketCount"]),
                                TotalPrice = Convert.ToDouble(dtr["TotalPrice"])
                            };

                            // Add the booking object to the list
                            bookings.Add(booking);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadAll error: {0}", ex.Message);
            }

            return bookings;
        }

        // Method to Read a Booking by its ID
        public Booking ReadById(int bookingId)
        {
            Booking booking = null;

            try
            {
                // Declare SQL query for selecting a booking by its ID
                string sql = @"SELECT Id, MovieTitle, Studio, ShowTime, Date, SeatNumber, TicketCount, TotalPrice
                               FROM Booking WHERE Id = @bookingId";

                // Create the command object inside a using block
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    // Register the parameter and set its value
                    cmd.Parameters.AddWithValue("@bookingId", bookingId);

                    // Create the data reader inside a using block
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        // Check if a record is returned
                        if (dtr.Read())
                        {
                            // Convert the result set row to a Booking object
                            booking = new Booking
                            {
                                Id = Convert.ToInt32(dtr["Id"]),
                                MovieTitle = dtr["MovieTitle"].ToString(),
                                Studio = dtr["Studio"].ToString(),
                                ShowTime = dtr["ShowTime"].ToString(),
                                Date = dtr["Date"].ToString(),
                                SeatNumber = dtr["SeatNumber"].ToString(),
                                TicketCount = Convert.ToInt32(dtr["TicketCount"]),
                                TotalPrice = Convert.ToDouble(dtr["TotalPrice"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadById error: {0}", ex.Message);
            }

            return booking;
        }
    }
}
