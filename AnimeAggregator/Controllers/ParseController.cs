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
        [Route("GetUpdates/{pageNumber}")]
        public async Task<IEnumerable<AnimeUpdate>> Get(int pageNumber = 1)
        {
            var animes = new List<Anime>();
            var animeUpdates = new List<AnimeUpdate>();
            var updateNodes = await GetLastUpdateNodes(pageNumber);
            foreach(var node in updateNodes)
            {
                var nodeText = node.QuerySelector(".update-info").InnerHtml;
                var anime = new Anime { Name = node.QuerySelector(".update-title").InnerHtml };
                var regex = new Regex(@"[^a-zA-Z]");
                var publisher = new Publisher { Name = regex.Replace(nodeText, "") };
                var updateDate = node.QuerySelector(".update-date").InnerHtml;
                var episodeNums = Regex.Split(nodeText, @"\D+").Where(num => !string.IsNullOrEmpty(num)).ToList();
                DubType dubType;

                if (nodeText.Contains("озвучкой"))
                    dubType = DubType.Voiceover;
                else
                    dubType = DubType.Subtitiles;

                if (episodeNums.Count >= 2)
                {
                    var num1 = short.Parse(episodeNums[0]);
                    var num2 = short.Parse(episodeNums[1]);
                    if (num1 + 1 != num2)
                    {
                        for (var i = num1; i <= num2; i++)
                        {
                            var animeUpdate = new AnimeUpdate
                            {
                                Anime = anime,
                                Publisher = publisher,
                                EpisodeNum = i,
                                UpdateDate = updateDate,
                                DubType = dubType
                            };
                            animeUpdates.Add(animeUpdate);
                        }
                    }
                    else
                    {
                        foreach (var episodeNum in episodeNums)
                        {
                            var animeUpdate = new AnimeUpdate
                            {
                                Anime = anime,
                                Publisher = publisher,
                                EpisodeNum = short.Parse(episodeNum),
                                UpdateDate = updateDate,
                                DubType = dubType
                            };
                            animeUpdates.Add(animeUpdate);
                        }
                    }
                }
                else if (episodeNums.Count != 0)
                {
                    var animeUpdate = new AnimeUpdate
                    {
                        Anime = anime,
                        Publisher = publisher,
                        EpisodeNum = short.Parse(episodeNums[0]),
                        UpdateDate = updateDate,
                        DubType = dubType
                    };
                    animeUpdates.Add(animeUpdate);
                }
            }

            return animeUpdates;
        }

        private async Task<IEnumerable<HtmlNode>> GetLastUpdateNodes(int pageNumber = 1)
        {
            var nodes = new List<HtmlNode>();
            var result = await Client.GetAsync($"https://yummyanime.com/anime-updates?page={pageNumber}");
            var stream = await result.Content.ReadAsStreamAsync();
            var doc = new HtmlDocument();
            doc.Load(stream);
            nodes.AddRange(doc.QuerySelectorAll("ul.update-list li a"));
            nodes = nodes.ToList();
            return nodes;
        }
    }
}
