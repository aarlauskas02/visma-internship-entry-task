class MainProgram
{
    static void Main(string[] args)
    {
        ShortageManagement shortageManager = new();
        LoginManager loginManager = new();

        while (true)
        {
            Console.WriteLine("Welcome to the Shortage Manager!");
            Console.WriteLine("Please log in to continue.");
            Console.Write("Username: ");
            string username = Console.ReadLine() ?? "";
            if (loginManager.Login(username))
            {
                if (loginManager.CurrentUser == null)
                {
                    Console.WriteLine("No user is currently logged in.");
                    break;
                }
                Console.WriteLine($"Logged in as {loginManager.CurrentUser.Username} with role {loginManager.CurrentUser.Role}.");
                break;
            }
            else
            {
                Console.WriteLine("Login failed. Please try again.");
            }

        }

        while (true)
        {
            Console.WriteLine("Enter a command (add, remove, list, exit):");
            string command = Console.ReadLine()?.ToLower() ?? "";

            switch (command)
            {
                case "add":
                    ShortageFactory shortageFactory = new ShortageFactory();
                    Console.WriteLine("Enter shortage title:");
                    string title = Console.ReadLine() ?? "";

                    Console.WriteLine("Enter shortage name:");
                    string name = Console.ReadLine() ?? "";

                    Room room = EnumSelection<Room>();

                    Category category = EnumSelection<Category>();

                    Console.WriteLine("Enter priority (1-10):");
                    int priority = 0;
                    while (true)
                    {
                        priority = int.TryParse(Console.ReadLine(), out int parsedPriority) ? parsedPriority : 0;

                        if (priority < 1 || priority > 10)
                        {
                            Console.WriteLine("Invalid input. Priority must be between 1 and 10.");

                        }
                        else
                        {
                            break;
                        }
                    }
                    if (loginManager.CurrentUser == null)
                    {
                        Console.WriteLine("No user is currently logged in.");
                        break;
                    }
                    Shortage? newShortage = shortageFactory.CreateShortage(title, name, room, category, priority, loginManager.CurrentUser);
                    if (newShortage != null)
                    {
                        shortageManager.AddShortage(newShortage, loginManager.CurrentUser);
                    }
                    break;

                case "remove":
                    Console.WriteLine("Enter the title and room (Meeting_room, Kitchen, Bathroom) of the shortage to remove (format: Title Room):");
                    string toRemove = Console.ReadLine() ?? "";
                    string[] parts = toRemove.Split(' ', 2);
                    if (parts.Length < 2)
                    {
                        Console.WriteLine("Invalid input. Please provide both title and name.");
                        break;
                    }
                    string titleToRemove = parts[0];
                    string roomToRemove = parts[1];
                    if (!Enum.TryParse(roomToRemove, true, out Room roomEnum))
                    {
                        Console.WriteLine($"Invalid room type: {roomToRemove}. Please use 'Meeting_room', 'Kitchen', or 'Bathroom'.");
                        break;
                    }
                    if (loginManager.CurrentUser == null)
                    {
                        Console.WriteLine("No user is currently logged in.");
                        break;
                    }
                    shortageManager.RemoveShortageByTitleAndRoom(titleToRemove, roomEnum, loginManager.CurrentUser);
                    break;

                case "list":
                    Console.WriteLine("Possible filtering options:");
                    Console.WriteLine("1. Filter by Title\n(if the title is “wireless speaker”, typing for “title Speaker” will return this entry)\n(if applied multiple times, it will only return shortages with both words in the title)\n");
                    Console.WriteLine("2. Filter by CreatedOn date\n(typing for “createdOn 2023-10-01 2023-11-01” will return all entries created between these dates)\n");
                    Console.WriteLine("3. Filter by Category\n(typing category followed by Electronics, Food or Other will return all entries in that category)\n");
                    Console.WriteLine("4. Filter by Room\n(typing room followed by Meeting_room, Kitchen or Bathroom will return all entries in that room)\n");
                    Console.WriteLine("Simply press enter for no filters.");
                    Console.WriteLine("Room and Category filters are case sensitive.");
                    Console.WriteLine("Seperate different filters with a comma.");

                    string filter = Console.ReadLine() ?? "";
                    if (loginManager.CurrentUser == null)
                    {
                        Console.WriteLine("No user is currently logged in.");
                        break;
                    }
                    shortageManager.ExecuteFiltering(filter, loginManager.CurrentUser);
                    break;

                case "exit":
                    return;

                default:
                    Console.WriteLine("Unknown command. Please try again.");
                    break;
            }

        }

    }

    private static T EnumSelection<T>() where T : struct, Enum
    {
        var names = Enum.GetNames(typeof(T));
        for (int i = 0; i < names.Length; i++)
        {
            Console.WriteLine($"{i + 1} - {names[i]}");
        }
        while (true)
        {
            Console.WriteLine($"Please enter the number corresponding to the {typeof(T).Name}:");
            string input = Console.ReadLine() ?? "";
            if (int.TryParse(input, out int index) && index >= 1 && index <= names.Length)
            {
                return (T)Enum.Parse(typeof(T), names[index - 1]);
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        }
    }


}
