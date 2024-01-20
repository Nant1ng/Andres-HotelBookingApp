using LibraryAndService.Data;
using LibraryAndService.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAndService.Managers
{
    public class InvoiceManager
    {
        public void GetAll(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                Console.WriteLine("Get all Invoice.");
                Console.WriteLine("╔═══════════════════════════════════════════╗");
                Console.WriteLine("║  Id  | Total | Deadline | Payed  | Active ║");
                Console.WriteLine("║————————————————————————————————----------—║");

                foreach (Invoice invoice in dbContext.Invoice)
                {
                    Console.WriteLine($"║ {invoice.Id} | {invoice.Total} | {invoice.Deadline} | {invoice.IsPayed} | {invoice.IsActive} ║");

                    Console.WriteLine("║-------------------------------------------║");
                }
                Console.WriteLine("╚═══════════════════════════════════════════╝");
                Console.WriteLine();
                Console.WriteLine("Press any key to go back.");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void Payed(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext(options.Options))
            {
                bool isRunning = true;
                do
                {
                    Console.WriteLine("Invoice got Payed");

                    foreach (Invoice invoice in dbContext.Invoice
                        .Where(i => !i.IsPayed && i.IsActive == true))
                    {
                        Console.WriteLine($"Invoice Id: {invoice.Id}, Total: {invoice.Total}, Deadline: {invoice.Deadline}, Payed: {invoice.IsPayed}, Active: {invoice.IsActive}");
                    }
                    Console.WriteLine();
                    Console.Write("Write the Id of the Invoice got Payed: ");

                    if (int.TryParse(Console.ReadLine(), out int invoiceId))
                    {
                        Invoice? payedInvoice = dbContext.Invoice.Find(invoiceId);

                        if (payedInvoice != null && payedInvoice.IsPayed == false && payedInvoice.IsActive == true)
                        {
                            payedInvoice.IsPayed = true;
                            dbContext.SaveChanges();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Successfully Payed Invoice with Id {invoiceId}");
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
                            Console.WriteLine($"No Invoice found with Id {invoiceId} or it's Not Active.");
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
