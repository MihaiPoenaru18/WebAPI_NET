import { Component } from '@angular/core';
import { AuthenticatorService } from 'src/app/services/Auth/authenticator.service';

@Component({
  selector: 'cs-order-for-register-user',
  templateUrl: './order-for-register-user.component.html',
  styleUrls: ['./order-for-register-user.component.css'],
})
export class OrderForRegisterUserComponent {
  constructor(public auth: AuthenticatorService) {}
}
