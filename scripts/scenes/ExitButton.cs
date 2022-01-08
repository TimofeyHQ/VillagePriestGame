using Godot;
using System;

public class ExitButton : TextureButton
{
    // Declare member variables here. Examples:
    private float Resize = 0.75F;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }
    
    private void OnExitButtonUp()
    {
        this.RectScale /= Resize;
        var parent = GetParent() as Control;
        parent.Visible = false;
    }

    private void OnExitButtonDown()
    {
        this.RectScale *= Resize;
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
