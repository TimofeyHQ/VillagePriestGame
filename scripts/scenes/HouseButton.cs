using Godot;
using System;

public class HouseButton : TextureButton
{
    // Declare member variables here. Examples:
    [Export]
    private string OwnerVillagerName = "Priest";
    [Signal]
    public delegate void HouseSelectedSignal(string villagerName);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.Connect(nameof(HouseSelectedSignal), GetParent(), "OpenHouseMenu");
    }

    public void OnHouseButtonUp()
    {
        EmitSignal(nameof(HouseSelectedSignal), OwnerVillagerName);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
