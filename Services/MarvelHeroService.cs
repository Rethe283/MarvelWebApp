using MarvelWebApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarvelWebApp.Services
{
    public class MarvelHeroService : IHeroService
    {
        public Task<IEnumerable<Hero>> GetHeroes()
        {
            throw new NotImplementedException();
        }
    }
}
