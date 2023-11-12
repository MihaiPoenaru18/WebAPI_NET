import { Component, Input } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormBuilder,
  Validators,
} from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthenticatorService } from 'src/app/services/Auth/authenticator.service';

@Component({
  selector: 'cs-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css'],
  providers: [AuthenticatorService],
})
export class SignInComponent {
  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    public auth: AuthenticatorService
  ) {}
  @Input() isSubmitted = true;

  signInForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: [
      '',
      [
        Validators.required,
        Validators.pattern(
          /(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[.,@$!%*#?&^_-]).{8,99}/
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
      'Content-Length': '<calculated when request is sent>',
      'User-Agent': 'PostmanRuntime/7.33.0',
      'Accept-Encoding': 'gzip, deflate, br',
      Connection: 'keep-alive',
    });

    const requestBody = {
      email: this.signInForm.get('email')?.value,
      role: this.roleUser,
      password: this.signInForm.get('password')?.value,
    };
    this.auth.login(requestBody, this.signInForm);
    this.isSubmitted = this.auth.isSubmitted;
  }
  validationField(fieldname: string): string {
    const control = this.signInForm.get(fieldname);

    if (control?.invalid && (control?.dirty || control?.touched)) {
      return 'invalid';
    }
    if (control?.valid) {
      return 'valid';
    }
    return 'normal';
  }
  onUserInput(event: any) {
    let inputText = event.target.value;
    this.isSubmitted = inputText === '';
  }
  MessagePlaceholder(labelname: string, placeholder: string): string {
    return this.signInForm.get(labelname)?.invalid &&
      (this.signInForm.get(labelname)?.dirty ||
        this.signInForm.get(labelname)?.touched)
      ? ' Required'
      : placeholder;
  }
 
}
