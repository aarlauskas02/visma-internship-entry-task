public class Shortage
{
    public required string Title { get; set; }
    public required string Name { get; set; }
    public required Room Room { get; set; }
    public required Category Category { get; set; }
    public required int Priority { get; set; }
    public required DateTime CreatedOn { get; set; }
    public required User CreatedBy { get; set; }
}