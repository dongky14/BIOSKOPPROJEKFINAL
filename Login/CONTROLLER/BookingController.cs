using Login.MODEL.REPOSITORY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Login.MODEL.ENTITY;
using Login.MODEL.CONTEXT;
using Login.Model.Repository;
using System.Windows.Forms;

namespace Login.CONTROLLER
{
    public class BookingController
    {
        // Declare repository object to perform CRUD operations
        private BookingRepository _repository;
        Booking booking = new Booking();
        /// <summary>
        /// Method to Add a new booking
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
      public bool Add(string MovieTitle, string Studio, string ShowTime, string Date, string SeatNumber, int TicketCount, double TotalPrice, string PaymentMethod)
{
    try
    {
        // Ensure all required properties are set
        if (string.IsNullOrEmpty(MovieTitle) || string.IsNullOrEmpty(Studio) ||
            string.IsNullOrEmpty(ShowTime) || string.IsNullOrEmpty(Date) ||
            string.IsNullOrEmpty(SeatNumber) || string.IsNullOrEmpty(PaymentMethod))
        {
            MessageBox.Show("Invalid booking data!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return false;
        }

        // Prepare booking object
        Booking booking = new Booking
        {
            MovieTitle = MovieTitle,
            Studio = Studio,
            ShowTime = ShowTime,
            Date = Date,
            SeatNumber = SeatNumber,
            TicketCount = TicketCount,
            TotalPrice = TotalPrice
        };

        // Call repository to add booking to the database
        using (ApplicationDbContext context = new ApplicationDbContext())
        {
            _repository = new BookingRepository(context);
            int result = _repository.Add(booking);  // Call Add method from repository

            if (result > 0)
            {
                MessageBox.Show("Booking has been successfully added!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
            {
                MessageBox.Show("Failed to add booking!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
    }
}


        /// <summary>
        /// Method to Get all bookings
        /// </summary>
        /// <returns></returns>
        public List<Booking> GetAll()
        {
            List<Booking> list = new List<Booking>();

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                // Create repository object
                _repository = new BookingRepository(context);

                // Call ReadAll method from the repository
                list = _repository.ReadAll();
            }

            return list;
        }

        /// <summary>
        /// Method to Remove a booking by its ID
        /// </summary>
        /// <param name="bookingId"></param>
        /// <returns></returns>
        public int Remove(int bookingId)
        {
            int result = 0;

            // Check that bookingId is valid
            if (bookingId <= 0)
            {
                MessageBox.Show("Invalid Booking ID!", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                // Create repository object
                _repository = new BookingRepository(context);

                // Call Remove method from the repository
                result = _repository.Delete(bookingId);
            }

            if (result > 0)
            {
                MessageBox.Show("Booking has been successfully deleted!", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to delete booking!", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return result;
        }
    }
}
