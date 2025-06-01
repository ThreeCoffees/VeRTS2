using Godot;
using System;

public partial class ShipSelectionMenu : Control
{
	[Export(PropertyHint.Dir)]
	public string ShipsDirectory;

	[Export]
	public VBoxContainer ShipsContainer;
	[Export]
	public PackedScene ShipButtonScene;

	public override void _Ready()
	{
		spawnShipButtons();
	}

	private void spawnShipButtons() {
		foreach(string fileName in DirAccess.GetFilesAt(ShipsDirectory)){
			GD.Print(ShipsDirectory+"/"+fileName);
			ShipSelectionButton shipBtn = ShipButtonScene.Instantiate<ShipSelectionButton>();
			shipBtn.ShipData = (ShipDataResource)GD.Load(ShipsDirectory+"/"+fileName);

			ShipsContainer.AddChild(shipBtn);
		}
	}
}
