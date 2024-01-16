//services
import { NewsletterService } from './services/Newsletter/newsletter.service';
import { AuthenticatorService } from './services/Auth/authenticator.service';

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
import { UserinfoComponent } from './componets/userinfo/userinfo.component';
import { ProductsListComponent } from './componets/Product-Page/products-list/products-list.component';
import { ProductDetailsComponent } from './componets/Product-Page/product-details/product-details.component';
import { ProductsService } from './services/Product/products.service';

const appRoutes: Routes = [
  { path: 'sign-up', component: SignUpComponent },
  { path: 'sign-in', component: SignInComponent },
  { path: 'userInfo', component: UserinfoComponent },
  { path: 'products-list', component: ProductsListComponent },
  { path: 'Home', component: HomeComponent },
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
    SvgSignUpComponent,
    SvgFooterComponent,
    UserinfoComponent,
    ProductsListComponent,
    ProductDetailsComponent,
   
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
  providers: [NewsletterService,AuthenticatorService,ProductsService ],
  bootstrap: [AppComponent],
})
export class AppModule {}
