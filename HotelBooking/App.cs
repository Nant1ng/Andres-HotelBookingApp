using LibraryAndService.Data;
using LibraryAndService.Menu;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HotelBooking
{
    public class App
    {
        public static void Run()
        {
            // Boiler Plate Code
            var builder = new ConfigurationBuilder()
               .AddJsonFile($"Appsettings.json", true, true);
            var config = builder.Build();

            DbContextOptionsBuilder<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = config.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);

            using (var dbContext = new ApplicationDbContext(options.Options))
            {
                var dataPopulator = new DataPopulator();
                dataPopulator.MigrateAndPopulate(dbContext);
            }


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
                        booking.Menu(options);
                        break;

                    case '2':
                        guest.Menu(options);
                        break;

                    case '3':
                        room.Menu();
                        break;

                    case '0':
                        Console.WriteLine("Exiting...");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid selection. Please choose a valid option.");
                        Console.ResetColor();
                        break;
                }
            } while (true);
        }
    }
}
