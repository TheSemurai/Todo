import { AuthenticationService } from './../services/authentication.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'singup',
  templateUrl: './singup.component.html',
})
export class RegisterPageComponent implements OnInit {
  public singUpForm!: FormGroup;

  constructor(private authenticationService: AuthenticationService) {}

  ngOnInit() {
    this.singUpForm = new FormGroup({
      username: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', Validators.required),
    });
  }

  public onSubmit() {
    this.authenticationService.register(
      this.singUpForm.get('username')!.value,
      this.singUpForm.get('email')!.value,
      this.singUpForm!.get('password')!.value
    );
  }
}
