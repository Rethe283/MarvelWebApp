using MarvelWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarvelWebApp.Services
{
    public interface IHeroService
    {
        Task<IEnumerable<Hero>> GetHeroes(ListOptions options);
    }
}
