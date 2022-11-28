namespace TapTitaner.Models;

public class Monster
{
    public string Name { get; set; } = "Slime";
    public int HitPoints { get; set; } = 5;
    public int MaximumHitPoints { get; set; } = 5;

    public ElementType ElementType { get; set; }
    public int Exp { get; set; } = 1;

    public Monster LevelUp()
    {
        Name = GetRandomMonsterName();;
        MaximumHitPoints ++;
        HitPoints = MaximumHitPoints;
        var next = new Random().Next(0, Enum.GetNames(typeof(ElementType)).Length);
        ElementType = (ElementType)next;
        Exp = 1;
        return this;
    }
    private static string GetRandomMonsterName()
    {
        var nameList = new[] { "Dawnscream", "Cavecreep", "Poisonfreak", "Ashman", "The False Blob", "The Dark Horror", "The Enraged Witch", "The Primitive Spite Cobra", "The Patriarch Bane Dog", "The Silver Army Boar", "Mistling", "Blightfiend", "Hellfigure", "Infernalbeing", "The Filthy Gnoll", "The Faint Mongrel", "The Cruel Eyes", "The Ravaging Spite Rat", "The Chaotic Grieve Hawk", "The Furry Sun Monster", "Caverntree", "Brinestrike", "Chaosspawn", "Mourntooth", "The Dead Thing", "The Insane Face", "The Outlandish Beast", "The Cobalt Phantom Lizard", "The Electric Mountain Spider", "The Crimson Boulder Bear", "Terrorsword", "Cavernthing", "Auracat", "Cinderwings", "The Dreary Savage", "The Grim Revenant", "The Nasty Revenant", "The Stalking Dread Lion", "The Crowned Razor Warthog", "The Masked Thunder Gorilla", "Tombbeast", "Fetidfoot", "Chaosdeviation", "Moldling", "The Ancient Vermin", "The Faint Horror", "The Blue Witch", "The Howling Nightmare Hog", "The Crazed Thunder Leviathan", "The Greater Killer Critter" };
        var start = new Random().Next(0, nameList.Length - 1);
        var end = start + 1;
        return nameList.Take(new Range(start, end)).Single();
    }
    // public Monster(string name, int hitPoints)
    // {
    //     Name = name;
    //     HitPoints = hitPoints;
    //     MaximumHitPoints = hitPoints;
    //     var next = new Random().Next(0, Enum.GetNames(typeof(ElementType)).Length);
    //     ElementType = (ElementType)next;
    //     Exp = 1;
    // }
}