namespace LibraryAndService.Menu
{
    public class Room
    {
        public void Menu()
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

                                                    [0] Go Back
                ");

                char key = Console.ReadKey().KeyChar;
                Console.Clear();

                switch (key)
                {
                    case '1':
                        Console.WriteLine("You pressed 1 - Booking");
                        break;

                    case '2':
                        Console.WriteLine("You pressed 2 - Guest");
                        break;

                    case '3':
                        Console.WriteLine("You pressed 3 - Room");
                        break;

                    case '4':
                        Console.WriteLine("You pressed 3 - Room");
                        break;

                    case '5':
                        Console.WriteLine("You pressed 3 - Room");
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
