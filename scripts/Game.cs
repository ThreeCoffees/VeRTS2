using Godot;
using System;

public partial class Game : Node2D
{
    private PackedScene playerScene;
    private Node2D playerSpawn;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        playerScene = GD.Load<PackedScene>("res://scenes/player.tscn");
        playerSpawn = GetNode<Node2D>("PlayerSpawn");

        foreach(PlayerInfo pi in MultiplayerManager.Players.Values){
            Player currPlayer = playerScene.Instantiate<Player>();
            currPlayer.Name = "player_" + pi.Id;
            currPlayer.Id = (int)pi.Id;
            playerSpawn.AddChild(currPlayer);
        }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
