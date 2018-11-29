using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
        private readonly HttpClient Client = new HttpClient();
        [HttpGet]
        [Route("GetUpdates/{pageNumber}/{pageSize}")]
        public async Task<IEnumerable<AnimeUpdate>> Get(int pageNumber = 1, int pageSize = 10)
        {
            var animes = new List<Anime>();
            var animeUpdates = new List<AnimeUpdate>();
            var updateNodes = await GetLastUpdateNodes(pageNumber, pageSize);
            foreach(var node in updateNodes)
            {
                var anime = new Anime { Name = node.QuerySelector(".update-title").InnerHtml };
                var nodeText = node.QuerySelector(".update-info").InnerHtml;
                var publisher = new Publisher { Name = nodeText.Split(' ').Last() };
                var episodeNums = Regex.Split(nodeText, @"\D+");
                foreach (var episodeNum in episodeNums)
                {
                    if (!string.IsNullOrEmpty(episodeNum))
                    {
                        var animeUpdate = new AnimeUpdate { Anime = anime, Publisher = publisher, EpisodeNum = short.Parse(episodeNum) };
                        animeUpdates.Add(animeUpdate);
                    }
                }
            }
            return animeUpdates;
        }

        private async Task<IEnumerable<HtmlNode>> GetLastUpdateNodes(int pageNumber = 1, int pageSize = 10)
        {
            var nodes = new List<HtmlNode>();
            for (var i = 0; nodes.Count < pageSize * pageNumber; i++)
            {
                var result = await Client.GetAsync($"https://yummyanime.com/anime-updates?page={i}");
                var stream = await result.Content.ReadAsStreamAsync();
                var doc = new HtmlDocument();
                doc.Load(stream);
                nodes.AddRange(doc.QuerySelectorAll(".update-list-block"));
            }
            nodes = nodes.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return nodes;
        }
    }
}
