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

        if(OS.HasFeature("dedicated_server")){
            var args = OS.GetCmdlineUserArgs();
            int port = MultiplayerManager.DefaultPort;
            int playerCount = MultiplayerManager.DefaultPlayerCount;
            foreach(var arg in args){
                var keyValue = arg.Split("=");
                switch(keyValue[0]){
                    case "port":
                        port = keyValue[1].ToInt();
                        break;
                    case "player-count":
                        playerCount = keyValue[1].ToInt();
                        break;
                    default:
                        GD.PrintErr("Unknown User Arg");
                        break;
                }
            }
            MultiplayerManager.Instance.CallDeferred(MultiplayerManager.MethodName.StartServer, port, playerCount);
        }
	}
    
    public override void _ExitTree(){
        JoinButton.Pressed -= OnJoinButtonPressed;
        HostButton.Pressed -= OnHostButtonPressed;
    }

    private void OnHostButtonPressed(){
        GetTree().ChangeSceneToPacked(HostMenu);
    }

    private void OnJoinButtonPressed(){
        GetTree().ChangeSceneToPacked(JoinMenu);
    }
}
