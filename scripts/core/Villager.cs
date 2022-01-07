using System;
using System.Collections.Generic;

public class Villager
{
    public String VillagerName { get; set; }
    public Dictionary <string, Characteristic> Relationships;
    public Dictionary <Animal, Characteristic> Animals;
    public Dictionary <Resource, Characteristic> Resources;
    public Dictionary <Skill, Characteristic> Skills;
    public Characteristic StateOfTheHouse;
    public Villager(string name)
    {
        this.VillagerName = name;
    }

    public bool Decide(string villagerName)
    {
        var willingness = 100 * 
            (this.Relationships["Priest"].GetPercent() + this.Relationships[villagerName].GetPercent())
            / 2;
        if (willingness >= new Random().Next(100))
            return true;
        else
            return false;
    }

    public void Slaughter(Animal animal, int count)
    {
        if (this.Animals[animal] - count >= 0)
        {
            int meat_k = 0;
            this.Animals[animal] -= count;
            switch (animal)
            {
                case Animal.Cow: meat_k = 25; break; 
                case Animal.Goat: meat_k = 10; break; 
                case Animal.Chicken: meat_k = 3; break; 
                case Animal.Horse: meat_k = 20; break;
            }
            for (int i = 0; i < count; i ++)
                this.Resources[Resource.Food] +=
                (int)((this.Skills[Skill.Butchery].GetPercent() + 1) * meat_k / 2);
        } 
    }

    public void Harvest(Animal animal)
    {   
        if (this.Animals[animal] > 0)
        {
        var rand = new Random();
        int food_k, water_k;
        switch (animal)
        {
                case Animal.Chicken: food_k = 3; water_k = 1; break;
                case Animal.Cow: food_k = 6; water_k = 6; break;
                case Animal.Goat: food_k = 4; water_k = 4; break;
                case Animal.Horse: food_k = 1; water_k = 1; break;
                default: food_k = 1; water_k = 1; break;
        }
        for (int i = 0; this.Animals[animal] > i; i ++)
            if (this.Skills[Skill.Foraging] >= rand.Next(100))
            {
                this.Resources[Resource.Food] += rand.Next(food_k);
                this.Resources[Resource.Water] += rand.Next(water_k);
            }
        }
    }

    public void Give(Villager taker, Animal animal, int count)
    {
        if (this.Animals[animal] >= count)
        {
            taker.Animals[animal] += count;
            this.Animals[animal] -= count;
        }
    }
};