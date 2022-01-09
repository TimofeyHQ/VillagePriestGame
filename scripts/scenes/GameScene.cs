using Godot;
using System;

namespace VillagePriestGame.Scenes
{
    public class GameScene : Control
    {
        // Declare member variables here. Examples:
        private Core.Village CurrentVillage;
        public string Diary = "";
        public Core.Characteristic AvaliableActions;
        private Background Background;
        [Signal]
        public delegate void OnTimeChange(int DayTime);

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            AvaliableActions = new Core.Characteristic(2, 2);
            var VillageReader = new Core.VillageCreator();
            CurrentVillage = VillageReader.ReadVillageFromJson("Village.json");
        }

        public void AddRecordToTheDiary(string record)
        {
            Diary += record;
        }

        public void OpenHouseMenu(string OwnerRole, HouseButton caller)
        {
            if (OwnerRole == "Priest")
            {
                CurrentVillage.PassTime();
                if (CurrentVillage.TimeOfDay < 2)
                    AvaliableActions += AvaliableActions.MaxValue;
                EmitSignal(nameof(OnTimeChange), CurrentVillage.TimeOfDay.CurrentValue);
            }
            else if (caller.Owner == null)
            {
                foreach (var villager in CurrentVillage.Community)
                    if (villager.Role == OwnerRole)
                        caller.Owner = villager;
            }
            // else                 
        }

        public void onBackgroundReady()
        {
            Background = GetChild(0).GetChild(0) as Background;
            this.Connect(nameof(OnTimeChange), Background, "OnTimeChange");
        }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    };
}