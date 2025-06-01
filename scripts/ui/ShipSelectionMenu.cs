using Godot;
using System;

public partial class ShipSelectionMenu : Control
{
	[Export(PropertyHint.Dir)]
	public string ShipsDirectory;

    public static Godot.Collections.Dictionary<string, ShipDataResource> shipTypesDict = new Godot.Collections.Dictionary<string, ShipDataResource>();

	[Export]
	public VBoxContainer ShipsContainer;
	[Export]
	public PackedScene ShipButtonScene;
    [Export]
    private ButtonGroup shipSelectionBtnGroup;
    [Export]
    private PackedScene shipScene;

    [Export]
    private Node2D shipSpawn;

	public override void _Ready()
	{
        fillShipTypesDict();
		spawnShipButtons();
	}

    public void fillShipTypesDict(){
		foreach(string fileName in DirAccess.GetFilesAt(ShipsDirectory)){
			shipTypesDict.Add(ShipsDirectory+"/"+fileName, (ShipDataResource)GD.Load(ShipsDirectory+"/"+fileName));
		}
    }

    public override void _UnhandledInput(InputEvent @event){
        if(@event.IsActionPressed("pick_primary")){
            BaseButton btn = shipSelectionBtnGroup.GetPressedButton();
            if(btn != null){
                RpcId(1, MethodName.spawnShip, 
                        ((ShipSelectionButton)btn.GetParent()).ShipData.ResourcePath, 
                        GetViewport().GetCamera2D().GetLocalMousePosition() + GetViewport().GetCamera2D().Position, 
                        Multiplayer.GetUniqueId());
            }
        }
    }

    [Rpc(MultiplayerApi.RpcMode.AnyPeer, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
    private void spawnShip(string shipPath, Vector2 position, int playerId){
        ShipDataResource shipData = shipTypesDict[shipPath];
        GD.Print($"Spawn player at: {position}");

        Ship ship = shipScene.Instantiate<Ship>();
        ship.ShipData = shipData;
        ship.playerId = playerId;
        ship.Position = position;
        shipSpawn.AddChild(ship, true);
    }
    

	private void spawnShipButtons() {
		foreach(ShipDataResource shipData in shipTypesDict.Values){
			ShipSelectionButton shipBtn = ShipButtonScene.Instantiate<ShipSelectionButton>();
			shipBtn.ShipData = shipData;

			ShipsContainer.AddChild(shipBtn);
		}
	}
}
