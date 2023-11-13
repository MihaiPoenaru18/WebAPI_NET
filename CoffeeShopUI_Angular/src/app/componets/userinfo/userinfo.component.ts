import { Component, OnInit, Output } from '@angular/core';
import { AuthenticatorService } from 'src/app/services/Auth/authenticator.service';
import {
  FormControl,
  FormGroup,
  FormBuilder,
  Validators,
} from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NewsletterService } from 'src/app/services/Newsletter/newsletter.service';

@Component({
  selector: 'cs-userinfo',
  templateUrl: './userinfo.component.html',
  styleUrls: ['./userinfo.component.css'],
  providers: [AuthenticatorService,NewsletterService],
})
export class UserinfoComponent {
  @Output() email: string;
  @Output() name: string;
  @Output() newsletterActived: boolean = false;

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private auth: AuthenticatorService,
    private news:NewsletterService
  ) {}
  form = this.fb.group({
    email: this.auth.getEmail,
    role: 'User',
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

  getInfo() {
    console.log('password valid', this.form.value, this.form.valid);

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Content-Length': '<calculated when request is sent>',
      'User-Agent': 'PostmanRuntime/7.33.0',
      'Accept-Encoding': 'gzip, deflate, br',
      Connection: 'keep-alive',
    });

    const requestBody = {
      email: this.form.get('email')?.value,
      role: this.form.get('role')?.value,
      password: this.form.get('password')?.value,
    };

    this.auth.userInfo(requestBody);
    this.email = this.auth.info.email;
    this.name = this.auth.info.firstName + ' ' + this.auth.info.lastName;
    this.newsletterActived = this.auth.info.isActiveNewsletter;
  }
  addToNewsletter(): void {
    const requestBody = {
      email: this.email ,
      name: this.email,
      isActived: true,
    };
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Content-Length': '<calculated when request is sent>',
      'User-Agent': 'PostmanRuntime/7.33.0',
      'Accept-Encoding': 'gzip, deflate, br',
      Connection: 'keep-alive',
    });
    this.news.addUserToNewsletter(requestBody,true,this.form)
  }
  MessagePlaceholder(labelname: string, placeholder: string): string {
    return this.form.get(labelname)?.invalid &&
      (this.form.get(labelname)?.dirty || this.form.get(labelname)?.touched)
      ? ' Required'
      : placeholder;
  }
  validationField(fieldname: string): string {
    const control = this.form.get(fieldname);

    if (control?.invalid && (control?.dirty || control?.touched)) {
      return 'invalid';
    }
    if (control?.valid) {
      return 'valid';
    }
    return 'normal';
  }
}
