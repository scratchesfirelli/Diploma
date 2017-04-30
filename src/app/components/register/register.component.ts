import { Router } from '@angular/router';
import { AuthService } from './../../services/auth.service';
import { FormBuilder, FormGroup, AbstractControl, Validators, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

function passwordMatcher(c: AbstractControl) {
  return c.get('password').value === c.get('confirmPassword').value ? null: {nomatch: true};
}


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  form: FormGroup;
  email: AbstractControl;
  password: AbstractControl;
  confirmPassword: AbstractControl;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router) { }

  ngOnInit() {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.pattern(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/i)]],
      password: ['', [Validators.required, Validators.minLength(6),Validators.maxLength(20)]],
      confirmPassword: ['', [Validators.required]],
    }, {validator: passwordMatcher});
    this.email = this.form.controls['email'];
    this.password = this.form.controls['password'];
    this.confirmPassword = this.form.controls['confirmPassword'];
  }

  submitForm(form: FormGroup) {

    this.authService.register( this.form.value.email,  this.form.value.password).subscribe(result => {
      if (result.success) {
        this.router.navigate(['/login']);
      }
    })
  }
}
