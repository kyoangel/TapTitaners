namespace TapTitaner.Models;

public class Hero
{
    private int _currentLvLeftExp;

    public Hero(int attackPoints, int manaPoints)
    {
        Lv = 1;
        _currentLvLeftExp = Lv;
        AttackPoints = attackPoints;
        ManaPoints = manaPoints;
    }

    public int AttackPoints { get; private set; }

    public int ManaPoints { get; set; }
    public int TotalExp { get; private set; }

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

    public int Lv { get; set; }

    private void LevelUp()
    {
        Lv += 1;
        AttackPoints = Convert.ToInt16(Math.Ceiling(Lv / 2d));
        ManaPoints = 10;
    }
}