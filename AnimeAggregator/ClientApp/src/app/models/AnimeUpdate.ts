import { Anime } from "./Anime";
import { Publisher } from "./Publisher";

export class AnimeUpdate {
  public anime: Anime;
  public publisher: Publisher;
  public episodeNum: number;
  public updateDate: string;
}
