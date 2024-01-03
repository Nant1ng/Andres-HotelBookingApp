using LibraryAndService.Menu;
using Microsoft.Extensions.Configuration;

namespace HotelBooking
{
    public class App
    {
        public static void Run()
        {
            //var builder = new ConfigurationBuilder()
            //   .AddJsonFile($"appsettings.json", true, true);
            //var config = builder.Build();

            do
            {
                Console.WriteLine(@"
        ██╗  ██╗ ██████╗ ████████╗███████╗██╗         ███████╗ █████╗ ███╗   ██╗████████╗ █████╗ ███╗   ██╗ █████╗ 
        ██║  ██║██╔═══██╗╚══██╔══╝██╔════╝██║         ██╔════╝██╔══██╗████╗  ██║╚══██╔══╝██╔══██╗████╗  ██║██╔══██╗
        ███████║██║   ██║   ██║   █████╗  ██║         ███████╗███████║██╔██╗ ██║   ██║   ███████║██╔██╗ ██║███████║
        ██╔══██║██║   ██║   ██║   ██╔══╝  ██║         ╚════██║██╔══██║██║╚██╗██║   ██║   ██╔══██║██║╚██╗██║██╔══██║
        ██║  ██║╚██████╔╝   ██║   ███████╗███████╗    ███████║██║  ██║██║ ╚████║   ██║   ██║  ██║██║ ╚████║██║  ██║
        ╚═╝  ╚═╝ ╚═════╝    ╚═╝   ╚══════╝╚══════╝    ╚══════╝╚═╝  ╚═╝╚═╝  ╚═══╝   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝
                                                                                                                   
                                                    [1] Booking

                                                    [2] Guest

                                                    [3] Room

                                                    [0] Exit  
                ");

                char key = Console.ReadKey().KeyChar;
                Console.Clear();

                Booking booking = new Booking();
                Guest guest = new Guest();
                Room room = new Room();

                switch (key)
                {
                    case '1':
                        booking.Menu();
                        break;

                    case '2':
                        guest.Menu();
                        break;

                    case '3':
                        room.Menu();
                        break;

                    case '0':
                        Console.WriteLine("Exiting...");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid selection. Please choose a valid option.");
                        break;
                }
            } while (true);
        }
    }
}
