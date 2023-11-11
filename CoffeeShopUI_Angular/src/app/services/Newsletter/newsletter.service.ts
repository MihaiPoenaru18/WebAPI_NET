import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root',
})
export class NewsletterService {
  constructor(private http: HttpClient) {}
  urlBase = 'https://localhost:7282/api/';

  addUserToNewsletter(requestBody: any, isSubmitted: boolean, form: FormGroup) {
    this.http
      .post<any>(this.urlBase + 'AddUserToNewsLetter', requestBody)
      .subscribe({
        next: (response) => {
          console.log('POST request successful', response);
          if (response.Success) {
            console.log('Subscriber Success', response.Message);
          } else if (response.Message) {
            console.error('Subscriber Failed', response.Message);
          }
          isSubmitted = true;
          form.reset();
        },
        error: (error) => {
          console.error('POST request failed', error);
        },
      });
  }

  getStatusNewsletter() {
    return this.urlBase + 'GetNewsLetterInfo';
  }
}
