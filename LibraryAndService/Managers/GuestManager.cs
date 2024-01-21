using LibraryAndService.Data;
using LibraryAndService.Interface;
using LibraryAndService.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAndService.Managers
{
    public class GuestManager : IEntityManager, IRecover
    {
        public void Create(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            using (var dbContext = new ApplicationDbContext(options.Options))
            {
                bool isRunning = true;
                do
                {
                    Console.WriteLine("Create a Guest.");
                    Console.WriteLine("Write exit if you want to go back.");
                    Console.WriteLine();
                    Console.Write("First Name: ");
                    string? firstNameInput = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(firstNameInput) && firstNameInput.Length > 1 && !string.Equals(firstNameInput, "exit", StringComparison.OrdinalIgnoreCase))
                    {
                        string firstName = firstNameInput;

                        Console.Write("Last Name: ");
                        string? lastNameInput = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(lastNameInput) && lastNameInput.Length > 1 && !string.Equals(lastNameInput, "exit", StringComparison.OrdinalIgnoreCase))
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

                                if (!string.IsNullOrWhiteSpace(emailInput) && !string.Equals(emailInput, "exit", StringComparison.OrdinalIgnoreCase))
                                {
                                    string email = emailInput;

                                    Console.WriteLine("Age can be left blank.");
                                    Console.Write("Age: ");
                                    string? ageInput = Console.ReadLine();

                                    if (byte.TryParse(ageInput, out byte age) && age > 0)
                                    {
                                        dbContext.Guest.Add(new Guest(firstName, lastName, phoneNumber, email, age, true));
                                        dbContext.SaveChanges();

                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Guest was Successfully Created.");
                                        Console.ResetColor();

                                        isRunning = false;

                                        Console.WriteLine();
                                        Console.WriteLine("Press any key to go back.");
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    else if (string.IsNullOrWhiteSpace(ageInput))
                                    {
                                        dbContext.Guest.Add(new Guest(firstName, lastName, phoneNumber, email, null, true));
                                        dbContext.SaveChanges();

                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Guest was Successfully Created.");
                                        Console.ResetColor();

                                        isRunning = false;

                                        Console.WriteLine();
                                        Console.WriteLine("Press any key to go back.");
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    else if (string.Equals(ageInput, "exit", StringComparison.OrdinalIgnoreCase))
                                    {
                                        isRunning = false;
                                        Console.Clear();
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid input. Please enter a valid age (a positive number) or leave blank.");
                                        Console.ResetColor();
                                    }
                                }
                                else if (string.Equals(emailInput, "exit", StringComparison.OrdinalIgnoreCase))
                                {
                                    isRunning = false;
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Invalid input. Email cannot be blank.");
                                    Console.ResetColor();
                                }
                            }
                            else if (string.Equals(phoneNumberInput, "exit", StringComparison.OrdinalIgnoreCase))
                            {
                                isRunning = false;
                                Console.Clear();
                            }
                            else
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid input. Phone Number must contain only numbers and whitespace.");
                                Console.ResetColor();
                            }
                        }
                        else if (string.Equals(lastNameInput, "exit", StringComparison.OrdinalIgnoreCase))
                        {
                            isRunning = false;
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input. Last Name must be longer than one character.");
                            Console.ResetColor();
                        }
                    }
                    else if (string.Equals(firstNameInput, "exit", StringComparison.OrdinalIgnoreCase))
                    {
                        isRunning = false;
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. First Name must be longer than one character.");
                        Console.ResetColor();
                    }

                } while (isRunning);
            }
        }
        public void GetOne(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            using (var dbContext = new ApplicationDbContext(options.Options))
            {
                bool isRunning = true;
                do
                {
                    Console.WriteLine("Get a Guest.");
                    Console.WriteLine("Write exit if you want to go back.");
                    Console.WriteLine();

                    foreach (Guest guest in dbContext.Guest)
                    {
                        Console.WriteLine($"Id: {guest.Id}, Full Name: {guest.FirstName} {guest.LastName}, Phone Number: {guest.PhoneNumber}");
                    }
                    Console.WriteLine();

                    Console.WriteLine("Search for a Guest by writing their First Name and the four last digits of their Phone Number");
                    Console.WriteLine("Separate the First Name and numbers with ,");
                    Console.Write("First Name and 4 digits: ");

                    string? userInput = Console.ReadLine();

                    if (!string.IsNullOrEmpty(userInput) && !string.Equals(userInput, "exit", StringComparison.OrdinalIgnoreCase))
                    {
                        string[] inputParts = userInput.Split(',');

                        if (inputParts.Length == 2)
                        {
                            string firstName = inputParts[0].Trim();
                            string lastFourDigits = inputParts[1].Trim();

                            Guest? foundGuest = dbContext.Guest
                                .FirstOrDefault(g => g.FirstName == firstName && g.PhoneNumber.EndsWith(lastFourDigits));

                            if (foundGuest != null)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Guest found Successfully.");
                                Console.ResetColor();
                                Console.WriteLine($"Id: {foundGuest.Id}, Full Name: {foundGuest.FirstName} {foundGuest.LastName}, Phone Number: {foundGuest.PhoneNumber}");

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
                                Console.WriteLine("No matching guest found.");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input format. Please separate First Name and 4 digits with a comma.");
                            Console.ResetColor();
                        }
                    }
                    else if (string.Equals(userInput, "exit", StringComparison.OrdinalIgnoreCase))
                    {
                        isRunning = false;
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Input cannot be empty.");
                        Console.ResetColor();
                    }
                } while (isRunning);
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
                Console.WriteLine();
                Console.WriteLine("Press any key to go back.");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void Update(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            using (var dbContext = new ApplicationDbContext(options.Options))
            {
                bool isRunning = true;
                do
                {
                    Console.WriteLine("Update a Guest.");
                    Console.WriteLine("Write exit if you want to go back.");
                    Console.WriteLine();

                    foreach (Guest guest in dbContext.Guest)
                    {
                        Console.WriteLine($"Id: {guest.Id}, Full Name: {guest.FirstName} {guest.LastName}, Phone Number: {guest.PhoneNumber}");
                    }
                    Console.WriteLine();

                    Console.Write("Write the Id of the Guest you want to update: ");
                    string? userInput = Console.ReadLine();
                    if (int.TryParse(userInput, out int guestId))
                    {
                        Guest? guestToUpdate = dbContext.Guest.Find(guestId);

                        if (guestToUpdate != null)
                        {
                            Console.Write("First Name: ");
                            string? firstNameUpdate = Console.ReadLine();

                            if (!string.IsNullOrWhiteSpace(firstNameUpdate) && !string.Equals(firstNameUpdate, "exit", StringComparison.OrdinalIgnoreCase))
                            {
                                Console.Write("Last Name: ");
                                string? lastNameUpdate = Console.ReadLine();

                                if (!string.IsNullOrWhiteSpace(lastNameUpdate) && !string.Equals(lastNameUpdate, "exit", StringComparison.OrdinalIgnoreCase))
                                {
                                    Console.WriteLine("Phone Number can only contain numbers and whitespace.");
                                    Console.Write("Phone Number: ");
                                    string? phoneNumberUpdate = Console.ReadLine();

                                    if (!string.IsNullOrEmpty(phoneNumberUpdate) && phoneNumberUpdate.All(c => char.IsDigit(c) || char.IsWhiteSpace(c)))
                                    {
                                        Console.Write("Email: ");
                                        string? emailUpdate = Console.ReadLine();

                                        if (!string.IsNullOrWhiteSpace(emailUpdate) && !string.Equals(emailUpdate, "exit", StringComparison.OrdinalIgnoreCase))
                                        {
                                            Console.WriteLine("Age can be left blank.");
                                            Console.Write("Age: ");
                                            string? ageUpdate = Console.ReadLine();

                                            if (byte.TryParse(ageUpdate, out byte age) && age > 0)
                                            {
                                                guestToUpdate.FirstName = firstNameUpdate;
                                                guestToUpdate.LastName = lastNameUpdate;
                                                guestToUpdate.PhoneNumber = phoneNumberUpdate;
                                                guestToUpdate.Email = emailUpdate;
                                                guestToUpdate.Age = age;
                                                guestToUpdate.IsActive = true;
                                                dbContext.SaveChanges();

                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine($"Guest with Id {guestId} has been updated.");
                                                Console.ResetColor();

                                                isRunning = false;

                                                Console.WriteLine();
                                                Console.WriteLine("Press any key to go back.");
                                                Console.ReadKey();
                                                Console.Clear();
                                            }
                                            else if (string.IsNullOrWhiteSpace(ageUpdate))
                                            {
                                                guestToUpdate.FirstName = firstNameUpdate;
                                                guestToUpdate.LastName = lastNameUpdate;
                                                guestToUpdate.PhoneNumber = phoneNumberUpdate;
                                                guestToUpdate.Email = emailUpdate;
                                                guestToUpdate.Age = null;
                                                guestToUpdate.IsActive = true;
                                                dbContext.SaveChanges();

                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine($"Guest with Id {guestId} has been updated.");
                                                Console.ResetColor();

                                                isRunning = false;

                                                Console.WriteLine();
                                                Console.WriteLine("Press any key to go back.");
                                                Console.ReadKey();
                                                Console.Clear();
                                            }
                                            else if (string.Equals(ageUpdate, "exit", StringComparison.OrdinalIgnoreCase))
                                            {
                                                isRunning = false;
                                                Console.Clear();
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Invalid input. Please enter a valid age (a positive number) or leave blank.");
                                                Console.ResetColor();
                                            }
                                        }
                                        else if (string.Equals(emailUpdate, "exit", StringComparison.OrdinalIgnoreCase))
                                        {
                                            isRunning = false;
                                            Console.Clear();
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Invalid input. Email cannot be blank.");
                                            Console.ResetColor();
                                        }
                                    }
                                    else if (string.Equals(phoneNumberUpdate, "exit", StringComparison.OrdinalIgnoreCase))
                                    {
                                        isRunning = false;
                                        Console.Clear();
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid input. Phone Number must contain only numbers and whitespace.");
                                        Console.ResetColor();
                                    }
                                }
                                else if (string.Equals(lastNameUpdate, "exit", StringComparison.OrdinalIgnoreCase))
                                {
                                    isRunning = false;
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Invalid input. Last Name cannot be blank.");
                                    Console.ResetColor();
                                }
                            }
                            else if (string.Equals(firstNameUpdate, "exit", StringComparison.OrdinalIgnoreCase))
                            {
                                isRunning = false;
                                Console.Clear();
                            }
                            else
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid input. First Name cannot be blank.");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"No Guest found with Id {guestId}.");
                            Console.ResetColor();
                        }
                    }
                    else if (string.Equals(userInput, "exit", StringComparison.OrdinalIgnoreCase))
                    {
                        isRunning = false;
                        Console.Clear();
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
            using (var dbContext = new ApplicationDbContext(options.Options))
            {
                bool isRunning = true;
                do
                {
                    Console.WriteLine("Delete a Guest.");
                    Console.WriteLine("Write exit if you want to go back.");
                    Console.WriteLine();

                    foreach (Booking b in dbContext.Booking.Include(b => b.Guest).Where(b => b.Guest.IsActive && b.IsActive == false))
                    {
                        Console.WriteLine($"Id: {b.Guest.Id}, Full Name: {b.Guest.FirstName} {b.Guest.LastName}, Phone Number: {b.Guest.PhoneNumber}, Is Active: {b.Guest.IsActive}");
                    }
                    Console.WriteLine();
                    Console.Write("Write the Guest Id of the Guest you want to Delete: ");
                    string? userInput = Console.ReadLine();

                    if (int.TryParse(userInput, out int guestId))
                    {
                        Guest? guestToDelete = dbContext.Guest.Find(guestId);

                        if (guestToDelete != null && guestToDelete.IsActive == true)
                        {

                            bool activeBooking = dbContext.Booking.Any(b => b.Guest.Id == guestId && b.IsActive == true);

                            if (!activeBooking)
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
                                Console.WriteLine("Cannot delete Guest. They have active bookings.");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"No Active Guest found with Id {guestId}.");
                            Console.ResetColor();
                        }
                    }
                    else if (string.Equals(userInput, "exit", StringComparison.OrdinalIgnoreCase))
                    {
                        isRunning = false;
                        Console.Clear();
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
                bool isRunning = true;
                do
                {
                    Console.WriteLine("Recover a Guest.");
                    Console.WriteLine("Write exit if you want to go back.");
                    Console.WriteLine();
                    foreach (Guest guest in dbContext.Guest.Where(g => !g.IsActive))
                    {
                        Console.WriteLine($"Id: {guest.Id}, Full Name: {guest.FirstName} {guest.LastName}, Phone Number: {guest.PhoneNumber}");
                    }

                    Console.WriteLine();
                    Console.Write("Enter the Guest Id to recover: ");
                    string? userInput = Console.ReadLine();

                    if (int.TryParse(userInput, out int guestId))
                    {
                        Guest? guestToRecover = dbContext.Guest.Find(guestId);

                        if (guestToRecover != null && !guestToRecover.IsActive)
                        {
                            guestToRecover.IsActive = true;
                            dbContext.SaveChanges();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Guest with Id {guestId} has been Recovered.");
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
                            Console.WriteLine($"Guest with Id {guestId} dose not exist or already active.");
                            Console.ResetColor();
                        }
                    }
                    else if (string.Equals(userInput, "exit", StringComparison.OrdinalIgnoreCase))
                    {
                        isRunning = false;
                        Console.Clear();
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
    }
}
