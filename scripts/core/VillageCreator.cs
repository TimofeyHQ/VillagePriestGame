using Newtonsoft.Json;
using Godot;

namespace VillagePriestGame.Core
{
    public class VillageCreator
    {
        public Village ReadVillageFromJson(string pathToJson)
        {
            var file = new File();
            file.Open("res://" + pathToJson, File.ModeFlags.Read);
            var jsonString = file.GetAsText();
            var jsonData = JsonConvert.DeserializeObject<Village>(jsonString);
            file.Close();
            return jsonData;
        }

        public void WriteVillageToJson(Village village, string pathToJson)
        {
            string jsonedVillage = JsonConvert.SerializeObject(village);
            var file = new File();
            file.Open("res://" + pathToJson, File.ModeFlags.Write);
            file.StoreString(jsonedVillage);
            file.Close();
        }
    };
}