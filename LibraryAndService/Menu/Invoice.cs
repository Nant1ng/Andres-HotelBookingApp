using LibraryAndService.Data;
using LibraryAndService.Managers;
using Microsoft.EntityFrameworkCore;

namespace LibraryAndService.Menu
{
    public class Invoice
    {
        public void Menu(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            bool isRunning = true;

            do
            {
                Console.WriteLine(@"
                                        ██╗███╗   ██╗██╗   ██╗ ██████╗ ██╗ ██████╗███████╗
                                        ██║████╗  ██║██║   ██║██╔═══██╗██║██╔════╝██╔════╝
                                        ██║██╔██╗ ██║██║   ██║██║   ██║██║██║     █████╗  
                                        ██║██║╚██╗██║╚██╗ ██╔╝██║   ██║██║██║     ██╔══╝  
                                        ██║██║ ╚████║ ╚████╔╝ ╚██████╔╝██║╚██████╗███████╗
                                        ╚═╝╚═╝  ╚═══╝  ╚═══╝   ╚═════╝ ╚═╝ ╚═════╝╚══════╝
                                                  
                                                        
                                                    [1] Get all Invoices

                                                    [2] Invoice got Payed

                                                    [0] Go Back 
                ");

                char key = Console.ReadKey().KeyChar;
                Console.Clear();

                InvoiceManager invoiceManager = new InvoiceManager();

                switch (key)
                {
                    case '1':
                        invoiceManager.GetAll(options);
                        break;

                    case '2':
                        invoiceManager.Payed(options);
                        break;

                    case '0':
                        isRunning = false;
                        break;

                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid selection. Please choose a valid option.");
                        Console.ResetColor();
                        break;
                }
            } while (isRunning);
        }
    }
}