import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, HttpClientModule],
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
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  get formControls() { return this.registerForm.controls; }

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
          this.errorMessage = 'That email is already in use!  Go back to the login page.';
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
