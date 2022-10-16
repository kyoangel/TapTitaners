namespace TapTitaner.Models;

public class Monster
{
    public string Name { get; }
    public int HitPoints { get; set; }
    public int MaximumHitPoints { get; }

    public ElementType ElementType { get; set; }

    public Monster(string name, int hitPoints)
    {
        Name = name;
        HitPoints = hitPoints;
        MaximumHitPoints = hitPoints;
        var next = new Random().Next(0, Enum.GetNames(typeof(ElementType)).Length);
        ElementType = (ElementType)next;
    }
}