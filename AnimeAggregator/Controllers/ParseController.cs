using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AnimeAggregator.Interfaces;
using AnimeAggregator.Models;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
