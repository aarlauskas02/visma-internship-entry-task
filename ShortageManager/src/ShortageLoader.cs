using System.Text.Json;

public class ShortageLoader
{

    private readonly string appFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ShortageManager");
    private readonly string filePath;
    public List<Shortage> shortages = [];

    public ShortageLoader(string fileName = "shortagedata.json")
    {
        if (!Directory.Exists(appFolder))
            Directory.CreateDirectory(appFolder);

        filePath = Path.Combine(appFolder, fileName);

        if (!File.Exists(filePath))
            File.WriteAllText(filePath, "[]");

        LoadShortagesFromFile();
    }

    public void LoadShortagesFromFile()
    {
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
                        var shortage = JsonSerializer.Deserialize<Shortage>(element);
                        if (shortage != null)
                        {
                            shortages.Add(shortage);
                        }
                        else
                        {
                            Console.WriteLine("Warning: Shortage deserialization returned null.");
                        }
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"Warning: Error deserializing shortage: {ex.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading shortages file: {ex.Message}");
        }
        OrderShortagesByPriority();
    }

    public void SaveShortagesToFile(List<Shortage> shortagesUpdate)
    {
        this.shortages = shortagesUpdate;
        try
        {
            OrderShortagesByPriority();
            using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            JsonSerializer.Serialize(stream, shortages);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving shortages to file: {ex.Message}");
        }
    }

    private void OrderShortagesByPriority()
    {
        shortages.Sort((x, y) => y.Priority.CompareTo(x.Priority));
    }

    internal List<Shortage> GetShortages()
    {
        if (shortages.Count == 0)
        {
            Console.WriteLine("No shortages found.");
            return [];
        }
        return shortages;
    }
}