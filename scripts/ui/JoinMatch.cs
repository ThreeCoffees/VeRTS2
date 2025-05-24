using Godot;
using System;

public partial class JoinMatch : Control
{
    private Button GoBackButton;
    private Button JoinButton;

    private LineEdit ServerIpInput;
    private SpinBox PortInput;
    private LineEdit NicknameInput;
    
    private PackedScene MainMenu;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        GoBackButton = GetNode<Button>("GoBackButton");
        JoinButton = GetNode<Button>("MenuContainer/JoinButton");

        ServerIpInput = GetNode<LineEdit>("MenuContainer/JoinDataContainer/ServerIpInput");
        PortInput = GetNode<SpinBox>("MenuContainer/JoinDataContainer/PortInput");
        NicknameInput = GetNode<LineEdit>("MenuContainer/JoinDataContainer/NicknameInput");

        MainMenu = GD.Load<PackedScene>("res://scenes/ui/main_menu.tscn");

        JoinButton.GrabFocus();

        GoBackButton.Pressed += OnGoBackButtonPressed;
        JoinButton.Pressed += OnJoinButtonPressed;
	}

    private void OnGoBackButtonPressed(){
        GetTree().ChangeSceneToPacked(MainMenu);
    }

    private void OnJoinButtonPressed(){
        GD.Print("Join Game");
    
        MultiplayerManager.Instance.JoinServer(ServerIpInput.Text, (int)PortInput.Value);
    }
}
