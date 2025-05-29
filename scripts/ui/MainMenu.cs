using Godot;
using System;

public partial class MainMenu : Control
{
    private Button JoinButton;
    private Button HostButton;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        JoinButton = GetNode<Button>("MenuContainer/ButtonsContainer/JoinButton");
        HostButton = GetNode<Button>("MenuContainer/ButtonsContainer/HostButton");

        JoinButton.GrabFocus();

        JoinButton.Pressed += OnJoinButtonPressed;
        HostButton.Pressed += OnHostButtonPressed;
	}
    
    public override void _ExitTree(){
        JoinButton.Pressed -= OnJoinButtonPressed;
        HostButton.Pressed -= OnHostButtonPressed;
    }

    private void OnHostButtonPressed(){
        SceneManager.Instance.Scene = SceneManager.SceneEnum.HostMatchMenu;
    }

    private void OnJoinButtonPressed(){
        SceneManager.Instance.Scene = SceneManager.SceneEnum.JoinMatchMenu;
    }
}
