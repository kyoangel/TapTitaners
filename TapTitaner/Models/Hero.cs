namespace TapTitaner.Models;

public class Hero
{
    public Hero(int attackPoints, int manaPoints)
    {
        AttackPoints = attackPoints;
        ManaPoints = manaPoints;
    }

    public int AttackPoints { get; }

    public int ManaPoints { get; set; }
}