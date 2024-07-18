import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, HttpClientModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm: FormGroup;
  isSubmitted = false;
  errorMessage: string | null = null;
  errorTimeout: any;

  constructor(
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private router: Router,
  ) {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  get formControls() { return this.loginForm.controls; }

  login() {
    this.isSubmitted = true;
    this.errorMessage = null;
    if (this.loginForm.invalid) {
      return;
    }
    this.authService.login(this.loginForm.value).subscribe(
      (response) => {
        localStorage.setItem('token', response.token);
        this.router.navigate(['home']);
      },
      (error) => {
        if (error.status === 400) {
          this.errorMessage = 'Invalid email or password. Please try again.';
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
