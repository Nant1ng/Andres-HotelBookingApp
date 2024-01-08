using LibraryAndService.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryAndService
{
    /// <summary>
    /// The CheckInvoiceDeadline method deactivates bookings and their invoices when the current date surpasses unpaid invoice deadlines. 
    /// It identifies such bookings, sets them and their invoices as inactive, and saves these updates to the database, ensuring data integrity based on payment deadlines.
    /// </summary>
    public class UpdateBookingAndInvoiceStatus
    {
        public static void CheckInvoiceDeadline(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext(options.Options))
            {
                DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

                var bookingsToUpdate = dbContext.Booking
                                                .Include(b => b.Invoice)
                                                .Where(b => b.IsActive && b.Invoice != null && !b.Invoice.IsPayed && currentDate > b.Invoice.Deadline)
                                                .ToList();

                foreach (var booking in bookingsToUpdate)
                {
                    booking.IsActive = false;
                    booking.Invoice.IsActive = false;
                }

                dbContext.SaveChanges();
            }
        }
    }
}
