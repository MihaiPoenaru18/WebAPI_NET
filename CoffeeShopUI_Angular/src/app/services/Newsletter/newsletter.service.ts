import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NewsletterService {

  constructor(private http: HttpClient) { }
   
  addUserToNewsletter(){
    return 'https://localhost:7282/api/AddUserToNewsLetter'
  }
}
