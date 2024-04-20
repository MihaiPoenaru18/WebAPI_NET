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
    public auth: AuthenticatorService
  ) {}
 
}
