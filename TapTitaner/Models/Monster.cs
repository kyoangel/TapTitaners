namespace TapTitaner.Models;

public class Monster
{
    public string Name { get; }
    public int HitPoints { get; set; }
    public int MaximumHitPoints { get; }

    public Monster(string name, int hitPoints)
    {
        Name = name;
        HitPoints = hitPoints;
        MaximumHitPoints = hitPoints;
    }
}