using MarvelWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarvelWebApp.Services
{
    public class InMemoryHeroService : IHeroService
    {
        private static readonly IEnumerable<Hero> _heroes = new List<Hero>
        {
            new Hero
            {
                Name = "Hulk",
                Description = "Caught in a gamma bomb explosion while trying to save the life of a teenager, Dr. Bruce Banner was transformed into the incredibly powerful creature called the Hulk. An all too often misunderstood hero, the angrier the Hulk gets, the stronger the Hulk gets.",
                ThumbnailUrl = "http://i.annihil.us/u/prod/marvel/i/mg/5/a0/538615ca33ab0.jpg"
            },
            new Hero
            {
                Name = "Iron Man",
                Description = "Wounded, captured and forced to build a weapon by his enemies, billionaire industrialist Tony Stark instead created an advanced suit of armor to save his life and escape captivity. Now with a new outlook on life, Tony uses his money and intelligence to make the world a safer, better place as Iron Man.",
                ThumbnailUrl = "http://i.annihil.us/u/prod/marvel/i/mg/9/c0/527bb7b37ff55.jpg"
            },
            new Hero
            {
                Name = "Spider-Man",
                Description = "Bitten by a radioactive spider, high school student Peter Parker gained the speed, strength and powers of a spider. Adopting the name Spider-Man, Peter hoped to start a career using his new abilities. Taught that with great power comes great responsibility, Spidey has vowed to use his powers to help people.",
                ThumbnailUrl = "http://i.annihil.us/u/prod/marvel/i/mg/3/50/526548a343e4b.jpg"
            },
            new Hero
            {
                Name = "Captain America",
                Description = "Vowing to serve his country any way he could, young Steve Rogers took the super soldier serum to become America's one-man army. Fighting for the red, white and blue for over 60 years, Captain America is the living, breathing symbol of freedom and liberty.",
                ThumbnailUrl = "http://i.annihil.us/u/prod/marvel/i/mg/3/50/537ba56d31087.jpg"
            }
        };

        public Task<IEnumerable<Hero>> GetHeroes(string page)
        {
            return Task.FromResult(_heroes);
        }
    }
}
