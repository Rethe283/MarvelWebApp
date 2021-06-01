using MarvelWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MarvelWebApp.Controllers
{
    [Route("heroes")]
    public class HeroesController : Controller
    {
        public HeroesController(IHeroService heroService)
        {
            HeroService = heroService ?? throw new ArgumentNullException(nameof(heroService));
        }

        public IHeroService HeroService { get; }
        
        [HttpGet("list")]
        public async Task<IActionResult> GetHeroes()
        {
            if (Request.Query["page"] != default(string) )
            {
                string page = Request.Query["page"];
                var heroes = await HeroService.GetHeroes(page);
                return View(heroes);
            }
            else
            {
                var heroes = await HeroService.GetHeroes("1");
                return View(heroes);
            }
        }
    }
}
