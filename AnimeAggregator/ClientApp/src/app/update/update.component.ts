import { Component, OnInit, Input } from '@angular/core';
import { Publisher } from '../models/Publisher';
import { AnimeUpdate, DubType } from '../models/AnimeUpdate';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.css']
})
export class UpdateComponent implements OnInit {
  @Input() publisher: Publisher;
  @Input() publisherUpdates: AnimeUpdate[];
  selectedDubType = DubType;
  keys: Array<string>;

  constructor() {
    this.keys = Object.keys(this.selectedDubType).filter(e => parseInt(e, 10) >= 0);
  }

  onChange() {
    console.log(this.selectedDubType);
  }

  ngOnInit() {
  }

}
