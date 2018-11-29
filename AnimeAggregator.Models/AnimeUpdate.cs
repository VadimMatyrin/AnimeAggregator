using System;
using System.Collections.Generic;
using System.Text;

namespace AnimeAggregator.Models
{
    public class AnimeUpdate
    {
        public Anime Anime { get; set; }
        public Publisher Publisher { get; set; }
        public short EpisodeNum { get; set; }
        public string UpdateDate { get; set; }
        public DubType DubType { get;set;}
    }
    public enum DubType
    {
        Subtitiles,
        Voiceover
    }
}
