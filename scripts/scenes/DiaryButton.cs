using Godot;
using System;

namespace VillagePriestGame.Scenes
{
    public class DiaryButton : TextureButton
    {
        // Declare member variables here. Examples:
        private DiaryScene Diary;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            
        }

        private void OnDiaryButtonUp()
        {
            if (Diary == null)
                Diary = FindNode("DiaryScene") as DiaryScene;
            GD.Print(Diary == null);
        }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    }

}