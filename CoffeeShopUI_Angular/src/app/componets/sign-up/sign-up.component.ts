import { Component, OnInit,NgZone } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormBuilder,
  Validators,
} from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { isSubscription } from 'rxjs/internal/Subscription';

@Component({
  selector: 'cs-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css'],

})
export class SignUpComponent {
  constructor(private fb: FormBuilder, private http: HttpClient,private zone: NgZone) {}
  isSubmitted = false;

  signUpForm = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
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
    isSubscripToNewsletter: false,
  });
  
  onSubmit(): void {
    {
      console.log(
        'signUpForm form',
        this.signUpForm.value,
        this.signUpForm.valid
      );
      this.zone.run(() => {
        // Your problematic code here, such as XMLHttpRequest or asynchronous operations
      });
      const headers = new HttpHeaders({
        'Content-Type': 'application/json',
        'Content-Length': '<calculated when request is sent>',
        'User-Agent': 'PostmanRuntime/7.33.0',
        'Accept-Encoding': 'gzip, deflate, br',
        Authorization: 'no Auth',
        Connection: 'keep-alive',
      });

      const requestBody = {
        email: this.signUpForm.get('email')?.value,
        firstName: this.signUpForm.get('firstName')?.value,
        lastName: this.signUpForm.get('lastName')?.value,
        role: "User",
        password: this.signUpForm.get('password')?.value,
        newsLetter: {
          email: this.signUpForm.get('email')?.value,
          name:
            this.signUpForm.get('firstName')?.value +
            ' ' +
            this.signUpForm.get('lastName')?.value,
          isActived: this.signUpForm.get('isSubscripToNewsletter')?.value,
        },
      };

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
          this.isSubmitted = true;
          this.signUpForm.reset();
        }
      });
    }
  }

  validationField(fieldname:string):string{
    const control = this.signUpForm.get(fieldname);

    if (control?.invalid && (control?.dirty || control?.touched)) {
      return 'invalid';
    }
    if(control?.valid){
      return 'valid';
    }
    return 'normal';
                    
  }
  
  onUserInput(event: any) {
    let inputText = event.target.value;
  }

  MessagePlaceholder(labelname: string,placeholder:string): string {
    const control = this.signUpForm.get(labelname);

    if (control?.invalid && (control?.dirty || control?.touched)) {
      return 'Required';
    }
    return placeholder;
  }
}
