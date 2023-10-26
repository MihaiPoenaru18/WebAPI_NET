import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormBuilder,
  Validators,
} from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css'],
})
export class SignInComponent {
  constructor(private fb: FormBuilder, private http: HttpClient) {}
  isSubmitted = true;

  signInForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: [
      '',
      [
        Validators.required,
        Validators.pattern(
          '(?=.*d)(?=.*[a-z])(?=.*[A-Z])(?=.*[.,@$!%*#?&^_-]).{8,99}'
        ),
      ],
    ],
  });
  roleUser = 'User';
  UserRole(): void {
    const emailControl = this.signInForm.get('email');
    if (emailControl?.value) {
      if (emailControl.value.includes('Admin')) {
        this.roleUser = 'Admin';
      }
    }
  }

  onSubmit(): void {
    console.log(
      'signUpForm form',
      this.signInForm.value,
      this.signInForm.valid
    );
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Content-Length':'<calculated when request is sent>',
      'User-Agent':'PostmanRuntime/7.33.0',
      'Accept-Encoding':'gzip, deflate, br',
      'Connection': 'keep-alive',

    });

    const requestBody = {
      email: this.signInForm.get('email')?.value,
      role: this.roleUser,
      password: this.signInForm.get('password')?.value,
    };

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
          this.isSubmitted = true;
          this.signInForm.reset();
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
  MessagePlaceholder(labelname: string): string {
    return this.signInForm.get(labelname)?.invalid &&
      (this.signInForm.get(labelname)?.dirty ||
        this.signInForm.get(labelname)?.touched)
      ? ' Required'
      : labelname;
  }
}
