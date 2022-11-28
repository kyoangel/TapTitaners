using System.Text.Json.Serialization;
using TapTitaner.Controllers;

namespace TapTitaner.Models;

public class StoredData
{
    [JsonPropertyName("_monster")] public Monster Monster { get; set; }
    [JsonPropertyName("_hero")] public Hero Hero { get; set; }
}