using Godot;
using System;

public partial class HostMatch : Control
{
    [Export]
    private Button GoBackButton;
    [Export]
    private Button HostButton;

    [Export]
    private SpinBox PortInput;
    [Export]
    private SpinBox PlayerCountInput;
    [Export]
    private SpinBox ArmySizeInput;
    
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        HostButton.GrabFocus();

        GoBackButton.Pressed += OnGoBackButtonPressed;
        HostButton.Pressed += OnHostButtonPressed;
	}

    private void OnGoBackButtonPressed(){
        SceneManager.Instance.Scene = SceneManager.SceneEnum.MainMenu;
    }

    private void OnHostButtonPressed(){
        GD.Print("Host Game");
        MultiplayerManager.Instance.StartServer((int)PortInput.Value, (int)PlayerCountInput.Value);
    }
}
