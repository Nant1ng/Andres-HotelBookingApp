using LibraryAndService.Data;
using LibraryAndService.Enumeration;
using LibraryAndService.Interface;
using LibraryAndService.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAndService.Managers
{
    public class BookingManager : IEntityManager
    {
        public void Create(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext(options.Options))
            {
                bool isRunning = true;
                do
                {
                    Console.WriteLine("Create a Booking.");
                    Console.WriteLine();

                    foreach (Guest guest in dbContext.Guest
                        .Where(g => !dbContext.Booking
                        .Any(b => b.Guest.Id == g.Id && b.IsActive)))
                    {
                        Console.WriteLine($"Id: {guest.Id}, Full Name: {guest.FirstName} {guest.LastName}, Phone Number: {guest.PhoneNumber}");
                    }

                    Console.WriteLine();
                    Console.Write("Enter Guest Id: ");

                    if (int.TryParse(Console.ReadLine(), out int guestId))
                    {
                        Guest? guest = dbContext.Guest
                            .Find(guestId);

                        if (guest != null)
                        {
                            Booking? existingBooking = dbContext.Booking
                                .FirstOrDefault(b => b.Guest.Id == guestId && b.IsActive);

                            if (existingBooking == null)
                            {
                                Console.Write("Enter Check-in Date (yyyy-mm-dd): ");

                                if (DateOnly.TryParse(Console.ReadLine(), out DateOnly checkInDate))
                                {
                                    Console.Write("Enter Check-out Date (yyyy-mm-dd): ");

                                    if (DateOnly.TryParse(Console.ReadLine(), out DateOnly checkOutDate) || checkOutDate >= checkInDate)
                                    {
                                        Console.Write("Enter the number of guests (1-4): ");

                                        if (byte.TryParse(Console.ReadLine(), out byte amountOfGuests) || amountOfGuests > 0 || amountOfGuests < 5)
                                        {
                                            List<Room> availableRooms = dbContext.Room
                                                .Where(room => (amountOfGuests <= 2
                                                    && room.IsActive
                                                    && (room.RoomType == RoomType.SingleRoom || room.RoomType == RoomType.DoubleRoom))
                                                    || (amountOfGuests > 2 && room.RoomType == RoomType.DoubleRoom))
                                                .Where(room => !dbContext.Booking
                                                .Any(b => b.Room.Id == room.Id
                                                    && b.IsActive
                                                    && ((checkInDate < b.EndDate && checkInDate >= b.StartDate)
                                                    || (checkOutDate > b.StartDate && checkOutDate <= b.EndDate))))
                                                .ToList();

                                            if (availableRooms.Any())
                                            {
                                                Console.WriteLine("Available Rooms.");

                                                foreach (Room room in availableRooms)
                                                {
                                                    Console.WriteLine($"Id: {room.Id}, Name: {room.RoomName}");
                                                }

                                                Console.WriteLine();
                                                Console.Write("Enter a Room Id: ");

                                                if (int.TryParse(Console.ReadLine(), out int roomId))
                                                {
                                                    Room? roomToBook = dbContext.Room.Find(roomId);

                                                    bool isRoomAvailable = availableRooms.Any(r => r.Id == roomId) && !dbContext.Booking
                                                        .Any(b => b.Room.Id == roomId && b.IsActive
                                                    && ((checkInDate < b.EndDate && checkInDate >= b.StartDate)
                                                    || (checkOutDate > b.StartDate && checkOutDate <= b.EndDate)));

                                                    if (roomToBook != null && isRoomAvailable)
                                                    {
                                                        AmountOfBed extraBed = AmountOfBed.None;

                                                        if (roomToBook.RoomSize > 20)
                                                        {
                                                            Console.WriteLine("Do you want an extra bed? (None, OneExtraBed, TwoExtraBeds): ");
                                                            if (Enum.TryParse(Console.ReadLine(), out AmountOfBed extraBedChoice))
                                                            {
                                                                extraBed = extraBedChoice;
                                                            }
                                                        }

                                                        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
                                                        DateOnly deadline = today.AddDays(10);

                                                        Invoice invoice = new Invoice
                                                        {
                                                            Total = roomToBook.Price,
                                                            Deadline = deadline,
                                                            IsPayed = false,
                                                            IsActive = true,
                                                        };
                                                        dbContext.Invoice.Add(invoice);

                                                        Booking booking = new Booking(checkInDate, checkOutDate, amountOfGuests, extraBed, true)
                                                        {
                                                            Guest = guest,
                                                            Room = roomToBook,
                                                            Invoice = invoice
                                                        };

                                                        dbContext.Booking.Add(booking);
                                                        dbContext.SaveChanges();

                                                        Console.ForegroundColor = ConsoleColor.Green;
                                                        Console.WriteLine($"Booking and Invoice created successfully for Guest ID {guestId}.");
                                                        Console.ResetColor();

                                                        isRunning = false;

                                                        Console.WriteLine();
                                                        Console.WriteLine("Press any key to go back.");
                                                        Console.ReadKey();
                                                        Console.Clear();
                                                    }
                                                    else
                                                    {
                                                        Console.Clear();
                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                        Console.WriteLine($"Room with Id {roomId} is not available for the selected dates.");
                                                        Console.ResetColor();
                                                    }
                                                }
                                                else
                                                {
                                                    Console.Clear();
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("No available rooms for the selected number of guests and dates.");
                                                    Console.ResetColor();
                                                }
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("No available rooms for the selected dates.");
                                                Console.ResetColor();
                                            }
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Invalid input for the number of guests. It must be between 1 and 4.");
                                            Console.ResetColor();
                                        }
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid input for Check-out Date.");
                                        Console.ResetColor();
                                    }
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Invalid input for Check-in Date.");
                                    Console.ResetColor();
                                }
                            }
                            else
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Guest with Id {guestId} already has an active booking.");
                                Console.ResetColor();
                            }

                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Guest with Id {guestId} was not found");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input for Guest Id.");
                        Console.ResetColor();
                    }
                } while (isRunning);
            }
        }
        public void GetOne(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext(options.Options))
            {
                bool isRunning = true;
                do
                {
                    Console.WriteLine("Get a Booking.");

                    foreach (Booking booking in dbContext.Booking
                        .Include(b => b.Guest)
                        .Include(b => b.Room)
                        .Include(b => b.Invoice))
                    {
                        Console.WriteLine($"Id: {booking.Id}, Check In Date: {booking.StartDate}, Check Out Date: {booking.EndDate}, Active: {booking.IsActive}, Guest Id: {booking.Guest.Id}, Room Id: {booking.Room.Id}, Invoice Id: {booking.Invoice.Id}");
                    }
                    Console.WriteLine();

                    Console.Write("Enter a Booking Id: ");
                    if (int.TryParse(Console.ReadLine(), out int bookingId))
                    {
                        Booking? booking = dbContext.Booking
                            .Include(b => b.Guest)
                            .Include(b => b.Room)
                            .Include(b => b.Invoice).FirstOrDefault(b => b.Id == bookingId);

                        if (booking != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Booking found Successfully.");
                            Console.ResetColor();
                            Console.WriteLine($"Booking Found: Id: {booking.Id}, Check In Date: {booking.StartDate}, Check Out Date: {booking.EndDate}, Amount of Guest: {booking.AmountOfGuest}, Amount of Extra Bed: {booking.ExtraBed}, Guest Id: {booking.Guest.Id}, Room Id: {booking.Room.Id}, Invoice Id: {booking.Invoice.Id}");

                            isRunning = false;

                            Console.WriteLine();
                            Console.WriteLine("Press any key to go back.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"No Booking found with Id {bookingId}.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please enter a valid Booking Id.");
                        Console.ResetColor();
                    }

                } while (isRunning);
            }
        }
        public void GetAll(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext(options.Options))
            {
                Console.WriteLine("Get all Bookings");
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║  Id  | Check In Date | Check Out Date | Amount Of Guest | ExtraBed | Active | GuestId | RoomId | InvoiceId ║");
                Console.WriteLine("║—————————————————————————————————————————————————————————————————————---------------------------------------║");

                foreach (Booking booking in dbContext.Booking
                    .Include(b => b.Guest)
                    .Include(b => b.Room)
                    .Include(b => b.Invoice))
                {
                    Console.WriteLine($"║ {booking.Id} | {booking.StartDate} | {booking.EndDate} | {booking.AmountOfGuest} | {booking.ExtraBed} | {booking.IsActive} | {booking.Guest.Id} | {booking.Room.Id} | {booking.Invoice.Id} ║");
                    Console.WriteLine("║------------------------------------------------------------------------------------------------------------║");
                }
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
                Console.WriteLine();
                Console.WriteLine("Press any key to go back.");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void Update(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext(options.Options))
            {
                bool isRunning = true;
                do
                {
                    Console.WriteLine("Update a Booking.");
                    Console.WriteLine("FYI, you can't change the Guest Id on the booking and you can't Update a booking that's Not Active.");
                    foreach (Booking booking in dbContext.Booking
                    .Include(b => b.Guest)
                    .Include(b => b.Room)
                    .Include(b => b.Invoice)
                    .Where(b => b.IsActive == true))
                    {
                        Console.WriteLine($"Id: {booking.Id}, Check In Date: {booking.StartDate}, Check Out Date: {booking.EndDate}, Guest Last Name: {booking.Guest.LastName}; Room Id: {booking.Room.Id}, Invoice Id: {booking.Invoice.Id}");
                    }

                    Console.WriteLine();
                    Console.Write("Enter a Booking Id you want to Update: ");

                    if (int.TryParse(Console.ReadLine(), out int bookingId))
                    {
                        Booking? bookingToUpdate = dbContext.Booking.Find(bookingId);

                        if (bookingToUpdate != null && bookingToUpdate.IsActive == true)
                        {
                            Console.Write("Enter Check-in Date (yyyy-mm-dd): ");

                            if (DateOnly.TryParse(Console.ReadLine(), out DateOnly checkInDateUpdate))
                            {
                                Console.Write("Enter Check-out Date (yyyy-mm-dd): ");

                                if (DateOnly.TryParse(Console.ReadLine(), out DateOnly checkOutDateUpdate) || checkOutDateUpdate >= checkInDateUpdate)
                                {
                                    Console.Write("Enter the number of guests (1-4): ");

                                    if (byte.TryParse(Console.ReadLine(), out byte amountOfGuestsUpdate) || amountOfGuestsUpdate > 0 || amountOfGuestsUpdate < 5)
                                    {
                                        List<Room> availableRooms = dbContext.Room
                                            .Where(room => (amountOfGuestsUpdate <= 2
                                                && room.IsActive
                                                && (room.RoomType == RoomType.SingleRoom || room.RoomType == RoomType.DoubleRoom))
                                                || (amountOfGuestsUpdate > 2 && room.RoomType == RoomType.DoubleRoom))
                                            .Where(room => !dbContext.Booking
                                            .Any(b => b.Room.Id == room.Id
                                                && b.IsActive
                                                && ((checkInDateUpdate < b.EndDate && checkInDateUpdate >= b.StartDate)
                                                || (checkOutDateUpdate > b.StartDate && checkOutDateUpdate <= b.EndDate))))
                                            .ToList();

                                        if (availableRooms.Any())
                                        {
                                            Console.WriteLine("Available Rooms.");

                                            foreach (Room room in availableRooms)
                                            {
                                                Console.WriteLine($"Id: {room.Id}, Name: {room.RoomName}");
                                            }

                                            Console.WriteLine();
                                            Console.Write("Enter a Room Id: ");

                                            if (int.TryParse(Console.ReadLine(), out int roomIdUpdate))
                                            {
                                                Room? roomToBookUpdate = dbContext.Room.Find(roomIdUpdate);

                                                bool isRoomAvailable = availableRooms
                                                    .Any(r => r.Id == roomIdUpdate) && !dbContext.Booking
                                                    .Any(b => b.Room.Id == roomIdUpdate && b.IsActive
                                                    && ((checkInDateUpdate < b.EndDate && checkInDateUpdate >= b.StartDate)
                                                    || (checkOutDateUpdate > b.StartDate && checkOutDateUpdate <= b.EndDate)));

                                                if (roomToBookUpdate != null && isRoomAvailable)
                                                {
                                                    AmountOfBed extraBedUpdate = AmountOfBed.None;

                                                    if (roomToBookUpdate.RoomSize > 20)
                                                    {
                                                        Console.WriteLine("Do you want an extra bed? (None, OneExtraBed, TwoExtraBeds): ");
                                                        if (Enum.TryParse(Console.ReadLine(), out AmountOfBed extraBedChoice))
                                                        {
                                                            extraBedUpdate = extraBedChoice;
                                                        }
                                                    }

                                                    DateOnly today = DateOnly.FromDateTime(DateTime.Now);
                                                    DateOnly newDeadline = today.AddDays(10);

                                                    bookingToUpdate.StartDate = checkInDateUpdate;
                                                    bookingToUpdate.EndDate = checkOutDateUpdate;
                                                    bookingToUpdate.AmountOfGuest = amountOfGuestsUpdate;
                                                    bookingToUpdate.ExtraBed = extraBedUpdate;

                                                    bookingToUpdate.Invoice.Deadline = newDeadline;
                                                    bookingToUpdate.Invoice.Total = roomToBookUpdate.Price;

                                                    dbContext.SaveChanges();

                                                    Console.ForegroundColor = ConsoleColor.Green;
                                                    Console.WriteLine($"Booking and Invoice was successfully Updated.");
                                                    Console.ResetColor();

                                                    isRunning = false;

                                                    Console.WriteLine();
                                                    Console.WriteLine("Press any key to go back.");
                                                    Console.ReadKey();
                                                    Console.Clear();
                                                }
                                                else
                                                {
                                                    Console.Clear();
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine($"Room with Id {roomIdUpdate} is not available for the selected dates.");
                                                    Console.ResetColor();
                                                }
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("No available rooms for the selected number of guests and dates.");
                                                Console.ResetColor();
                                            }
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("No available rooms for the selected dates.");
                                            Console.ResetColor();
                                        }
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid input for the number of guests. It must be between 1 and 4.");
                                        Console.ResetColor();
                                    }
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Invalid input for Check-out Date.");
                                    Console.ResetColor();
                                }
                            }
                            else
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid input for Check-in Date.");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"No Booking found with Id {bookingId}.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please enter a valid Guest Id.");
                        Console.ResetColor();
                    }

                } while (isRunning);
            }
        }
        public void Delete(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext(options.Options))
            {
                bool isRunning = true;
                do
                {
                    Console.WriteLine("Delete a Booking.");

                    foreach (Booking booking in dbContext.Booking.Where(b => b.IsActive)
                        .Include(b => b.Guest)
                        .Include(b => b.Room)
                        .Include(b => b.Invoice))
                    {
                        Console.WriteLine($"Id: {booking.Id}, Check In Date: {booking.StartDate}, Check Out Date: {booking.EndDate}, Active: {booking.IsActive}, Guest Id: {booking.Guest.Id}, Room Id: {booking.Room.Id}, Invoice Id: {booking.Invoice.Id}");
                    }
                    Console.WriteLine();
                    Console.Write("Write the Booking Id of the Booking you want to Delete: ");

                    if (int.TryParse(Console.ReadLine(), out int bookingId))
                    {
                        Booking? bookingToDelete = dbContext.Booking
                            .Include(b => b.Invoice)
                            .Include(b => b.Invoice).FirstOrDefault(b => b.Id == bookingId);

                        if (bookingToDelete != null && bookingToDelete.IsActive == true)
                        {
                            bookingToDelete.Invoice.IsActive = false;
                            bookingToDelete.IsActive = false;
                            dbContext.SaveChanges();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Successfully soft-deleted Booking with Id {bookingId}.");
                            Console.ResetColor();

                            isRunning = false;

                            Console.WriteLine();
                            Console.WriteLine("Press any key to go back.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"No Booking found with Id {bookingId} or it's Not Active.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please enter a valid Invoice Id.");
                        Console.ResetColor();
                    }

                } while (isRunning);

            }

        }
    }
}
