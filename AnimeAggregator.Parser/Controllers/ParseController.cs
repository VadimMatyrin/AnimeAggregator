using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
        [HttpGet]
        [Route("GetUpdates/{amount}")]
        public async Task<IEnumerable<Anime>> Get(int amount)
        {
            var updates = new List<Anime>();
            var updateNodes = await GetLastUpdatesNodes(amount);
            updates = updateNodes.Select(au => new Anime { Name = au.InnerHtml }).ToList();
            return updates;
        }

        private async Task<IEnumerable<HtmlNode>> GetLastUpdatesNodes(int amount)
        {
            var hc = new HttpClient();
            var nodes = new List<HtmlNode>();
            for(var i = 0; nodes.Count < amount; i++)
            {
                var result = await hc.GetAsync($"https://yummyanime.com/anime-updates?page={i}");
                var stream = await result.Content.ReadAsStreamAsync();
                var doc = new HtmlDocument();
                doc.Load(stream);
                nodes.AddRange(doc.QuerySelectorAll(".update-title"));
            }
            return nodes;
        }
    }
}
