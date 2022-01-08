using Godot;
using System;

namespace VillagePriestGame.Scenes
{
    public class DiaryScene : Control
    {
        // Declare member variables here. Examples:
        private RichTextLabel Content;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {

        }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
        private void ReadDiary()
        {
            var GameScene = (GetNode("../../../GameScene") as GameScene);
            Content.Text = GameScene.Diary;
        }

        private void DiaryContentReady()
        {
            Content = GetChild(0).GetChild(0).GetChild(0) as RichTextLabel;   
        }
        private void OpenDiary()
        {
            this.Visible = true;
            ReadDiary();
        }
    }
}
