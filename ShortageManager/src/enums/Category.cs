using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Category
{
    Electronics,
    Food,
    Other
}