using Newtonsoft.Json;
using System.IO;

namespace VillagePriestGame.Core
{
    public class VillageCreator
    {
        public Village ReadVillageFromJson(string pathToJson)
        {
            var jsonString = File.ReadAllText(pathToJson);
            var jsonData = JsonConvert.DeserializeObject<Village>(jsonString);
            return jsonData;
        }

        public void WriteVillageToJson(Village village, string pathToJson)
        {
            string jsonedVillage = JsonConvert.SerializeObject(village);
            File.WriteAllText(pathToJson, jsonedVillage);
        }
    };
}