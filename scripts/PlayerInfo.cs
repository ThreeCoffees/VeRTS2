using Godot;

public partial class PlayerInfo : RefCounted
{
    public string Name;
    public long Id;
    public string Color;

    public PlayerInfo(string Name, long Id)
    {
        this.Name = Name;
        this.Id = Id;
    }
}
