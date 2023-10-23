import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormBuilder,
  Validators,
} from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css'],
})
export class SignUpComponent {
  constructor(private fb: FormBuilder, private http: HttpClient) {}
  isSubmitted = true;

  signUpForm = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: [
      '',
      [
        Validators.required /*, Validators.pattern('/(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@$!%*#?&^_-]).{8,99}/')*/,
      ],
    ],
  });
  onSubmit(): void {
    console.log(
      'signUpForm form',
      this.signUpForm.value,
      this.signUpForm.valid
    );
  }
  onUserInput(event: any) {
    let inputText = event.target.value;
    this.isSubmitted = inputText === '';
  }
  MessagePlaceholder(labelname: string): string {
    const control = this.signUpForm.get(labelname);
  
    if (control?.invalid && (control?.dirty || control?.touched)) {
      return 'Required';
    }
  
    return labelname;
  }
  // MessagePlaceholderPassword():string{
  //   return this.newsLetterForm.get('fullName')?.invalid &&
  //   (this.newsLetterForm.get('fullName')?.dirty ||
  //   this.newsLetterForm.get('fullName')?.touched || this.signUpForm.get('password').pa)
  //   ? ' Required'
  //   : ' Full Name';
  // }
}
