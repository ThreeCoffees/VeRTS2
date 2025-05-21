using Godot;
using System;

public partial class MainMenu : Control
{
    private Button JoinButton;
    private Button HostButton;

    private PackedScene JoinMenu;
    private PackedScene HostMenu;
    
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        JoinButton = GetNode<Button>("MenuContainer/ButtonsContainer/JoinButton");
        HostButton = GetNode<Button>("MenuContainer/ButtonsContainer/HostButton");
        JoinMenu = GD.Load<PackedScene>("res://scenes/ui/join_match.tscn");
        HostMenu = GD.Load<PackedScene>("res://scenes/ui/host_match.tscn");

        JoinButton.GrabFocus();

        JoinButton.Pressed += OnJoinButtonPressed;
        HostButton.Pressed += OnHostButtonPressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    private void OnHostButtonPressed(){
        GetTree().ChangeSceneToPacked(HostMenu);
    }

    private void OnJoinButtonPressed(){
        GetTree().ChangeSceneToPacked(JoinMenu);
    }
}
