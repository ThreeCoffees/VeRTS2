using Godot;
using System;

[GlobalClass]
public partial class ShipDataResource : Resource
{
    [Export]
    public string ShipName = "no name";
    [Export]
    public string ShipDescription = "no description";
    [Export]
    public int ShipCost = 0;
    [Export]
    public Texture2D ShipTexture = null;
}
