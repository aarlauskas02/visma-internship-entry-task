public class ShortageFactory
{

    public Shortage? CreateShortage(
        string title,
        string name,
        Room room,
        Category category,
        int priority,
        User createdBy)
    {
        if (ValidateShortage(
            title,
            name,
            room,
            category,
            priority))
        {
            return new Shortage
            {
                Title = title,
                Name = name,
                Room = room,
                Category = category,
                Priority = priority,
                CreatedOn = DateTime.Now,
                CreatedBy = createdBy
            };
        }
        else
        {
            Console.WriteLine("Shortage creation failed due to validation errors.");
            return null;
        }
    }

    private static bool ValidateShortage(
        string title,
        string name,
        Room room,
        Category category,
        int priority)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("ERROR: Shortage title empty.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("ERROR: Shortage name empty.");
            return false;
        }

        return true;
    }
}