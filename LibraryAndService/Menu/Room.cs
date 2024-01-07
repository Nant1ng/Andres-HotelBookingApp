using LibraryAndService.Data;
using LibraryAndService.Managers;
using Microsoft.EntityFrameworkCore;

namespace LibraryAndService.Menu
{
    public class Room
    {
        public void Menu(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            bool isRunning = true;

            do
            {
                Console.WriteLine(@"
                                        ██████╗  ██████╗  ██████╗ ███╗   ███╗
                                        ██╔══██╗██╔═══██╗██╔═══██╗████╗ ████║
                                        ██████╔╝██║   ██║██║   ██║██╔████╔██║
                                        ██╔══██╗██║   ██║██║   ██║██║╚██╔╝██║
                                        ██║  ██║╚██████╔╝╚██████╔╝██║ ╚═╝ ██║
                                        ╚═╝  ╚═╝ ╚═════╝  ╚═════╝ ╚═╝     ╚═╝
                                     
                                                        
                                                    [1] Create a Room

                                                    [2] Get all Rooms

                                                    [3] Get a Room

                                                    [4] Update a Room

                                                    [5] Delete a Room

                                                    [6] Recover a Room

                                                    [7] Hard Delete a Room

                                                    [0] Go Back
                ");

                char key = Console.ReadKey().KeyChar;
                Console.Clear();

                RoomManager roomManager = new RoomManager();

                switch (key)
                {
                    case '1':
                        roomManager.Create(options);
                        break;

                    case '2':
                        roomManager.GetAll(options);
                        break;

                    case '3':
                        roomManager.GetOne(options);
                        break;

                    case '4':
                        roomManager.Update(options);
                        break;

                    case '5':
                        roomManager.Delete(options);
                        break;

                    case '6':
                        roomManager.Recover(options);
                        break;

                    case '7':
                        roomManager.HardDelete(options);
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
