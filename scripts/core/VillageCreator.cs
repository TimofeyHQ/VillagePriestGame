using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace VillagePriestGame.Core
{
    public class VillageCreator
    {
        public Village ReadVillageFromJson(string pathToJson)
        {
            var jsonString = File.ReadAllText(pathToJson);
            var jsonData = JsonSerializer.Deserialize<Village>(jsonString);
            return jsonData;
        }
    };
}