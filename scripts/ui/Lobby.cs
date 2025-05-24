using Godot;
using System;

public partial class Lobby : Control
{
    private Button DisconnectButton;
    private Button StartGameButton;
    private VBoxContainer PlayerList;

    private PackedScene GameScene;

    public override void _Ready(){
        DisconnectButton = GetNode<Button>("DisconnectButton");
        StartGameButton = GetNode<Button>("MenuContainer/StartGameButton");
        PlayerList = GetNode<VBoxContainer>("MenuContainer/ScrollContainer/PlayerList");

        GameScene = GD.Load<PackedScene>("res://scenes/game.tscn");
        
        StartGameButton.GrabFocus();

        DisconnectButton.Pressed += OnDisconnectButtonPressed;
        StartGameButton.Pressed += MultiplayerManager.Instance.OnStartGameButtonPressed;
        MultiplayerManager.Instance.PlayerConnected += OnPlayerConnected;
        MultiplayerManager.Instance.PlayerDisconnected += OnPlayerDisconnected;
    }

    private void OnPlayerDisconnected(long Id)
    {
        Label playerLabel = PlayerList.GetNode<Label>("playerLabel_" + Id);
        if(playerLabel is not null){
            playerLabel.QueueFree();
        }
    }

    private void OnPlayerConnected(long Id, PlayerInfo playerInfo)
    {
        GD.Print(Multiplayer.GetInstanceId(), Id);
        Label playerLabel = new Label();
        playerLabel.Text = playerInfo.Name + "_"+ playerInfo.Id;
        playerLabel.Name = "playerLabel_" + playerInfo.Id;

        PlayerList.AddChild(playerLabel);
    }

    private void OnDisconnectButtonPressed()
    {
        GD.Print("Leaving Game");

        MultiplayerManager.Instance.PlayerConnected -= OnPlayerConnected;
        MultiplayerManager.Instance.PlayerDisconnected -= OnPlayerDisconnected;

        Multiplayer.MultiplayerPeer.Close();
    }
}
