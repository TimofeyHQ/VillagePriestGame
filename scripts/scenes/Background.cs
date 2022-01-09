using Godot;
using System;

namespace VillagePriestGame.Scenes
{
    public class Background : TextureRect
    {
        // Declare member variables here. Examples:
        // private int a = 2;
        // private string b = "text";

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            
        }

        private void OnTimeChange(int time)
        {
            Texture picture;
            if (time == 0) picture = GD.Load<Texture>("./images/background/morning.png");
            else if (time == 1) picture = GD.Load<Texture>("./images/background/day.png");
            else if (time == 2) picture = GD.Load<Texture>("./images/background/evening.png");
            else picture = GD.Load<Texture>("./images/background/night.png");
            this.Texture = picture;
        }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    }

}
