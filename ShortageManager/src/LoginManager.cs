using System.Text.Json;

public class LoginManager
{
    public User? CurrentUser { get; private set; }
    private readonly string appFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ShortageManager");
    private readonly string fileName = "logindata.json";
    private readonly string filePath;
    public LoginManager()
    {
        if (!Directory.Exists(appFolder))
            Directory.CreateDirectory(appFolder);

        filePath = Path.Combine(appFolder, fileName);

        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
            Console.WriteLine("No users found. Please create a user first.");
        }

    }
    public bool Login(string username)
    {

        // Trying to read the file and deserialize the JSON
        try
        {
            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using var doc = JsonDocument.Parse(stream);
            var root = doc.RootElement;

            if (root.ValueKind == JsonValueKind.Array)
            {
                foreach (var element in root.EnumerateArray())
                {
                    try
                    {
                        var user = JsonSerializer.Deserialize<User>(element);
                        if (user != null)
                        {
                            if (user.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
                            {
                                CurrentUser = user;
                                return true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Warning: User deserialization returned null.");
                            return false;
                        }
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"Warning: Error deserializing shortage: {ex.Message}");
                        return false;
                    }
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading shortages file: {ex.Message}");
            return false;
        }
    }
}