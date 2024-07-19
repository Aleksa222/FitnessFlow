import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { AbstractControl, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, HttpClientModule, RouterModule],
  templateUrl: './register.component.html'
})
export class RegisterComponent {
  registerForm: FormGroup;
  isSubmitted = false;
  errorMessage: string | null = null;
  errorTimeout: any;

  constructor(
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private router: Router,
  ) {
    this.registerForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]],
    }, {
      validator: this.mustMatch('password', 'confirmPassword')
    });
  }

  get formControls() { return this.registerForm.controls; }

  mustMatch(password: string, confirmPassword: string) {
    return (control: AbstractControl) => {
      const formGroup = control as FormGroup;
      const passwordControl = formGroup.controls[password];
      const confirmPasswordControl = formGroup.controls[confirmPassword];

      if (confirmPasswordControl.errors && !confirmPasswordControl.errors['mustMatch']) {
        return;
      }

      if (passwordControl.value !== confirmPasswordControl.value) {
        confirmPasswordControl.setErrors({ mustMatch: true });
      } else {
        confirmPasswordControl.setErrors(null);
      }
    }
  }

  register() {
    this.isSubmitted = true;
    if (this.registerForm.invalid) {
      return;
    }
    this.authService.register(this.registerForm.value).subscribe(
      (response) => {
        localStorage.setItem('token', response.token);
        this.router.navigate(['home']);
      },
      (error) => {
        if (error.status === 400) {
          this.errorMessage = 'That email is already in use!  Have you tried logging in?';
        } else {
          this.errorMessage = 'An error occurred. Please try again later.';
        }
        this.setErrorTimeout();
        console.error(error);
      }
    );
  }

  setErrorTimeout() {
    if (this.errorTimeout) {
      clearTimeout(this.errorTimeout);
    }
    this.errorTimeout = setTimeout(() => {
      this.errorMessage = null;
    }, 1000);
  }
}
