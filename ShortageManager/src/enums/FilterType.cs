using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FilterType
{
    Title,
    CreatedOn,
    Category,
    Room
}
