import { Component, OnInit, Input } from '@angular/core';
import { Anime } from '../models/Anime';
import { AnimePreview } from '../models/AnimePreview';
import { AnimeService } from '../services/AnimeService';

@Component({
  selector: 'anime-preview',
  templateUrl: './anime-preview.component.html',
  styleUrls: ['./anime-preview.component.css']
})
export class AnimePreviewComponent implements OnInit {

  @Input('anime')
  set anime(value: Anime) {
    this.animePreview = undefined;
    this._anime = value;
    if(value !== undefined && value !== null)
      this.animeService.getAnimePreview(this._anime.pageSrc).subscribe(data => {
        this.animePreview = data;
        var txt = document.createElement("textarea");
        txt.innerHTML = this.animePreview.description;
        this.animePreview.description = txt.value;
      });
  }
  get anime(): Anime {
    return this._anime;
  }
  _anime: Anime;
  animePreview: AnimePreview;
  constructor(private animeService: AnimeService) { }

  ngOnInit() {
    
  }

}
