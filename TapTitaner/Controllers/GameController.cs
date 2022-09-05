using Microsoft.AspNetCore.Mvc;
using TapTitaner.Models;

namespace TapTitaner.Controllers;

public class GameController : Controller
{
    private static Monster _monster = null;

    // GET
    public IActionResult Index()
    {
        var gameViewModel = new GameViewModel();
        _monster = new Monster("Goblin", 5);
        gameViewModel.Monster = _monster;
        return View(gameViewModel);
    }

    [HttpPost]
    public IActionResult Hit(HitRequest request)
    {
        _monster.HitPoints -= request.AttackPoints;
        
        if (_monster.HitPoints == 0)
        {
            _monster = new Monster("Dwarf", 6);
        }

        return Ok(_monster);
    }
}