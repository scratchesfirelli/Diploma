import { AbstractControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { AuthService } from "app/services/auth.service";
import { Router } from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginError: String;
  form: FormGroup;
  email: AbstractControl;
  password: AbstractControl;
  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router) { }

  ngOnInit() {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.pattern(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/i)]],
      password: ['', [Validators.required]]
    });
    this.email = this.form.controls['email'];
    this.password = this.form.controls['password'];
    this.form.valueChanges
      .distinctUntilChanged()
      .debounceTime(4000)
      .subscribe(value => this.loginError = null);
  }

  submitForm(form: FormGroup) {

    this.authService.logIn(this.form.value.email, this.form.value.password)
      .subscribe(result => {
        if (result.success) {
          console.log(result)
          this.router.navigate(['/product/list', 1]);
        } else {
          console.log(result.error)
          this.loginError = result.error;
        }
      })
  }
}
