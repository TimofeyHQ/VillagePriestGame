using Godot;
using System;
namespace VillagePriestGame.Scenes
{
    public class HouseButton : TextureButton
    {
        // Declare member variables here. Examples:
        [Export]
        private string OwnerVillagerName;
        public Core.Villager Owner { get; set; }
        [Signal]
        public delegate void HouseSelectedSignal(string villagerName, HouseButton sender);

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            this.Connect(nameof(HouseSelectedSignal), GetParent().GetParent(), "OpenHouseMenu");
        }

        public void OnHouseButtonUp()
        {
            EmitSignal(nameof(HouseSelectedSignal), OwnerVillagerName, this);
        }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    };
}