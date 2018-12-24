import { Component, OnInit, Input } from '@angular/core';
import { Publisher } from '../models/Publisher';
import { AnimeUpdate, DubType } from '../models/AnimeUpdate';

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
    this._publisherUpdates = value;
    this.filteredPublishers = this._publisherUpdates;
    //this.animeNames = Array.from(new Set(this._publisherUpdates.map(item => item.anime.name)));
  }
  _filteredPublishers: AnimeUpdate[];
  get filteredPublishers(): AnimeUpdate[] {
    return this._filteredPublishers;
  }
  set filteredPublishers(value: AnimeUpdate[]) {
    this._filteredPublishers = value;
    this.animeNames = Array.from(new Set(value.map(item => item.anime.name)));
  }
  dubType = DubType;
  selectedDubType: string = "Any";
  selectedAnime: string = "Any";
  keys: Array<string>;
  animeNames: Array<string>;

  constructor() {
    this.keys = Object.keys(DubType).filter(e => parseInt(e, 10) >= 0);
  }

  onChange(dubType: string, animeName: string) {

    if (dubType !== null)
      this.selectedDubType = dubType;

    if (animeName !== null)
      this.selectedAnime = animeName;

    if (this.selectedDubType === "Any")
      this.filteredPublishers = this._publisherUpdates;
    else
      this.filteredPublishers = this._publisherUpdates.filter(u => u.dubType === +this.selectedDubType);

    if (this.selectedAnime === "Any" ) 
      this.filteredPublishers = this.filteredPublishers;
    else
      this.filteredPublishers = this.filteredPublishers.filter(u => u.anime.name === this.selectedAnime);

  }

  ngOnInit() {
  }

}
