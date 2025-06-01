using Godot;
using System;

public partial class Ship : CharacterBody2D
{
    private ShipDataResource shipData;
    public ShipDataResource ShipData
    {
        get { return shipData; }
        set
        {
            shipData = value;
            GD.Print(shipData.ShipName);
            updateShipData();
        }
    }

    [Export]
    private Sprite2D sprite;
    [Export]
    private CollisionShape2D collisionShape2D;
    [Export]
    private Label nameLabel;

    public int playerId;

    private void updateShipData()
    {
        sprite.Texture = shipData.ShipTexture;
        //shipCollisionShape2D = shipData.ShipCollisionShape2D;
        nameLabel.Text = shipData.ShipName;
    }
}
