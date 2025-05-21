using Godot;
using System;

public partial class JoinMatch : Control
{
    private Button GoBackButton;
    private Button JoinButton;
    
    private PackedScene MainMenu;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        GoBackButton = GetNode<Button>("GoBackButton");
        JoinButton = GetNode<Button>("MenuContainer/JoinButton");
        MainMenu = GD.Load<PackedScene>("res://scenes/ui/main_menu.tscn");

        JoinButton.GrabFocus();

        GoBackButton.Pressed += OnGoBackButtonPressed;
        JoinButton.Pressed += OnJoinButtonPressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    private void OnGoBackButtonPressed(){
        GetTree().ChangeSceneToPacked(MainMenu);
    }

    private void OnJoinButtonPressed(){
        GD.Print("Join Game");
    }
}
