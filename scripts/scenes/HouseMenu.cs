using Godot;
using System;

namespace VillagePriestGame.Scenes
{
    public class HouseMenu : Control
    {
        // Declare member variables here. Examples:
        Core.Villager Villager;
        Label NameTagLabel;
        Label GoodsLabel;
        Portrait Portrait;
        [Signal]
        public delegate void AddDiaryRecordSignal(string record);
        
        private string GoodsToString()
        {
            string result = "Has", temp = "";
            foreach (var farm in Villager.FarmStock)
                if (farm.Value > 0)
                    if (farm.Value.CurrentValue == 1)
                    {
                        if (temp != "")
                            temp += ',';
                        temp += (" a " + farm.Key.ToString());
                    }
                    else
                    {
                        if (temp != "")
                            temp += ',';
                        if (farm.Key != Enums.Farm.Greens)
                            temp += (farm.Value.CurrentValue + " of " + farm.Key.ToString() + "s");
                        else
                            temp += (farm.Value.CurrentValue + " of " + farm.Key.ToString());
                    }
            if (temp == "")
                result += " nothing of animals or green.";
            else
                result += temp;
            result += ".\n";
            foreach (var resourse in Villager.Resources)
            {
                result += ("Has " + resourse.Key.ToString() + " enough for " + Decimal.Truncate(100 * (decimal)resourse.Value.CurrentValue / 3)/100 + " days.\n");
            }
            return result;
        }

        private decimal ComputeProbability(string NameOfVillager)
        {
            return (this.Villager.Relationships["Priest"].GetPercent() + this.Villager.Relationships[NameOfVillager].GetPercent()) / 2;
        }
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            
        }

        public void OpenMenu(string villagerName)
        {
            var Game = (GetParent().GetParent() as GameScene);
            Villager = Game.GetVillager(villagerName);
            NameTagLabel.Text = Villager.VillagerName + " The " +  Villager.Role;
            GoodsLabel.Text = GoodsToString();
            Portrait.Texture = GD.Load<Texture>("./images/interface/portraits/" + Villager.VillagerName + ".png");

            this.Visible = true;
        }

        private void OnNameAndRoleLabelReady()
        {
            NameTagLabel = GetChild(0).GetChild(0).GetChild(0).GetChild(0) as Label;
        }
        private void OnGoodsLabelReady()
        {
            GoodsLabel = GetChild(0).GetChild(0).GetChild(0).GetChild(1)  as Label;
        }

        private void OnPortraitReady()
        {
            Portrait = GetChild(0) as Portrait;
        }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    }
}
