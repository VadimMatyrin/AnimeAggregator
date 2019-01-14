import { Component, OnInit, Input } from '@angular/core';
import { Publisher } from '../models/Publisher';
import { AnimeUpdate, DubType } from '../models/AnimeUpdate';
import { Anime } from '../models/Anime';
import { forEach } from '@angular/router/src/utils/collection';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.css']
})
export class UpdateComponent implements OnInit {
  _publisherUpdates: AnimeUpdate[];

  @Input() publisher: Publisher;
  @Input('publisherUpdates')
  set publisherUpdates(value: AnimeUpdate[]) {
    this.selectedAnime = null;
    this._publisherUpdates = value;
    this.filteredPublishers = this._publisherUpdates;
    this.selectedDubType = "Any";
    this.selectedAnime = undefined;
    this.animes = [];
    for (var animeUpdate of this._publisherUpdates) {
      if (this.animes.filter(a => a.name == animeUpdate.anime.name).length === 0)
        this.animes.push(animeUpdate.anime);
    }
    //this.animes = Array.from(new Set(this._publisherUpdates.map(item => item.anime)));
    //this.animeNames = Array.from(new Set(this._publisherUpdates.map(item => item.anime.name)));
  }
  filteredPublishers: AnimeUpdate[];
  dubType = DubType;
  selectedDubType: string = undefined;
  selectedAnime: Anime = undefined;
  keys: Array<string>;
  animes: Array<Anime> = [];

  constructor() {
    this.keys = Object.keys(DubType).filter(e => parseInt(e, 10) >= 0);
  }

  onChange(dubType: string, anime: string) {

    if (dubType !== null)
      this.selectedDubType = dubType; 

    if (dubType === "Any")
      this.selectedDubType = undefined;

    if (anime !== null)
      this.selectedAnime = this.animes.filter(a => a.name === anime)[0];

    if (anime === "Any")
      this.selectedAnime = undefined;

     if (this.selectedDubType === undefined)
      this.filteredPublishers = this._publisherUpdates;
    else
      this.filteredPublishers = this._publisherUpdates.filter(u => u.dubType === +this.selectedDubType);

    if (this.selectedAnime === undefined)
      this.filteredPublishers = this.filteredPublishers;
    else
      this.filteredPublishers = this.filteredPublishers.filter(u => u.anime.name === this.selectedAnime.name);

  }

  ngOnInit() {
  }

}
