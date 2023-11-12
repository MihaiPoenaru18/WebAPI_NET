import { HttpClient } from '@angular/common/http';
import { Injectable, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthenticatorService {
  @Input() isSubmitted = false;
  @Input() isRegistered =false;
  constructor(private http: HttpClient, private router: Router) {}

  register(requestBody: any, form: FormGroup) {
    this.http
      .post<any>('https://localhost:7282/api/Auth/RegisterUser', requestBody)
      .subscribe({
        next: (response) => {
          console.log('POST request successful', response);
          if (response.Success) {
            console.log('Sign up with Success', response.Message);
          } else if (response.Message) {
            console.error('Sign up Failed', response.Message);
          }
          form.reset();
          this.isRegistered = true;
        },
        error: (error) => {
          console.error('POST request failed', error);
          if (error.status === 400) {
            console.error('Bad Request:', error.error);
          } else if (error.status === 401) {
            console.error('Unauthorized:', error.error);
          } else {
            console.error('An unexpected error occurred:', error);
          }
        },
      });
  }

  login(requestBody: any, form: FormGroup) {
    this.http
      .post<any>('https://localhost:7282/api/Auth/Authenticate', requestBody)
      .subscribe({
        next: (response) => {
          console.log('POST request successful', response);
          if (response.Success) {
            console.log('Sign in with Success', response.Message);
          } else if (response.Message) {
            console.error('Sign in Failed', response.Message);
          }
          localStorage.setItem('name', response.name);
          localStorage.setItem('email', response.email);
          localStorage.setItem('token_valid', response.token);
          localStorage.setItem('Create Data token', response.createdDate);
          localStorage.setItem('End Data token', response.expiresDate);
          this.isSubmitted = true;
          form.reset();
        },
        error: (error) => {
          this.isSubmitted = false;
          console.error('POST request failed', error);
        },
      });
  }

  get getName() {
    return localStorage.getItem('name');
  }

  get isAuthenticated() {
    return !!localStorage.getItem('token_valid');
  }
  // goToHomePage() {
  //   if (this.isSubmitted==true) {
  //     this.router.navigateByUrl('http://localhost:4200/');
  //     console.log('go to home page')
  //   }
  // }

  logout() {
    localStorage.clear;
    localStorage.removeItem('email');
    localStorage.removeItem('token_valid');
    this.isSubmitted = false;
  }
}
