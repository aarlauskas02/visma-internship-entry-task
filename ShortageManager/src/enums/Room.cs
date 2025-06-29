using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Room
{
    Meeting_room,
    Kitchen,
    Bathroom
}