import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormBuilder,
  Validators,
} from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NewsletterService } from 'src/app/services/Newsletter/newsletter.service';

@Component({
  selector: 'cs-news-letter-container',
  templateUrl: './news-letter-container.component.html',
  styleUrls: ['./news-letter-container.component.css'],
  providers: [NewsletterService],
})
export class NewsLetterContainerComponent {
  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private newsletterServices: NewsletterService
  ) {}

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
      'Content-Length': '<calculated when request is sent>',
      'User-Agent': 'PostmanRuntime/7.33.0',
      'Accept-Encoding': 'gzip, deflate, br',
      Connection: 'keep-alive',
    });
    this.newsletterServices.addUserToNewsletter(requestBody,this.isSubmitted,this.newsLetterForm)
  }

  onUserInput(event: any) {
    let inputText = event.target.value;
    this.isSubmitted = inputText === '';
  }

  MessagePlaceholder(labelname: string, placeholder: string): string {
    const control = this.newsLetterForm.get(labelname);

    if (control?.invalid && (control?.dirty || control?.touched)) {
      return 'Required';
    }
    return placeholder;
  }
}
