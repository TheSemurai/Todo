import { Component } from '@angular/core';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
})
export class LogInComponent {
  loginEmail: string = 'a@a.a';
  loginPassword: string = 'Passw0rd!';
}
