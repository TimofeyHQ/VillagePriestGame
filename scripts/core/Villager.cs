using System;
using System.Collections.Generic;

namespace VillagePriestGame.Core
{
    public class Villager
    {
        public String VillagerName { get; set; }
        public Dictionary <string, Characteristic> Relationships;
        public Dictionary <Enums.Farm, Characteristic> FarmStock;
        public Dictionary <Enums.Resource, Characteristic> Resources;
        public Dictionary <Enums.Skill, Characteristic> Skills;
        readonly static int WaterDailyNeed = 3;
        readonly static int FoodDailyNeed = 3;
        readonly static int WoodDailyNeed = 3;
        private static Dictionary <Enums.Farm, (int Food, int Water)> Coeficients; 
        private bool isHelpingSomeone; 
        public Villager(string name)
        {
            this.VillagerName = name;
            isHelpingSomeone = false;
            if (Coeficients == null)
                {
                    Coeficients = new();
                    Coeficients[Enums.Farm.Chicken] = (2, 0);
                    Coeficients[Enums.Farm.Cow] = (5, 5);
                    Coeficients[Enums.Farm.Goat] = (3, 3);
                    Coeficients[Enums.Farm.Greens] = (1, 1);
                    Coeficients[Enums.Farm.Horse] = (0, 0);
                }
        }

        public bool Decide(string villagerName)
        {
            var willingness = 100 * 
                (this.Relationships["Priest"].GetPercent() + this.Relationships[villagerName].GetPercent())
                / 2;
            if (willingness >= new Random().Next(100))
            {
                isHelpingSomeone = true;
                return true;
            }
            else
                return false;
        }

        public void Slaughter(Enums.Farm animal, int count)
        {
            if (animal == Enums.Farm.Greens)
                throw new Exception("Greens are not animals and can't be slaughtered.");
            if (this.FarmStock[animal] - count >= 0)
            {
                int meat_k = 0;
                this.FarmStock[animal] -= count;
                switch (animal)
                {
                    case Enums.Farm.Cow: meat_k = 25; break; 
                    case Enums.Farm.Goat: meat_k = 10; break; 
                    case Enums.Farm.Chicken: meat_k = 3; break; 
                    case Enums.Farm.Horse: meat_k = 20; break;
                }
                for (int i = 0; i < count; i ++)
                    this.Resources[Enums.Resource.Food] +=
                    (int)((this.Skills[Enums.Skill.Butchery].GetPercent() + 1) * meat_k / 2);
            } 
        }

        public void Harvest(Enums.Farm animalOrGreen)
        {   var farmingSkill = this.Skills[Enums.Skill.Farming].GetPercent() * 100;
            if (this.FarmStock[animalOrGreen] > 0)
            {
            var rand = new Random();
            for (int i = 0; this.FarmStock[animalOrGreen] > i; i ++)
                if (farmingSkill >= rand.Next(100))
                {
                    this.Resources[Enums.Resource.Food] += rand.Next(Coeficients[animalOrGreen].Food + 1);
                    this.Resources[Enums.Resource.Water] += rand.Next(Coeficients[animalOrGreen].Water + 1);
                }
            }
        }

        public void GetWood()
        {
            var rand = new Random();
            var woodworkSkill = this.Skills[Enums.Skill.Woodwork].GetPercent() * 100;
            for (int i = 0; i < 6; i ++)
                if (woodworkSkill >= rand.Next(100))
                    this.Resources[Enums.Resource.Wood] ++;
        }

        public void Forage()
        {
            var rand = new Random();
            var foragingSkill = this.Skills[Enums.Skill.Foraging].GetPercent() * 100;
            for (int i = 0; i < 5; i ++)
            {
                if (foragingSkill >= rand.Next(100))
                    this.Resources[Enums.Resource.Food] ++;
                if (foragingSkill >= rand.Next(100))
                    this.Resources[Enums.Resource.Water] ++;
            }
        }

        public void ForageForWater()
        {
            var rand = new Random();
            var foragingSkill = this.Skills[Enums.Skill.Foraging].GetPercent() * 100;
            for (int i = 0; i < 7; i ++)
            {
                if (foragingSkill >= rand.Next(100))
                    this.Resources[Enums.Resource.Water] ++;
            }
        }

        public void Hunt()
        {
            var HuntingSkill = this.Skills[Enums.Skill.Hunting].GetPercent() * 100;
            var rand = new Random();
            for (int i = 0; i < 5; i ++)
                if (HuntingSkill >= rand.Next(100))
                    this.Resources[Enums.Resource.Food] ++;
        }

        public void Fish()
        {
            var FishingSkill = this.Skills[Enums.Skill.Fishing].GetPercent() * 100;
            var rand = new Random();
            for (int i = 0; i < 5; i ++)
                if (FishingSkill >= rand.Next(100))
                    this.Resources[Enums.Resource.Food] ++;
        }

