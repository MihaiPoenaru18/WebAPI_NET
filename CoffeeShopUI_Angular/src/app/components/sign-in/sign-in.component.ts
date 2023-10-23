import { Component } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent {
  constructor(private fb: FormBuilder, private http: HttpClient) {}

  signInForm= this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password:['',[Validators.required, Validators.pattern('/(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@$!%*#?&^_-]).{8,99}/')]]
  });
  onSubmit(): void {
    console.log(
      'signUpForm form',
      this.signInForm.value,
      this.signInForm.valid
    );
  }
  MessagePlaceholder(labelname:string):string{
    return this.signInForm.get(labelname)?.invalid &&
    (this.signInForm.get(labelname)?.dirty ||
    this.signInForm.get(labelname)?.touched)
    ? ' Required': labelname;
  }

}
