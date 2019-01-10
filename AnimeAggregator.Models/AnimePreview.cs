using System;

namespace AnimeAggregator.Models
{
    public class AnimePreview
    {
        public Anime Anime { get; set; }
        public string Description { get; set; }
        public string ImageHref { get; set; }
        public string AnimeStatus { get; set; }

    }
}