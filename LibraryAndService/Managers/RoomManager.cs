using LibraryAndService.Data;
using LibraryAndService.Enumeration;
using Microsoft.EntityFrameworkCore;
using LibraryAndService.Models;
using LibraryAndService.Interface;

namespace LibraryAndService.Managers
{
    public class RoomManager : IEntityManager, IRecover, IHardDelete
    {
        public void Create(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext(options.Options))
            {
                bool isRunning = true;
                do
                {
                    Console.WriteLine("Create a Room.");
                    Console.WriteLine();

                    Console.Write("Room Name: ");
                    string? roomName = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(roomName))
                    {
                        Console.Write("Price: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal price) && price > 0)
                        {
                            Console.WriteLine("Select Room Type: ");
                            foreach (RoomType type in Enum.GetValues(typeof(RoomType)))
                            {
                                Console.WriteLine($"- {type}");
                            }

                            Console.Write("Room Type: ");
                            if (Enum.TryParse(Console.ReadLine(), out RoomType roomType))
                            {
                                Console.Write("Room Size (in sq. meters): ");
                                if (byte.TryParse(Console.ReadLine(), out byte roomSize) && roomSize >= 10 && roomSize <= 100)
                                {
                                    Room newRoom = new Room(roomName, price, roomType, roomSize, true);
                                    dbContext.Room.Add(newRoom);
                                    dbContext.SaveChanges();

                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Room was Successfully Updated.");
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
                                    Console.WriteLine("Invalid input for Room Size.");
                                    Console.WriteLine("Room Size must be more or equal 10 and less or equal 100.");
                                    Console.ResetColor();
                                }
                            }
                            else
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid input for Room Type.");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input for Price.");
                            Console.WriteLine("Price can't be less than 1.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Room Name cannot be empty.");
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
                    Console.WriteLine("Get a Room.");

                    foreach (Room room in dbContext.Room)
                    {
                        Console.WriteLine($"Id: {room.Id}, Room Name: {room.RoomName}, Active: {room.IsActive}");
                    }
                    Console.WriteLine();

                    Console.Write("Enter a Room Id: ");
                    if (int.TryParse(Console.ReadLine(), out int roomId))
                    {
                        Room? room = dbContext.Room.FirstOrDefault(r => r.Id == roomId);

                        if (room != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Room found Successfully.");
                            Console.ResetColor();
                            Console.WriteLine($"Room Found: Id: {room.Id}, Name: {room.RoomName}, Type: {room.RoomType}, Size: {room.RoomSize} sq. meters, Price: {room.Price}, Active: {room.IsActive}");

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
                            Console.WriteLine($"No Room found with Id {roomId}.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please enter a valid Room Id.");
                        Console.ResetColor();
                    }
                } while (isRunning);
            }
        }
        public void GetAll(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext(options.Options))
            {
                Console.WriteLine("Get all Rooms.");
                Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
                Console.WriteLine("║  Id  | Room Name | Price | RoomType | RoomSize | Active ║");
                Console.WriteLine("║—————————————————————————————————————————————————————————║");

                foreach (Room room in dbContext.Room)
                {
                    Console.WriteLine($"║ {room.Id} | {room.RoomName} | {room.Price} | {room.RoomType} | {room.RoomSize} | {room.IsActive} ║");

                    Console.WriteLine("║-----------------------------------------------------║");
                }

                Console.WriteLine("╚═════════════════════════════════════════════════════════╝");
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
                    Console.WriteLine("Update a Room.");
                    Console.WriteLine();

                    foreach (Room room in dbContext.Room)
                    {
                        Console.WriteLine($"Id: {room.Id}, Name: {room.RoomName}, Type: {room.RoomType}, Size: {room.RoomSize} sq. meters, Price: {room.Price}, Active: {room.IsActive}");
                    }

                    Console.WriteLine();
                    Console.Write("Enter the Id of the Room to update: ");
                    if (int.TryParse(Console.ReadLine(), out int roomId))
                    {
                        Room? roomToUpdate = dbContext.Room.FirstOrDefault(r => r.Id == roomId);

                        if (roomToUpdate != null)
                        {
                            Console.Write("New Room Name: ");
                            string? roomNameUpdate = Console.ReadLine();

                            if (!string.IsNullOrWhiteSpace(roomNameUpdate))
                            {
                                Console.Write("New Price: ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal priceUpdate) && priceUpdate > 0)
                                {
                                    Console.WriteLine("Select New Room Type");
                                    foreach (RoomType type in Enum.GetValues(typeof(RoomType)))
                                    {
                                        Console.WriteLine($"- {type}");
                                    }

                                    if (Enum.TryParse(Console.ReadLine(), out RoomType roomTypeUpdate))
                                    {
                                        Console.Write("New Room Size  (in sq. meters): ");
                                        if (byte.TryParse(Console.ReadLine(), out byte roomSizeUpdate) && roomSizeUpdate >= 10 && roomSizeUpdate <= 100)
                                        {
                                            roomToUpdate.RoomName = roomNameUpdate;
                                            roomToUpdate.Price = priceUpdate;
                                            roomToUpdate.RoomType = roomTypeUpdate;
                                            roomToUpdate.RoomSize = roomSizeUpdate;

                                            dbContext.SaveChanges();
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine($"Room with Id {roomId} has been updated.");
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
                                            Console.WriteLine("Invalid input for Room Size.");
                                            Console.WriteLine("Room Size must be more or equal 10 and less or equal 100.");
                                            Console.ResetColor();
                                        }
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid input for Room Type.");
                                        Console.ResetColor();
                                    }
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Invalid input for Price.");
                                    Console.WriteLine("Price can't be less than 1.");
                                    Console.ResetColor();
                                }
                            }
                            else
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Room Name cannot be empty.");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Room with Id {roomId} not found.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input for Room Id.");
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
                    Console.WriteLine("Delete a Room.");

                    foreach (Room room in dbContext.Room.Where(r => r.IsActive))
                    {
                        Console.WriteLine($"Id: {room.Id}, Room Name: {room.RoomName}, Room Type: {room.RoomType}, Active: {room.IsActive}");
                    }

                    Console.WriteLine();
                    Console.Write("Write the Room Id of the Room you want to Delete: ");

                    if (int.TryParse(Console.ReadLine(), out int roomId))
                    {
                        Room? roomToDelete = dbContext.Room.Find(roomId);

                        if (roomToDelete != null && roomToDelete.IsActive == true)
                        {
                            roomToDelete.IsActive = false;
                            dbContext.SaveChanges();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Successfully soft-deleted Room with Id {roomId}.");
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
                            Console.WriteLine($"No Active Room found with Id {roomId}.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please enter a valid Room Id.");
                        Console.ResetColor();
                    }
                } while (isRunning);
            }
        }
        public void Recover(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext(options.Options))
            {
                bool isRunning = true;
                do
                {
                    Console.WriteLine("Recover a Room.");

                    foreach (Room room in dbContext.Room.Where(r => !r.IsActive))
                    {
                        Console.WriteLine($"Id: {room.Id}, Room Name: {room.RoomName}, Room Type: {room.RoomType}, Active: {room.IsActive}");
                    }

                    Console.WriteLine();
                    Console.Write("Enter the Room Id to recover: ");

                    if (int.TryParse(Console.ReadLine(), out int roomId))
                    {
                        Room? roomToRecover = dbContext.Room.Find(roomId);

                        if (roomToRecover != null && !roomToRecover.IsActive)
                        {
                            roomToRecover.IsActive = true;
                            dbContext.SaveChanges();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Room with Id {roomId} has been Recovered.");
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
                            Console.WriteLine($"Room with Id {roomId} dose not exist or already active.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please enter a valid Room Id.");
                        Console.ResetColor();
                    }
                } while (isRunning);
            }
        }
        public void HardDelete(DbContextOptionsBuilder<ApplicationDbContext> options)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext(options.Options))
            {
                bool isRunning = true;
                do
                {
                    Console.WriteLine("Hard Delete a Room.");
                    Console.WriteLine();
                    Console.WriteLine("Are you sure you want to HARD Delete a Room? (You can only Hard Delete a room thats not Active.)");
                    Console.Write("yes/no: ");

                    string? inputValue = Console.ReadLine();

                    if (inputValue != null)
                    {
                        string yesOrNo = inputValue.ToLower();

                        if (yesOrNo == "yes")
                        {
                            foreach (Room room in dbContext.Room.Where(r => !r.IsActive))
                            {
                                Console.WriteLine($"Id: {room.Id},Room Name: {room.RoomName}, Active: {room.IsActive}");
                            }
                            Console.WriteLine();
                            Console.Write("Enter a Room Id: ");

                            if (int.TryParse(Console.ReadLine(), out int roomId))
                            {
                                Room? roomToHardDelete = dbContext.Room.FirstOrDefault(r => r.Id == roomId);

                                if (roomToHardDelete != null && !roomToHardDelete.IsActive)
                                {
                                    dbContext.Room.Remove(roomToHardDelete);
                                    dbContext.SaveChanges();

                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"Room with Id {roomId} was Successfully Hard Deleted.");
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
                                    Console.WriteLine($"Room with Id {roomId} dose not exist or is a Active Room.");
                                    Console.ResetColor();
                                }
                            }
                            else
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid input. Please enter a valid Room Id.");
                                Console.ResetColor();
                            }
                        }
                        else if (yesOrNo == "no")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Okay!");
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
                            Console.WriteLine("Invalid input. Please enter yes or no.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please enter yes or no.");
                        Console.ResetColor();
                    }

                } while (isRunning);
            }
        }
    }
}
