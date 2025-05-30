using Godot;
using System;

public partial class Player : Node2D
{
    [Export]
    public double MoveSpeed = 100.0;

    [Export]
    public int Id;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        GetNode<MultiplayerSynchronizer>("MultiplayerSynchronizer").SetMultiplayerAuthority(Id);
        if(Multiplayer.GetUniqueId() == Id){
            GetNode<Camera2D>("Camera2D").MakeCurrent();
        } else {
            GetNode<Camera2D>("Camera2D").Enabled = false;
        }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if(Id != Multiplayer.GetUniqueId()){
            return;
        }
        Vector2 Direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Position += Direction * (float)(MoveSpeed * delta);
	}
}
