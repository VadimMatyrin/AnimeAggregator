import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { PublisherMenuComponent } from './publisher-menu/publisher-menu.component';
import { HomeComponent } from './home/home.component';
import { AnimeService } from './services/AnimeService';
import { UpdateComponent } from './update/update.component';
import { AnimePreviewComponent } from './anime-preview/anime-preview.component';


@NgModule({
  declarations: [
    AppComponent,
    PublisherMenuComponent,
    HomeComponent,
    UpdateComponent,
    AnimePreviewComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
    ])
  ],
  providers: [AnimeService],
  bootstrap: [AppComponent]
})
export class AppModule { }
