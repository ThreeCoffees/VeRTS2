using Godot;
using System;

public partial class SceneManager : Node
{
    public static SceneManager Instance {get; private set;}

    public enum SceneEnum {
        MainMenu,
        JoinMatchMenu,
        HostMatchMenu,
        Lobby,
        Game,
    }

    [Export]
    public Godot.Collections.Dictionary<SceneEnum, PackedScene> scenes = new Godot.Collections.Dictionary<SceneEnum, PackedScene>();

    private SceneEnum scene;
    public SceneEnum Scene {
        get { return scene; }
        set {
            if(scenes.ContainsKey(value)){
                scene = value;
                GetTree().ChangeSceneToPacked(scenes[scene]);
            } else {
                GD.PrintErr("Scene " + value + " is not registered in SceneManager. Add it to the autoload.");
            }
        }
    }

    public override void _Ready(){
        Instance = this;
        scene = SceneEnum.MainMenu;
    }
}
