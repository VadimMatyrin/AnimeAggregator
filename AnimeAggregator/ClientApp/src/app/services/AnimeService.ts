import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AnimeUpdate } from '../models/AnimeUpdate';
import { Observable } from 'rxjs';
import { AnimePreview } from '../models/AnimePreview';

@Injectable()
export class AnimeService {
  constructor(private http: HttpClient)
  {

  }
  getAnimeUpdates(page: number) {
    return this.http.get<AnimeUpdate[]>(`api/Parse/getUpdates/${page}`);
  }

  getAnimePreview(animeRef: string) {
    return this.http.get<AnimePreview>(`api/Parse/getAnimePreview?animeUrl=${encodeURIComponent(animeRef)}`);
  }
}
