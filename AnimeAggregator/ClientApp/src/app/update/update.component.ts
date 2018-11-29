import { Component, OnInit, Input } from '@angular/core';
import { Publisher } from '../models/Publisher';
import { AnimeUpdate } from '../models/AnimeUpdate';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.css']
})
export class UpdateComponent implements OnInit {
  @Input() publisher: Publisher;
  @Input() publisherUpdates: AnimeUpdate[];
  constructor( ) { }

  ngOnInit() {
  }

}
