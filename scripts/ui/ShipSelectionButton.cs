using Godot;
using System;

public partial class ShipSelectionButton : MarginContainer
{
    private ShipDataResource shipData;
    public ShipDataResource ShipData {
        get {return shipData;}
        set {
            shipData = value;
            if(shipData != null){
                updateData();
            }
        }
    }

    [Export]
    private Label ShipNameLabel;
    [Export]
    private Label ShipDataLabel;
    [Export]
    private Label ShipCostLabel;
    [Export]
    private TextureRect ShipIcon;

    private void updateData(){
        ShipNameLabel.Text = shipData.ShipName;
        ShipDataLabel.Text = shipData.ShipDescription;
        ShipCostLabel.Text = shipData.ShipCost.ToString();
        ShipIcon.Texture = shipData.ShipTexture;
    }
}
