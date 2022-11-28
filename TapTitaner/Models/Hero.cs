namespace TapTitaner.Models;

public class Hero
{
    private int _currentLvLeftExp;

    // public Hero(int attackPoints, int manaPoints)
    // {
    //     Lv = 1;
    //     _currentLvLeftExp = Lv;
    //     AttackPoints = attackPoints;
    //     ManaPoints = manaPoints;
    // }
    //
    // public Hero(int lv)
    // {
    //     Lv = 1;
    //     _currentLvLeftExp = Lv;
    //     ManaPoints = lv * 5;
    // }

    public int AttackPoints { get; set; }= 1;

    public int ManaPoints { get; set; } = 10;
    public int TotalExp { get; set; } 

    public void GainExp(int exp)
    {
        TotalExp += exp;
        _currentLvLeftExp -= exp;
        if (_currentLvLeftExp <= 0)
        {
            LevelUp();
            _currentLvLeftExp = Lv;
        }
    }

    public int Lv { get; set; } = 1;

    private void LevelUp()
    {
        Lv += 1;
        AttackPoints = Convert.ToInt16(Math.Ceiling(Lv / 2d));
        ManaPoints = Lv + 10;
    }
}