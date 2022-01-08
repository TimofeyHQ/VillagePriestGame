using System.Collections.Generic;

namespace VillagePriestGame.Core
{
    public class Village
    {
        public Villager[] Community;
        public uint CurrentDay {get; private set; }
        public Characteristic TimeOfDay {get; private set; }

        public void PassTime()
        {
            if (TimeOfDay > ++TimeOfDay)
                CurrentDay++;
        }

        public Villager GetVillager(string name)
        {
            foreach (var villager in Community)
                if (villager.VillagerName == name)
                    return villager;
            return null;
        }
    };
}