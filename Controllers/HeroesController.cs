using MarvelWebApp.Models;
using MarvelWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MarvelWebApp.Controllers
{
    [Route("heroes")]
    public class HeroesController : Controller
    {
        public const string Name = "Heroes";

        public HeroesController(IHeroService heroService)
        {
            HeroService = heroService ?? throw new ArgumentNullException(nameof(heroService));
        }

        public IHeroService HeroService { get; }

        [HttpGet("list")]
        public async Task<IActionResult> GetHeroes([FromQuery] ListOptions options)
        {
            var heroes = await HeroService.GetHeroes(options);
            TempData[nameof(options)] = options;
            return View(heroes);
        }
    }
}
