using Godot;
using System;

public partial class JoinMatch : Control
{
    [Export]
    private Button GoBackButton;
    [Export]
    private Button JoinButton;

    [Export]
    private LineEdit ServerIpInput;
    [Export]
    private SpinBox PortInput;
    [Export]
    private LineEdit NicknameInput;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        JoinButton.GrabFocus();

        GoBackButton.Pressed += OnGoBackButtonPressed;
        JoinButton.Pressed += OnJoinButtonPressed;
	}

    private void OnGoBackButtonPressed(){
        SceneManager.Instance.Scene = SceneManager.SceneEnum.MainMenu;
    }

    private void OnJoinButtonPressed(){
        GD.Print("Join Game");
    
        MultiplayerManager.Instance.JoinServer(ServerIpInput.Text, (int)PortInput.Value);
    }
}
