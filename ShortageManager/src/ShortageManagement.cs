public class ShortageManagement
{

    public List<Shortage> filteredShortages = [];
    public readonly List<Shortage> shortages = [];
    private readonly ShortageLoader shortageLoader;

    public ShortageManagement(string fileName = "shortagedata.json")
    {
        shortageLoader = new(fileName);
        shortages = shortageLoader.GetShortages();
    }

    public void AddShortage(Shortage newShortage, User user)
    {


        if (shortages.Any(s =>
            s.Title.Equals(newShortage.Title, StringComparison.OrdinalIgnoreCase) &&
            s.Room == newShortage.Room &&
            s.Priority >= newShortage.Priority))
        {

            Console.WriteLine("Shortage with the same title and name already exists with a higher priority.");

        }
        else if (shortages.Any(s =>
            s.Title.Equals(newShortage.Title, StringComparison.OrdinalIgnoreCase) &&
            s.Room == newShortage.Room &&
            s.Priority < newShortage.Priority))
        {
            RemoveShortageByTitleAndRoom(newShortage.Title, newShortage.Room, user);
            shortages.Add(newShortage);
            shortageLoader.SaveShortagesToFile(shortages);
            Console.WriteLine("Shortage with the same title and name but lower priority found and overwritten.");
        }
        else
        {

            shortages.Add(newShortage);
            shortageLoader.SaveShortagesToFile(shortages);
            Console.WriteLine("Shortage added successfully.");
        }
    }
    public void RemoveShortageByTitleAndRoom(string title, Room room, User user)
    {
        var removal = shortages.FirstOrDefault(s =>
            s.Title.Equals(title, StringComparison.OrdinalIgnoreCase) &&
            s.Room == room &&
            ((s.CreatedBy.Username == user.Username && s.CreatedBy.Role == user.Role) || user.Role == Role.Admin));

        if (removal == null)
        {
            Console.WriteLine("No shortage found with the specified title and name.");
            return;
        }

        shortages.Remove(removal);
        shortageLoader.SaveShortagesToFile(shortages);
        Console.WriteLine("Shortage removed successfully.");
    }

    public bool CheckIfEmpty()
    {

        if (shortages.Count == 0)
        {
            Console.WriteLine("No shortages found.");
            return true;
        }
        return false;
    }

    internal void GetAllShortages(User user)
    {
        if (!CheckIfEmpty())
        {
            foreach (var shortage in shortages)
            {
                if ((shortage.CreatedBy.Username == user.Username && shortage.CreatedBy.Role == user.Role) || user.Role == Role.Admin)
                {
                    Console.WriteLine(
                        $"Title: {shortage.Title}, Name: {shortage.Name}, Room: {shortage.Room}, Category: {shortage.Category}, Priority: {shortage.Priority}, CreatedOn: {shortage.CreatedOn}\n"
                    );

                }

            }
        }
    }

    internal void GetFilteredShortages(User user)
    {
        if (filteredShortages.Count != 0)
        {
            foreach (var shortage in filteredShortages)
            {
                if ((shortage.CreatedBy.Username == user.Username && shortage.CreatedBy.Role == user.Role) || user.Role == Role.Admin)
                {
                    Console.WriteLine(
                        $"Title: {shortage.Title}, Name: {shortage.Name}, Room: {shortage.Room}, Category: {shortage.Category}, Priority: {shortage.Priority}, CreatedOn: {shortage.CreatedOn}\n"
                    );

                }
            }
        }
        else
        {
            Console.WriteLine("No shortages found with the specified filters.");
        }
    }

    public void FilterByTitle(string filterValue)
    {
        if (!CheckIfEmpty() && filterValue.Split(' ').Length == 1)
        {
            if (filteredShortages.Count > 0)
            {
                filteredShortages.RemoveAll(filteredShortages => !filteredShortages.Title.Contains(filterValue, StringComparison.OrdinalIgnoreCase));
            }
            foreach (var shortage in shortages)
            {
                if (shortage.Title.Contains(filterValue, StringComparison.OrdinalIgnoreCase) && !filteredShortages.Contains(shortage))
                {
                    filteredShortages.Add(shortage);
                }
            }
        }
    }

    public void FilterByCreatedOn(string filterValue)
    {
        if (!CheckIfEmpty())
        {
            foreach (var shortage in shortages)
            {
                var dates = filterValue.Split(' ');
                if (dates.Length == 2 &&
                    DateTime.TryParse(dates[0], out DateTime startDate) &&
                    DateTime.TryParse(dates[1], out DateTime endDate))
                {
                    if (shortage.CreatedOn.Date >= startDate.Date && shortage.CreatedOn.Date <= endDate.Date && !filteredShortages.Contains(shortage))
                    {
                        filteredShortages.Add(shortage);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please use 'yyyy-MM-dd yyyy-MM-dd' for date ranges.");
                    break;
                }
            }
        }
    }

    public void FilterByCategory(string filterValue)
    {
        if (!CheckIfEmpty() && filterValue.Split(' ').Length == 1)
        {
            foreach (var shortage in shortages)
            {
                if (Enum.TryParse(filterValue, out Category category))
                {
                    if (shortage.Category == category && !filteredShortages.Contains(shortage))
                    {
                        filteredShortages.Add(shortage);
                    }
                }
                else
                {
                    Console.WriteLine($"Invalid category type: {filterValue}. Please use 'Electronics', 'Food', or 'Other'.");
                    break;
                }
            }
        }
    }

    public void FilterByRoom(string filterValue)
    {
        if (!CheckIfEmpty() && filterValue.Split(' ').Length == 1)
        {
            foreach (var shortage in shortages)
            {
                if (Enum.TryParse(filterValue, out Room room))
                {
                    if (shortage.Room == room && !filteredShortages.Contains(shortage))
                    {
                        filteredShortages.Add(shortage);
                    }
                }
                else
                {
                    Console.WriteLine($"Invalid room type: {filterValue}. Please use 'Meeting_room', 'Kitchen', or 'Bathroom'.");
                    break;
                }
            }
        }
    }

    public void ClearFilters()
    {
        filteredShortages.Clear();
    }

    internal void ExecuteFiltering(string filter, User? user)
    {
        bool hasFilters = false;
        string[] filterParts = filter.Split(',', StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < filterParts.Length; i++)
        {
            filterParts[i] = filterParts[i].Trim();
            string filterType = filterParts[i].Split(' ')[0].ToLower();
            string filterValue = filterParts[i][filterType.Length..].Trim();

            Enum.TryParse(filterType, true, out FilterType parsedFilterType);

            switch (parsedFilterType)
            {
                case FilterType.Title:
                    this.FilterByTitle(filterValue);
                    hasFilters = true;
                    break;

                case FilterType.CreatedOn:
                    this.FilterByCreatedOn(filterValue);
                    hasFilters = true;
                    break;

                case FilterType.Category:
                    this.FilterByCategory(filterValue);
                    hasFilters = true;
                    break;

                case FilterType.Room:
                    this.FilterByRoom(filterValue);
                    hasFilters = true;
                    break;

                default:
                    Console.WriteLine($"Unknown filter type {filterType}. Please use 'title', 'createdOn', 'category', 'room', or 'none'.");
                    hasFilters = true;
                    break;
            }

        }
        if (user == null)
        {
            Console.WriteLine("No user is currently logged in.");
        }
        else if (hasFilters)
        {
            this.GetFilteredShortages(user);
            this.ClearFilters();
        }
        else
        {
            this.GetAllShortages(user);
        }
    }
}