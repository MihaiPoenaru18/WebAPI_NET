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
import { ReactiveFormsModule } from '@angular/forms';
import { Route, RouterModule, Routes } from '@angular/router';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { HomeComponent } from './components/home/home.component';
import { SignInComponent } from './components/sign-in/sign-in.component';

const appRoutes: Routes = [
  { path: 'sign-up',  component: SignUpComponent },
  { path: 'sign-in', component: SignInComponent },
  { path: '', component: HomeComponent },
  { path: '', redirectTo: 'Home', pathMatch:'full' },
  { path: '**', component: HomeComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    NewsLetterContainerComponent,
    AboutUsComponent,
    FooterComponent,
    SocialMediaComponent,
    SignUpComponent,
     HomeComponent,
     SignInComponent,
    
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    HttpClientModule,
    ImageSliderModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes, { enableTracing: true }),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
