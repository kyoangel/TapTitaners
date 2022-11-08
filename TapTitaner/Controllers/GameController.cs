﻿using Microsoft.AspNetCore.Mvc;
using TapTitaner.Models;

namespace TapTitaner.Controllers;

public class GameController : Controller
{
    private static Monster _monster = null;
    private static Hero _hero = null;

    // GET
    public IActionResult Index()
    {
        var gameViewModel = new GameViewModel();
        _monster = new Monster("Slime", 5);
        _hero = new Hero(1, 10);
        gameViewModel.Monster = _monster;
        gameViewModel.Hero = _hero;
        return View(gameViewModel);
    }

    [HttpPost]
    public IActionResult Hit(HitRequest request)
    {
        _monster.HitPoints -= request.AttackPoints;

        IsMonsterDead();

        return Ok(new { _monster, _hero });
    }

    [HttpPost]
    public IActionResult SkillHit(HitRequest request)
    {
        if (_hero.ManaPoints == 0)
        {
            return BadRequest("ManaPoints not enough");
        }

        switch (request.ElementType)
        {
            case ElementType.Neutral:
                _monster.HitPoints -= request.AttackPoints * 3;
                break;
            case ElementType.Fire:
                switch (_monster.ElementType)
                {
                    case ElementType.Neutral:
                    case ElementType.Fire:
                        _monster.HitPoints -= request.AttackPoints * 2; 
                        break;
                    case ElementType.Water:
                        _monster.HitPoints -= request.AttackPoints;
                        break;
                    case ElementType.Wood:
                        _monster.HitPoints -= request.AttackPoints * 4; 
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                break;
            case ElementType.Water:
                switch (_monster.ElementType)
                {
                    case ElementType.Fire:
                        _monster.HitPoints -= request.AttackPoints * 4; 
                        break;
                    case ElementType.Neutral:
                    case ElementType.Water:
                        _monster.HitPoints -= request.AttackPoints * 2;
                        break;
                    case ElementType.Wood:
                        _monster.HitPoints -= request.AttackPoints; 
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                break;
            case ElementType.Wood:
                switch (_monster.ElementType)
                {
                    case ElementType.Fire:
                        _monster.HitPoints -= request.AttackPoints; 
                        break;
                    case ElementType.Water:
                        _monster.HitPoints -= request.AttackPoints * 4;
                        break;
                    case ElementType.Neutral:
                    case ElementType.Wood:
                        _monster.HitPoints -= request.AttackPoints * 2; 
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        IsMonsterDead();
        _hero.ManaPoints -= 1;

        return Ok(new { _monster, _hero });
    }

    private static void IsMonsterDead()
    {
        if (_monster.HitPoints <= 0)
        {
            _hero.GainExp(_monster.Exp);
            _monster = new Monster(GetRandomMonsterName(), GetMoreHitPoints(_monster.MaximumHitPoints));
        }
    }

    private static int GetMoreHitPoints(int lastTimeHitPoints)
    {
        return lastTimeHitPoints + 1;
    }

    private static string GetRandomMonsterName()
    {
        var nameList = new[] { "Dawnscream", "Cavecreep", "Poisonfreak", "Ashman", "The False Blob", "The Dark Horror", "The Enraged Witch", "The Primitive Spite Cobra", "The Patriarch Bane Dog", "The Silver Army Boar", "Mistling", "Blightfiend", "Hellfigure", "Infernalbeing", "The Filthy Gnoll", "The Faint Mongrel", "The Cruel Eyes", "The Ravaging Spite Rat", "The Chaotic Grieve Hawk", "The Furry Sun Monster", "Caverntree", "Brinestrike", "Chaosspawn", "Mourntooth", "The Dead Thing", "The Insane Face", "The Outlandish Beast", "The Cobalt Phantom Lizard", "The Electric Mountain Spider", "The Crimson Boulder Bear", "Terrorsword", "Cavernthing", "Auracat", "Cinderwings", "The Dreary Savage", "The Grim Revenant", "The Nasty Revenant", "The Stalking Dread Lion", "The Crowned Razor Warthog", "The Masked Thunder Gorilla", "Tombbeast", "Fetidfoot", "Chaosdeviation", "Moldling", "The Ancient Vermin", "The Faint Horror", "The Blue Witch", "The Howling Nightmare Hog", "The Crazed Thunder Leviathan", "The Greater Killer Critter" };
        var start = new Random().Next(0, nameList.Length - 1);
        var end = start + 1;
        return nameList.Take(new Range(start, end)).Single();
    }
}