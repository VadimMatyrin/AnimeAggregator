﻿namespace AnimeAggregator.Models
{
    public class AnimeUpdate
    {
        public Anime Anime { get; set; }
        public Publisher Publisher { get; set; }
        public int EpisodeNum { get; set; }
        public string UpdateDate { get; set; }
        public DubType DubType { get; set; }
    }
    public enum DubType
    {
        Subtitiles,
        Voiceover
    }
}
