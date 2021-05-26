using MarvelWebApp.Configuration;
using MarvelWebApp.Models;
using MarvelWebApp.Services.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MarvelWebApp.Services
{
    public class MarvelHeroService : IHeroService
    {
        private readonly JsonSerializerOptions _defaultSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

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
            var toHash = timestamp + ApiSettings.PrivateKey + ApiSettings.PublicKey;
            var hash = MD5.Create();
            var hashOutput = string.Join(string.Empty, hash.ComputeHash(Encoding.UTF8.GetBytes(toHash)).Select(b => b.ToString("x2")));
            var response = await HttpClient.GetAsync($"v1/public/characters?ts={timestamp}&apikey={ApiSettings.PublicKey}&hash={hashOutput}&limit=10&offset=50");
            if (!response.IsSuccessStatusCode)
            {
                return default;
            }
            var responseJson = await response.Content.ReadAsStringAsync();
            var responseModel = JsonSerializer.Deserialize<MarvelResponse>(responseJson, _defaultSerializerOptions);
            return responseModel.ToHeroEnumerable();
        }
    }
}
