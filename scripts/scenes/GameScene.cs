using Godot;
using System;

namespace VillagePriestGame.Scenes
{
    public class GameScene : Control
    {
        // Declare member variables here. Examples:
        private Core.Village CurrentVillage;
        public string Diary = "";

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            
        }

        public void OpenHouseMenu(string OwnerName)
        {
            GD.Print("Menu Opened for " + OwnerName);
        }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    };
}