        public void DayRoutine()
        {
            int curFood = this.Resources[Enums.Resource.Food].CurrentValue, 
                curWater = this.Resources[Enums.Resource.Water].CurrentValue,
                curWood = this.Resources[Enums.Resource.Wood].CurrentValue;
            bool needWater = false, needFood = false, needWood = false;
            var farmingSkill = this.Skills[Enums.Skill.Farming].GetPercent();
            var foragingSkill = this.Skills[Enums.Skill.Foraging].GetPercent();
            var huntingSkill = this.Skills[Enums.Skill.Hunting].GetPercent();
            var fishingSkill = this.Skills[Enums.Skill.Fishing].GetPercent();
            if (this.Resources[Enums.Resource.Food] < FoodDailyNeed)
                needFood = true;
            if (this.Resources[Enums.Resource.Water] < WaterDailyNeed)
                needWater = true;
            if (this.Resources[Enums.Resource.Wood] < WoodDailyNeed)
                needWood = true;
            if (!needFood || !needWater || !needWood)
                {
                    int waterCoef, foodCoef, woodCoef;
                    waterCoef = (this.Resources[Enums.Resource.Water].CurrentValue - WaterDailyNeed) 
                        / WaterDailyNeed;
                    foodCoef = (this.Resources[Enums.Resource.Food].CurrentValue - FoodDailyNeed) 
                        / FoodDailyNeed;
                    woodCoef = (this.Resources[Enums.Resource.Wood].CurrentValue - WoodDailyNeed) 
                        / WoodDailyNeed;
                    if (waterCoef <= foodCoef)
                        if (waterCoef <= woodCoef)
                            needWater = true;
                        else
                            needWood = true;
                    else
                        if (foodCoef <= woodCoef)
                            needFood = true;
                        else
                            needWood = true;
                }
            if (needFood && needWater) 
            {
                decimal water = 0, food = 0;
                Enums.Farm bestSource = 0;
                foreach (var source in this.FarmStock)
                {
                    var propableFood = farmingSkill * source.Value.CurrentValue * Coeficients[source.Key].Food;
                    var propableWater = farmingSkill * source.Value.CurrentValue * Coeficients[source.Key].Water;
                    if ((propableFood >= FoodDailyNeed || propableWater >= WaterDailyNeed)
                        && propableFood >= food && propableWater >= water)
                        {
                            water = propableWater;
                            food = propableFood;
                            bestSource = source.Key;
                        }
                }
                if (water == 0 && food == 0)
                {
                    Forage();
                }
                else
                {
                    Harvest(bestSource);
                }
            }
            else if (needWater)
            {
                decimal water = 0;
                Enums.Farm bestSource = 0;
                foreach (var source in this.FarmStock)
                {
                    var propableWater = farmingSkill * source.Value.CurrentValue * Coeficients[source.Key].Water;
                    if (propableWater >= WaterDailyNeed && propableWater >= water)
                        {
                            water = propableWater;
                            bestSource = source.Key;
                        }
                }
                if (water == 0)
                {
                    ForageForWater();
                }
                else
                {
                    Harvest(bestSource);
                }
            }
            else if (needFood)
            {
                Enums.Farm bestSource = 0;
                (decimal food, Enums.Skill skill) best = (0, Enums.Skill.Farming);
                foreach (var source in this.FarmStock)
                {
                    var propableFood = farmingSkill * source.Value.CurrentValue * Coeficients[source.Key].Food;
                    if (propableFood >= WaterDailyNeed && propableFood >= best.food)
                        {
                            best.food = propableFood;
                            bestSource = source.Key;
                            best.skill = Enums.Skill.Farming;
                        }
                }
                var skill = fishingSkill >= huntingSkill ? 
                    (value: fishingSkill, skill: Enums.Skill.Fishing)
                    : (value: huntingSkill, skill: Enums.Skill.Hunting);
                if (best.food <= skill.value * 5)
                {
                    best.food = skill.value * 5;
                    best.skill = skill.skill;
                }                            
                if (best.food == 0)
                {
                    Forage();
                }
                else if (best.skill == Enums.Skill.Farming)
                {
                    Harvest(bestSource);
                }
                else if (best.skill == Enums.Skill.Fishing)
                {
                    Fish();
                }
                else if (best.skill == Enums.Skill.Hunting)
                {
                    Hunt();
                }
            }
            else GetWood();
        }
        public void Give(Villager taker,  Enums.Farm animalOrGreen, int count)
        {
            if (this.FarmStock[animalOrGreen] >= count)
            {
                taker.FarmStock[animalOrGreen] += count;
                this.FarmStock[animalOrGreen] -= count;
            }
        }
    };
}