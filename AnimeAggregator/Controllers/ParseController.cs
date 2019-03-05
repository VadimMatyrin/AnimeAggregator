using AnimeAggregator.Interfaces;
using AnimeAggregator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace AnimeAggregator.Controllers
{
    [Route("api/[controller]")]
    public class ParseController : Controller
    {
        private readonly IParser _parser;

        public ParseController(IParser parser)
        {
            _parser = parser;
        }

        [HttpGet]
        [Route("getUpdates/{pageNumber}")]
        public async Task<IEnumerable<AnimeUpdate>> Get(int pageNumber = 1)
        {
            return await _parser.GetAnimeUpdatesFromPage(pageNumber);
        }

        [HttpGet]
        [Route("getAnimePreview/{animeUrl?}")]
        [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Client)]
        public async Task<AnimePreview> GetAnimePreview(string animeUrl)
        {
            return await _parser.GetAnimePreview(animeUrl);
        }


    }
}
