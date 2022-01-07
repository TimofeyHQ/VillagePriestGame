using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

public class VillageCreator
{
    public Village ReadVillageFromJson(string pathToJson)
    {
        var jsonString = File.ReadAllText(pathToJson);
        var jsonData = JsonSerializer.Deserialize<Village>(jsonString);
        return jsonData;
    }
}