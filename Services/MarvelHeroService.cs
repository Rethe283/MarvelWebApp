using MarvelWebApp.Configuration;
using MarvelWebApp.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MarvelWebApp.Services
{
    public class MarvelHeroService : IHeroService
    {
        public MarvelHeroService(HttpClient httpClient, IOptions<MarvelApiSettings> apiSettings)
        {
            HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            ApiSettings = apiSettings?.Value ?? throw new ArgumentNullException(nameof(apiSettings));
        }

        public HttpClient HttpClient { get; }
        public MarvelApiSettings ApiSettings { get; }

        public async Task<IEnumerable<Hero>> GetHeroes()
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var tohash = timestamp + ApiSettings.PrivateKey + ApiSettings.PublicKey;
            var hash = MD5.Create();
            var hashoutput = string.Join(string.Empty, hash.ComputeHash(Encoding.UTF8.GetBytes(tohash)).Select(b => b.ToString("x2")));
            var response = await HttpClient.GetAsync($"v1/public/characters?ts={timestamp}&apikey={ApiSettings.PublicKey}&hash={hashoutput}");
            if (!response.IsSuccessStatusCode)
            {
            }
            var responseJson = await response.Content.ReadAsStringAsync();
            return new List<Hero>();
        }
    }
}
