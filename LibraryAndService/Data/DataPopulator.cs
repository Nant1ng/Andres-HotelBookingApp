using LibraryAndService.Enumeration;
using LibraryAndService.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAndService.Data
{
    public class DataPopulator
    {
        public void MigrateAndPopulate(ApplicationDbContext dbContext)
        {
            dbContext.Database.Migrate();
            PopulateGuests(dbContext);
            PopulateRooms(dbContext);
            dbContext.SaveChanges();

            PopulateBookingAndInvoice(dbContext);
            dbContext.SaveChanges();
        }

        private void PopulateGuests(ApplicationDbContext dbContext)
        {
            if (!dbContext.Guest.Any(g => g.FirstName == "Andrés"))
                dbContext.Guest.Add(new Guest("Andrés", "Santana", "070 738 95 08", "andressantana99@hotmail.se", 100, true));

            if (!dbContext.Guest.Any(g => g.FirstName == "Rickard"))
                dbContext.Guest.Add(new Guest("Rickard", "Rickardson", "070 111 22 33", "rickardrickardson@email.com", 41, true));

            if (!dbContext.Guest.Any(g => g.FirstName == "Rikard"))
                dbContext.Guest.Add(new Guest("Rikard", "Rikardson", "070 111 22 33", "rickardrickardson@email.com", 40, true));

            if (!dbContext.Guest.Any(g => g.FirstName == "Rikkard"))
                dbContext.Guest.Add(new Guest("Rikkard", "Rikkardson", "070 111 22 33", "rikkardrickardson@email.com", null, true));
        }

        private void PopulateRooms(ApplicationDbContext dbContext)
        {
            if (!dbContext.Room.Any(r => r.RoomName == "Andrés VIP Room"))
                dbContext.Room.Add(new Room("Andrés VIP Room", 13377331.00M, RoomType.DoubleRoom, 100, true));

            if (!dbContext.Room.Any(r => r.RoomName == "Rikard VIP Room"))
                dbContext.Room.Add(new Room("Rikard VIP Room", 1.00M, RoomType.SingleRoom, 10, true));

            if (!dbContext.Room.Any(r => r.RoomName == "Standard Room"))
                dbContext.Room.Add(new Room("Standard Room", 100.00M, RoomType.SingleRoom, 16, true));

            if (!dbContext.Room.Any(r => r.RoomName == "Skogås City"))
                dbContext.Room.Add(new Room("Skogås City", 142.33M, RoomType.DoubleRoom, 26, true));
        }

        private void PopulateBookingAndInvoice(ApplicationDbContext dbContext)
        {
            DateOnly checkInDate = new DateOnly(2024, 02, 01);
            DateOnly checkOutDate = new DateOnly(2024, 02, 04);
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            DateOnly deadline = today.AddDays(10);
            Guest? guest = dbContext.Guest.Find(1);
            Room? room = dbContext.Room.Find(1);

            if (guest != null && room != null)
            {
                bool bookingExist = dbContext.Booking.Any(b => b.Id == 1);

                if (!bookingExist)
                {
                    Invoice invoice = new Invoice()
                    {
                        Total = room.Price,
                        Deadline = deadline,
                        IsPayed = false,
                        IsActive = true,
                    };
                    dbContext.Invoice.Add(invoice);
                    dbContext.Booking.Add(new Booking(checkInDate, checkOutDate, 1, AmountOfBed.OneExtraBed, true)
                    {
                        Guest = guest,
                        Room = room,
                        Invoice = invoice
                    });
                }
            }
        }
    }
}
