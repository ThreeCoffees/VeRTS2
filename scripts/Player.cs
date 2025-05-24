using Godot;
using System;

public partial class Player : Node2D
{
    [Export]
    public double MoveSpeed = 100.0;

    [Export]
    public long Id;
    [Export]
    public String Nickname;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        Vector2 Direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Position = Direction * (float)(MoveSpeed * delta);
	}
}
