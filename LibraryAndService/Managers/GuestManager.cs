using LibraryAndService.Data;
using LibraryAndService.Interface;
using LibraryAndService.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAndService.Managers
{
    public class GuestManager : IEntityManager
    {
        public void Create(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            using (var dbContext = new ApplicationDbContext(options.Options))
            {
                Console.WriteLine("Create a Guest.");
                Console.WriteLine();
                Console.Write("First Name: ");
                string? firstNameInput = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(firstNameInput))
                {
                    string firstName = firstNameInput;

                    Console.Write("Last Name: ");
                    string? lastNameInput = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(lastNameInput))
                    {
                        string lastName = lastNameInput;

                        Console.WriteLine("Phone Number can only contain number and whitespace.");
                        Console.Write("Phone Number: ");
                        string? phoneNumberInput = Console.ReadLine();

                        if (!string.IsNullOrEmpty(phoneNumberInput) && phoneNumberInput.All(c => char.IsDigit(c) || char.IsWhiteSpace(c)))
                        {
                            string phoneNumber = phoneNumberInput;

                            Console.Write("Email: ");
                            string? emailInput = Console.ReadLine();

                            if (!string.IsNullOrWhiteSpace(emailInput))
                            {
                                string email = emailInput;

                                Console.WriteLine("Age can be left blank.");
                                Console.Write("Age: ");
                                string? ageInput = Console.ReadLine();

                                if (byte.TryParse(ageInput, out byte age))
                                {
                                    dbContext.Guest.Add(new Guest(firstName, lastName, phoneNumber, email, age, false, true));
                                    dbContext.SaveChanges();
                                }
                                else
                                {
                                    dbContext.Guest.Add(new Guest(firstName, lastName, phoneNumber, email, null, false, true));
                                    dbContext.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }
        }
        public void GetOne(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            using (var dbContext = new ApplicationDbContext(options.Options))
            {
                Console.WriteLine("Get a Guest.");
                Console.WriteLine("Search for a Guest by writing their First Name and the four last digits of their Phone Number");
                Console.WriteLine("Separate the First Name and numbers with ,");
                Console.Write("First Name and 4 digits: ");

                string? input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input))
                {
                    string[] inputParts = input.Split(',');

                    if (inputParts.Length == 2)
                    {
                        string firstName = inputParts[0].Trim();
                        string lastFourDigits = inputParts[1].Trim();

                        Guest? foundGuest = dbContext.Guest
                            .FirstOrDefault(g => g.FirstName == firstName && g.PhoneNumber.EndsWith(lastFourDigits));

                        if (foundGuest != null)
                        {
                            Console.WriteLine("Guest found Successfully.");
                            Console.WriteLine($"Id: {foundGuest.Id}, Full Name: {foundGuest.FirstName} {foundGuest.LastName}, Phone Number: {foundGuest.PhoneNumber}");
                        }
                        else
                        {
                            Console.WriteLine("No matching guest found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input format. Please separate First Name and 4 digits with a comma.");
                    }
                }
                else
                {
                    Console.WriteLine("Input cannot be empty.");
                }
            }
        }
        public void GetAll(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            using (var dbContext = new ApplicationDbContext(options.Options))
            {
                Console.WriteLine("Get all Guests");
                Console.WriteLine("╔═════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║  Id  | First name | Last name | Phone Number | Email | Age | Active ║");
                Console.WriteLine("║—————————————————————————————————————————————————————————————————————║");

                string? age;

                foreach (var guest in dbContext.Guest)
                {
                    if (guest.Age == null)
                    {
                        age = "NaN";
                    }
                    else
                    {
                        age = guest.Age.ToString();
                    }

                    Console.WriteLine($"║ {guest.Id} | {guest.FirstName} | {guest.LastName} | {guest.PhoneNumber} | {guest.Email} | {age} | {guest.IsActive} ║");

                    Console.WriteLine("║-----------------------------------------------------------------║");
                }
                Console.WriteLine("╚═════════════════════════════════════════════════════════════════════╝");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void Update(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            using (var dbContext = new ApplicationDbContext(options.Options))
            {
                Console.WriteLine("Update a Guest.");

                foreach (Guest guest in dbContext.Guest)
                {
                    Console.WriteLine($"Id: {guest.Id}, Full Name: {guest.FirstName} {guest.LastName}, Phone Number: {guest.PhoneNumber}");
                }
                Console.WriteLine();

                Console.Write("Write the Id of the Guest you want to update: ");
                if (int.TryParse(Console.ReadLine(), out int guestId))
                {
                    Guest? guestToUpdate = dbContext.Guest.Find(guestId);

                    if (guestToUpdate != null)
                    {
                        Console.WriteLine("First Name: ");
                        string? firstNameUpdate = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(firstNameUpdate))
                        {
                            Console.WriteLine("Last Name: ");
                            string? lastNameUpdate = Console.ReadLine();

                            if (!string.IsNullOrWhiteSpace(lastNameUpdate))
                            {
                                Console.WriteLine("Phone Number can only contain number and whitespace.");
                                Console.Write("Phone Number: ");
                                string? phoneNumberUpdate = Console.ReadLine();

                                if (!string.IsNullOrEmpty(phoneNumberUpdate) && phoneNumberUpdate.All(c => char.IsDigit(c) || char.IsWhiteSpace(c)))
                                {
                                    Console.Write("Email: ");
                                    string? emailUpdate = Console.ReadLine();

                                    if (!string.IsNullOrWhiteSpace(emailUpdate))
                                    {
                                        Console.WriteLine("Age can be left blank.");
                                        Console.Write("Age: ");
                                        string? ageUpdate = Console.ReadLine();

                                        if (byte.TryParse(ageUpdate, out byte age))
                                        {
                                            guestToUpdate.FirstName = firstNameUpdate;
                                            guestToUpdate.LastName = lastNameUpdate;
                                            guestToUpdate.PhoneNumber = phoneNumberUpdate;
                                            guestToUpdate.Email = emailUpdate;
                                            guestToUpdate.Age = age;
                                            guestToUpdate.Booked = false;
                                            guestToUpdate.IsActive = true;
                                            dbContext.SaveChanges();
                                        }
                                        else
                                        {
                                            guestToUpdate.FirstName = firstNameUpdate;
                                            guestToUpdate.LastName = lastNameUpdate;
                                            guestToUpdate.PhoneNumber = phoneNumberUpdate;
                                            guestToUpdate.Email = emailUpdate;
                                            guestToUpdate.Age = null;
                                            guestToUpdate.Booked = false;
                                            guestToUpdate.IsActive = true;
                                            dbContext.SaveChanges();
                                        }
                                        Console.WriteLine($"Guest with Id {guestId} has been update.");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No Guest found with Id {guestId}.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid Guest Id.");
                    }
                    Console.ReadKey();
                    Console.Clear();
                }
            }

        }
        public void Delete(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            using (var dbContext = new ApplicationDbContext(options.Options))
            {
                bool isRunning = true;
                do
                {
                    Console.WriteLine("Delete a Guest.");

                    foreach (Guest guest in dbContext.Guest.Where(g => g.IsActive))
                    {
                        Console.WriteLine($"Id: {guest.Id}, Full Name: {guest.FirstName} {guest.LastName}, Phone Number: {guest.PhoneNumber}, Is Active: {guest.IsActive}");
                    }
                    Console.WriteLine();
                    Console.Write("Write the Guest Id of the Guest you want to Delete: ");

                    if (int.TryParse(Console.ReadLine(), out int guestId))
                    {
                        Guest? guestToDelete = dbContext.Guest.Find(guestId);

                        if (guestToDelete != null && guestToDelete.IsActive == true)
                        {
                            guestToDelete.IsActive = false;
                            dbContext.SaveChanges();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Successfully soft-deleted Guest with Id {guestId}.");
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
                            Console.WriteLine($"No Active Guest found with Id {guestId}.");
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
        public void Recover(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            using (var dbContext = new ApplicationDbContext(options.Options))
            {
                Console.WriteLine("Recover a Guest.");
                foreach (Guest guest in dbContext.Guest.Where(g => !g.IsActive))
                {
                    Console.WriteLine($"Id: {guest.Id}, Full Name: {guest.FirstName} {guest.LastName}, Phone Number: {guest.PhoneNumber}");
                }

                Console.Write("Enter the Guest Id to recover: ");
                string? intputValue = Console.ReadLine();
                if (int.TryParse(intputValue, out int guestId))
                {
                    Guest? guestToRecover = dbContext.Guest.Find(guestId);

                    if (guestToRecover != null && !guestToRecover.IsActive)
                    {
                        guestToRecover.IsActive = true;
                        dbContext.SaveChanges();
                        Console.WriteLine($"Guest with Id {guestId} has been Recovered.");
                    }
                    else
                    {
                        Console.WriteLine($"Guest with Id {guestId} dose not exist or already active.");
                    }
                }
                else
                {
                    Console.WriteLine("The Guest Id must be a number.");
                }
            }
        }
    }
}
