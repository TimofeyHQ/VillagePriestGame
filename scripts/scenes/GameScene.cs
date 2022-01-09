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
        [Signal]
        public delegate void TransmitDiary(string diary);
        [Signal]
        public delegate void OpenHouseMenuSignal(string villagerName);

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            AvaliableActions = new Core.Characteristic(2, 2);
            var VillageReader = new Core.VillageCreator();
            CurrentVillage = VillageReader.ReadVillageFromJson("Village.json");
            
        }

        public Core.Villager GetVillager(string name)
        {
            return CurrentVillage.GetVillager(name);
        }
        public void AddRecordToTheDiary(string record)
        {
            Diary += record;
        }

        public void OpenHouseMenu(string OwnerRole, HouseButton caller)
        {
            if (OwnerRole == "Priest" || OwnerRole == "God")
            {
                CurrentVillage.PassTime();
                if (CurrentVillage.TimeOfDay < 2)
                    AvaliableActions += AvaliableActions.MaxValue;
                EmitSignal(nameof(OnTimeChange), CurrentVillage.TimeOfDay.CurrentValue);
            }
            else if (AvaliableActions > 0) 
            {
                AvaliableActions --;
                if (caller.Owner == null)
                {
                    foreach (var villager in CurrentVillage.Community)
                        if (villager.Role == OwnerRole)
                            caller.Owner = villager;
                }
                EmitSignal(nameof(OpenHouseMenuSignal), caller.Owner.VillagerName);
            }
        }

        public void onBackgroundReady()
        {
            Background = GetChild(0).GetChild(0) as Background;
            this.Connect(nameof(OnTimeChange), Background, "OnTimeChange");
        }

        public void OnHouseMenuReady()
        {
            this.Connect(nameof(OpenHouseMenuSignal), GetChild(2).GetChild(1) as HouseMenu, "OpenMenu");
        }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    };
}