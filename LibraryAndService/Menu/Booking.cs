using LibraryAndService.Data;
using LibraryAndService.Managers;
using Microsoft.EntityFrameworkCore;

namespace LibraryAndService.Menu
{
    public class Booking
    {
        public void Menu(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            bool isRunning = true;

            do
            {
                Console.WriteLine(@"
                                ██████╗  ██████╗  ██████╗ ██╗  ██╗██╗███╗   ██╗ ██████╗ 
                                ██╔══██╗██╔═══██╗██╔═══██╗██║ ██╔╝██║████╗  ██║██╔════╝ 
                                ██████╔╝██║   ██║██║   ██║█████╔╝ ██║██╔██╗ ██║██║  ███╗
                                ██╔══██╗██║   ██║██║   ██║██╔═██╗ ██║██║╚██╗██║██║   ██║
                                ██████╔╝╚██████╔╝╚██████╔╝██║  ██╗██║██║ ╚████║╚██████╔╝
                                ╚═════╝  ╚═════╝  ╚═════╝ ╚═╝  ╚═╝╚═╝╚═╝  ╚═══╝ ╚═════╝ 
                                                        
                                                    [1] Create a Booking

                                                    [2] Get all Bookings

                                                    [3] Get a Booking

                                                    [4] Update a Booking

                                                    [5] Delete a Booking

                                                    [0] Go Back 
                ");

                char key = Console.ReadKey().KeyChar;
                Console.Clear();

                BookingManager bookingManager = new BookingManager();

                switch (key)
                {
                    case '1':
                        bookingManager.Create(options);
                        break;

                    case '2':
                        bookingManager.GetAll(options);
                        break;

                    case '3':
                        bookingManager.GetOne(options);
                        break;

                    case '4':
                        bookingManager.Update(options);
                        break;

                    case '5':
                        bookingManager.Delete(options);
                        break;

                    case '0':
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Invalid selection. Please choose a valid option.");
                        break;
                }
            } while (isRunning);
        }
    }
}
