using Microsoft.AspNetCore.Mvc;
using TapTitaner.Models;
using TapTitaner.Services;

namespace TapTitaner.Controllers;

public class GameController : Controller
{
    private static Monster _monster = null;
    private static Hero _hero = null;

    public IActionResult Index()
    {
        return View(); 
    }
    
    public IActionResult NewGame()
    {
        var gameViewModel = new GameViewModel();
        _monster = new Monster();
        _hero = new Hero();
        gameViewModel.Monster = _monster;
        gameViewModel.Hero = _hero;
        return View("Game",gameViewModel);
    }
    
    public IActionResult LoadGame()
    {
        var docPath = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
        var text = System.IO.File.ReadAllText(Path.Combine(docPath, "TapTitaner.txt"));
        var storedData = System.Text.Json.JsonSerializer.Deserialize<StoredData>(text);
        var gameViewModel = new GameViewModel();
        _monster = storedData.Monster;
        _hero = storedData.Hero;
        gameViewModel.Monster = _monster;
        gameViewModel.Hero = _hero;
        return View("Game",gameViewModel);
    } 

    [HttpPost, ServiceFilter(typeof(SaveFilterAttribute))]
    public IActionResult Hit(HitRequest request)
    {
        _monster.HitPoints -= request.AttackPoints;

        IsMonsterDead();

        return Ok(new { _monster, _hero });
    }

    [HttpPost, ServiceFilter(typeof(SaveFilterAttribute))]
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
            _monster = _monster.LevelUp();
        }
    }
}