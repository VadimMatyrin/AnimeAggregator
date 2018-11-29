import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AnimeUpdate } from '../models/AnimeUpdate';
import { Observable } from 'rxjs';

@Injectable()
export class AnimeService {
  constructor(private http: HttpClient)
  {

  }
  getAnimeUpdates(page: number) {
    return this.http.get<AnimeUpdate[]>(`api/Parse/GetUpdates/${page}`);
  }
}
