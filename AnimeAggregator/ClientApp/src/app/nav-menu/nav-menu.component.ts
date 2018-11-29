import { Component, OnInit } from '@angular/core';
import { AnimeUpdate } from '../models/AnimeUpdate';
import { AnimeService } from '../services/AnimeService';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  animeUpdates: AnimeUpdate[];
  constructor(private animeService: AnimeService) { }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  ngOnInit() {
    this.animeService.getAnimeUpdates(1).subscribe(data => this.animeUpdates = data);
  }
}
