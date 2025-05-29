using Godot;
using System;

public partial class HostMatch : Control
{
    private Button GoBackButton;
    private Button HostButton;

    private SpinBox PortInput;
    private SpinBox PlayerCountInput;
    private SpinBox ArmySizeInput;
    
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        GoBackButton = GetNode<Button>("GoBackButton");
        HostButton = GetNode<Button>("MenuContainer/HostButton");

        PortInput = GetNode<SpinBox>("MenuContainer/HostDataContainer/PortInput");
        PlayerCountInput = GetNode<SpinBox>("MenuContainer/HostDataContainer/PlayerCountInput");
        ArmySizeInput = GetNode<SpinBox>("MenuContainer/HostDataContainer/ArmySizeInput");

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
