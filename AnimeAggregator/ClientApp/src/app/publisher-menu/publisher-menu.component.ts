import { Component, OnInit } from '@angular/core';
import { AnimeUpdate } from '../models/AnimeUpdate';
import { AnimeService } from '../services/AnimeService';
import { Publisher } from '../models/Publisher';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './publisher-menu.component.html',
  styleUrls: ['./publisher-menu.component.css']
})
export class PublisherMenuComponent implements OnInit {
  isExpanded = false;
  animeUpdates: AnimeUpdate[];
  publishers: Publisher[] = [];
  selectedPublisher: Publisher;
  selectedPublisherUpdates: AnimeUpdate[] = [];

  constructor(private animeService: AnimeService) {
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  getPublishers() {
    for (let animeUpdate of this.animeUpdates) {
      if (this.publishers.map(p => p.name).indexOf(animeUpdate.publisher.name) === -1)
        this.publishers.push(animeUpdate.publisher)
    }
  }

  selectPublisher(publisher: Publisher) {
    this.selectedPublisher = publisher;
    this.selectedPublisherUpdates = this.animeUpdates.filter(au => au.publisher.name === this.selectedPublisher.name);
  }

  ngOnInit() {
    this.animeService.getAnimeUpdates(1).subscribe(data => {
      this.animeUpdates = data;
      this.getPublishers();
    });
  }
}
