import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './componets/header/header.component';
import { ImageSliderModule } from './componets/image-slider/imageSlider.module';
import { NewsLetterContainerComponent } from './componets/news-letter-container/news-letter-container.component';
import { AboutUsComponent } from './componets/about-us/about-us.component';
import { FooterComponent } from './componets/footer/footer.component';
import { SocialMediaComponent } from './componets/social-media/social-media.component';
import { ReactiveFormsModule } from '@angular/forms';
import { Route, RouterModule, Routes } from '@angular/router';
import { SignUpComponent } from './componets/sign-up/sign-up.component';
import { HomeComponent } from './componets/home/home.component';
import { SignInComponent } from './componets/sign-in/sign-in.component';
import { SvgSignUpComponent } from './componets/svg-sign-up/svg-sign-up.component';
import { SvgFooterComponent } from './componets/footer/svg-footer/svg-footer.component';

const appRoutes: Routes = [
  { path: 'sign-up', component: SignUpComponent },
  { path: 'sign-in', component: SignInComponent },
  { path: '', component: HomeComponent },
  { path: '', redirectTo: 'Home', pathMatch: 'full' },
  { path: '**', component: HomeComponent },
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
    SvgSignUpComponent,
    SvgFooterComponent,
   
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
