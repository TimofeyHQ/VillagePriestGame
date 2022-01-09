using System;

namespace VillagePriestGame.Core
{
    public class Village
    {
        public Villager[] Community;
        public uint CurrentDay {get; private set; }
        public Characteristic TimeOfDay {get; private set; }

        public void PassTime()
        {   
            if (TimeOfDay.CurrentValue == 1)
                foreach (var villager in Community)
                    if (!villager.isHelpingSomeone)
                        villager.DayRoutine();
            if (TimeOfDay.CurrentValue == TimeOfDay.MaxValue)
            {
                CurrentDay++;
                TimeOfDay -= TimeOfDay.MaxValue;
            }
            else 
                TimeOfDay ++;
        }

        public Villager GetVillager(string name)
        {
            foreach (var villager in Community)
                if (villager.VillagerName == name)
                    return villager;
            return null;
        }

        public Village()
        {
            Community = new Villager[7];
            CurrentDay = 0;
            TimeOfDay = new Characteristic(3);
        }
    };
}