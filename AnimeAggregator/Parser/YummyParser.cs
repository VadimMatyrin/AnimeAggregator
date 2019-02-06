using AnimeAggregator.Interfaces;
using AnimeAggregator.Models;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnimeAggregator.Parser
{
    public class YummyParser : IParser
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<IEnumerable<AnimeUpdate>> GetAnimeUpdatesFromPage(int pageNumber)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), pageNumber, "Page number cannot be less or equal to 0");

            var animes = new List<Anime>();
            var animeUpdates = new List<AnimeUpdate>();
            var updateNodes = (await GetLastUpdateNodes(pageNumber));
            foreach (var node in updateNodes)
            {
                var nodeInnerText = node.QuerySelector(".update-info").InnerText;
                var episodeNums = Regex.Split(nodeInnerText, @"\D+").Where(num => !string.IsNullOrEmpty(num)).ToList();
                if (episodeNums.Count == 0)
                    continue;

                var animePageSrc = $"https://yummyanime.com{node.Attributes.FirstOrDefault(a => a.Name == "href").Value}";
                var anime = new Anime { Name = node.QuerySelector(".update-title").InnerText, PageSrc = animePageSrc };
                var publisher = new Publisher { Name = Regex.Replace(nodeInnerText, @"[^a-zA-Z]", "") };
                var updateDate = node.QuerySelector(".update-date").InnerText;
                DubType dubType = nodeInnerText.Contains("озвучкой") ? DubType.Voiceover : DubType.Subtitiles;
                var num1 = int.Parse(episodeNums[0]);
                var num2 = int.Parse(episodeNums.ElementAtOrDefault(1) ?? episodeNums[0]);
                for (int i = num1; i <= num2; i++)
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

            return animeUpdates;
        }

        public async Task<AnimePreview> GetAnimePreview(string animeUrl)
        {
            var animePage = await GetAnimePage(animeUrl);
            var description = animePage.QuerySelector("#content-desc-text p").InnerText;
            var relativeUri = animePage.QuerySelector(".poster-block img").Attributes.FirstOrDefault(a => a.Name == "src")?.Value;
            var imageRef = $"https://yummyanime.com{relativeUri}";
            var animeStatus = animePage.QuerySelector(".badge").InnerText;
            var animePreview = new AnimePreview
            {
                AnimeStatus = animeStatus,
                Description = description,
                ImageHref = imageRef
            };

            return animePreview;
        }

        private async Task<HtmlDocument> GetAnimePage(string animeUrl)
        {
            var result = await _httpClient.GetAsync($"{animeUrl}");
            var stream = await result.Content.ReadAsStreamAsync();
            var doc = new HtmlDocument();
            doc.Load(stream);
            return doc;
        }

        private async Task<IEnumerable<HtmlNode>> GetLastUpdateNodes(int pageNumber = 1)
        {
            var nodes = new List<HtmlNode>();
            var result = await _httpClient.GetAsync($"https://yummyanime.com/anime-updates?page={pageNumber}");
            var stream = await result.Content.ReadAsStreamAsync();
            var doc = new HtmlDocument();
            doc.Load(stream);
            nodes.AddRange(doc.QuerySelectorAll("ul.update-list li a"));
            return nodes;
        }

    }
}
