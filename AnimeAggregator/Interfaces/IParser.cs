using AnimeAggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeAggregator.Interfaces
{
    public interface IParser
    {
        Task<IEnumerable<AnimeUpdate>> GetAnimeUpdatesFromPage(int pageNumber);

        Task<AnimePreview> GetAnimePreview(string animeUrl);
    }
}
