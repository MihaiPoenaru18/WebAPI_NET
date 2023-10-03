import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormBuilder,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'news-letter-container',
  templateUrl: './news-letter-container.component.html',
  styleUrls: ['./news-letter-container.component.css'],
})
export class NewsLetterContainerComponent {
  constructor(private fb: FormBuilder) {}

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
    this.isSubmitted = true;
    this.newsLetterForm.reset();
  }
  onUserInput(event: any) {
    let inputText = event.target.value;
    if (inputText == '') {
      this.isSubmitted = true; // Make button disabled
    } else {
      this.isSubmitted = false; // Make button enabled
    }
  }
  MessagePlaceholderFullName(): string {
    if (
      this.newsLetterForm.get('fullName')?.invalid &&
      (this.newsLetterForm.get('fullName')?.dirty ||
        this.newsLetterForm.get('fullName')?.touched)
    ) {
      return ' Required';
    } else {
      return ' Full Name';
    }
  }
  MessagePlaceholderEmail(): string {
    if (
      this.newsLetterForm.get('email')?.invalid &&
      (this.newsLetterForm.get('email')?.dirty ||
        this.newsLetterForm.get('fullName')?.touched)
    ) {
      return 'Email Required';
    } else {
      return 'Email';
    }
  }
 
}
