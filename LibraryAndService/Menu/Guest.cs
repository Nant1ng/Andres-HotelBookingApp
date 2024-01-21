using LibraryAndService.Data;
using LibraryAndService.Managers;
using Microsoft.EntityFrameworkCore;

namespace LibraryAndService.Menu
{
    public class Guest
    {
        public void Menu(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            bool isRunning = true;

            do
            {
                Console.WriteLine(@"
                                      ██████╗ ██╗   ██╗███████╗███████╗████████╗
                                     ██╔════╝ ██║   ██║██╔════╝██╔════╝╚══██╔══╝
                                     ██║  ███╗██║   ██║█████╗  ███████╗   ██║   
                                     ██║   ██║██║   ██║██╔══╝  ╚════██║   ██║   
                                     ╚██████╔╝╚██████╔╝███████╗███████║   ██║   
                                      ╚═════╝  ╚═════╝ ╚══════╝╚══════╝   ╚═╝   
                                           
                                                    [1] Create a Guest

                                                    [2] Get all Guests

                                                    [3] Get a Guest

                                                    [4] Update a Guest

                                                    [5] Delete a Guest

                                                    [6] Recover a Guest

                                                    [0] Go Back
                ");

                char key = Console.ReadKey().KeyChar;
                Console.Clear();

                GuestManager guestManager = new GuestManager();

                switch (key)
                {
                    case '1':
                        guestManager.Create(options);
                        break;

                    case '2':
                        guestManager.GetAll(options);
                        break;

                    case '3':
                        guestManager.GetOne(options);
                        break;

                    case '4':
                        guestManager.Update(options);
                        break;

                    case '5':
                        guestManager.Delete(options);
                        break;

                    case '6':
                        guestManager.Recover(options);
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
