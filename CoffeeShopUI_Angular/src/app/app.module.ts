import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { ImageSliderModule } from './components/image-slider/imageSlider.module';
import { NewsLetterContainerComponent } from './components/news-letter-container/news-letter-container.component';
import { AboutUsComponent } from './components/about-us/about-us.component';
import { FooterComponent } from './components/footer/footer.component';
import { SocialMediaComponent } from './components/social-media/social-media.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    NewsLetterContainerComponent,
    AboutUsComponent,
    FooterComponent,
    SocialMediaComponent,
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    HttpClientModule,
    ImageSliderModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }