using Godot;
using System;

public partial class HostMatch : Control
{
    private Button GoBackButton;
    private Button HostButton;
    
    private PackedScene MainMenu;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        GoBackButton = GetNode<Button>("GoBackButton");
        HostButton = GetNode<Button>("MenuContainer/HostButton");
        MainMenu = GD.Load<PackedScene>("res://scenes/ui/main_menu.tscn");

        HostButton.GrabFocus();

        GoBackButton.Pressed += OnGoBackButtonPressed;
        HostButton.Pressed += OnHostButtonPressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    private void OnGoBackButtonPressed(){
        GetTree().ChangeSceneToPacked(MainMenu);
    }

    private void OnHostButtonPressed(){
        GD.Print("Host Game");
    }
}
