using Godot;
using System;

public partial class MultiplayerManager : Node
{
    public static MultiplayerManager Instance {get; private set;}

    [Signal]
    public delegate void PlayerConnectedEventHandler(long Id, PlayerInfo playerInfo);
    [Signal]
    public delegate void PlayerDisconnectedEventHandler(long Id);

    public static Godot.Collections.Dictionary<long, PlayerInfo> Players = new Godot.Collections.Dictionary<long, PlayerInfo>();

    public const string DefaultServerIP = "127.0.0.1";
    public const int DefaultPort = 8080;
    public const int DefaultPlayerCount = 2;

    public override void _Ready(){
        Instance = this;

        Multiplayer.PeerConnected += OnPeerConnected;
        Multiplayer.PeerDisconnected += OnPeerDisonnected;
        Multiplayer.ConnectedToServer += OnConnectionSuccess;
        Multiplayer.ConnectionFailed += OnConnectionFailed;
        Multiplayer.ServerDisconnected += OnServerDisconnected;

        if(OS.HasFeature("dedicated_server")){
            var args = OS.GetCmdlineUserArgs();
            int port = DefaultPort;
            int playerCount = DefaultPlayerCount;
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
            CallDeferred(MethodName.StartServer, port, playerCount);
        }
    }

    public Error StartServer(int Port, int PlayerCount){
        GetTree().ChangeSceneToFile("res://scenes/ui/lobby.tscn");

        var ServerPeer = new ENetMultiplayerPeer();
        Error error = ServerPeer.CreateServer(Port, PlayerCount);
        if(error != Error.Ok){
            return error;
        }

        Multiplayer.MultiplayerPeer = ServerPeer;
        return Error.Ok;
    }

    public Error JoinServer(String Adress, int Port){
        if (string.IsNullOrEmpty(Adress)){
            Adress = DefaultServerIP;
        }

        var ClientPeer = new ENetMultiplayerPeer();
        Error error = ClientPeer.CreateClient(Adress, Port);
        if(error != Error.Ok){
            return error;
        }

        Multiplayer.MultiplayerPeer = ClientPeer;
        return Error.Ok;
    }

    private void OnPeerConnected(long Id){
        GD.Print("Player " + Id + " joined the game");
    }

    private void OnPeerDisonnected(long Id){
        GD.Print("Player " + Id + " left the game");
        Players.Remove(Id);
        EmitSignal(SignalName.PlayerDisconnected, Id);
    }

    public void OnConnectionSuccess(){
        GD.Print("Connected to server successfully");
        RpcId(1, MethodName.SendPlayerInfo, 
                    GetTree().GetCurrentScene().GetNode<LineEdit>("MenuContainer/JoinDataContainer/NicknameInput").Text, 
                    Multiplayer.GetUniqueId()
                );

        GetTree().ChangeSceneToFile("res://scenes/ui/lobby.tscn");
    }

    public void OnConnectionFailed(){
        GD.Print("Couldn't connect to server");
    }

    public void OnServerDisconnected(){
        GD.Print("Server disconnected");
        Players.Clear();
        GetTree().ChangeSceneToFile("res://scenes/ui/main_menu.tscn");
    }

    public void OnStartGameButtonPressed()
    {
        Rpc(MethodName.StartGame);
    }

    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
    private void StartGame(){
        GetTree().ChangeSceneToFile("res://scenes/game.tscn");
    }

    [Rpc(MultiplayerApi.RpcMode.AnyPeer)]
    private void SendPlayerInfo(string Name, long Id){
        PlayerInfo playerInfo = new PlayerInfo(Name, Id);

        if(!Players.ContainsKey(Id)){
            Players[Id] = playerInfo;
            EmitSignal(SignalName.PlayerConnected, Id, playerInfo);
        }

        if(Multiplayer.IsServer()){
            foreach(PlayerInfo pi in Players.Values){
                Rpc(MethodName.SendPlayerInfo, pi.Name, pi.Id);
            }
        }
    }
}
