using MarvelWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MarvelWebApp.Services.Models
{
    public class MarvelResponse
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Copyright { get; set; }
        public string AttributionText { get; set; }
        public string AttributionHTML { get; set; }
        public string Etag { get; set; }
        public MarvelData Data { get; set; }
    }

    public class MarvelData
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public List<MarvelDataResult> Results { get; set; }
    }

    public class MarvelDataResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public DateTime Modified { get; set; }
        public MarvelThumbnail Thumbnail { get; set; }
    }

    public class MarvelThumbnail
    {
        public string Path { get; set; }
        public string Extension { get; set; }
    }

    public static class MarvelResponseExtensions
    {
        public static IEnumerable<Hero> ToHeroEnumerable(this MarvelResponse model, int page)
        {
            var result = model.Data.Results.Select(x => new Hero
            {
                Name = x.Name,
                Description = x.Description,
                ThumbnailUrl = x.Thumbnail != null ? $"{x.Thumbnail.Path}.{x.Thumbnail.Extension}" : null
            });
            return result;
        }
    }
}
