import { Component } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'news-letter-container',
  templateUrl: './news-letter-container.component.html',
  styleUrls: ['./news-letter-container.component.css'],
})
export class NewsLetterContainerComponent {
  constructor(private fb: FormBuilder, private http: HttpClient) {}

  newsLetterForm = this.fb.group({
    fullName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
  });

  isDisabled = false;
  isSubmitted = true;

  onSubmit(): void {
    console.log(
      'submitted form',
      this.newsLetterForm.value,
      this.newsLetterForm.valid
    );
    
    // Prepare the request body
    const requestBody = {

      email: this.newsLetterForm.get('email')?.value,
      name: this.newsLetterForm.get('fullName')?.value,
      isActived: true,
    };

    // Define the HTTP headers if needed (e.g., for authentication)
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Content-Length':'<calculated when request is sent>',
      'User-Agent':'PostmanRuntime/7.33.0',
      'Accept-Encoding':'gzip, deflate, br',
      'Connection': 'keep-alive',

    });

    // Make the POST request
    this.http
    .post('https://localhost:7282/api/NewsLetterController/NewsLetter/AddUserToNewsLetter', requestBody)
    .subscribe({
      next: (response) => {
        console.log('POST request successful', response);
        this.isSubmitted = true;
        this.newsLetterForm.reset();
      },
      error: (error) => {
        console.error('POST request failed', error);
      }
    });
    
  }

  onUserInput(event: any) {
    let inputText = event.target.value;
    this.isSubmitted = inputText === '';
  }

  MessagePlaceholderFullName(): string {
    return this.newsLetterForm.get('fullName')?.invalid &&
      (this.newsLetterForm.get('fullName')?.dirty ||
      this.newsLetterForm.get('fullName')?.touched)
      ? ' Required'
      : ' Full Name';
  }

  MessagePlaceholderEmail(): string {
    return this.newsLetterForm.get('email')?.invalid &&
      (this.newsLetterForm.get('email')?.dirty ||
      this.newsLetterForm.get('email')?.touched)
      ? 'Email Required'
      : 'Email';
  }
}