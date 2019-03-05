using AnimeAggregator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimeAggregator.Interfaces
{
    public interface IParser
    {
        Task<IEnumerable<AnimeUpdate>> GetAnimeUpdatesFromPage(int pageNumber);

        Task<AnimePreview> GetAnimePreview(string animeUrl);
    }
}
