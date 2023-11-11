import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root',
})
export class AuthenticatorService {
  constructor(private http: HttpClient) {}
  urlBase = 'https://localhost:7282/api/Auth/';

  register(requestBody: any, isSubmitted: boolean, form: FormGroup) {
    this.http.post<any>("https://localhost:7282/api/Auth/RegisterUser", requestBody).subscribe({
      next: (response) => {
        console.log('POST request successful', response);
        if (response.Success) {
          console.log('Sign up with Success', response.Message);
        } else if (response.Message) {
          console.error('Sign up Failed', response.Message);
        }
        isSubmitted = true;
        form.reset();
      },
    });
  }

  login(requestBody: any, isSubmitted: boolean, form: FormGroup) {
    this.http.post<any>("https://localhost:7282/api/Auth/Authenticate", requestBody).subscribe({
      next: (response) => {
        console.log('POST request successful', response);
        if (response.Success) {
          console.log('Sign in with Success', response.Message);
        } else if (response.Message) {
          console.error('Sign in Failed', response.Message);
        }
        localStorage.setItem('email',response.email)
        localStorage.setItem('fullName',response.firstName+" "+response.lastName)
        localStorage.setItem('token_valid',response.token)
        localStorage.setItem('Create Data token',response.createdDate)
        localStorage.setItem('End Data token',response.expiresDate)
        isSubmitted = true;
        form.reset();
      },
      error: (error) => {
        console.error('POST request failed', error);
      },
    });
  }
  get getEmail(){
    return localStorage.getItem('email')
  }
  get isAuthenticated(){
    return !!localStorage.getItem('token_valid')
  }
}
