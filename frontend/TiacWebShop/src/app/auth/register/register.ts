// register.ts
import { Component, inject, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../core/services/auth.service';
import { catchError, Subject, takeUntil, tap } from 'rxjs';
import { RegisterRequest, RegisterResponse } from '../../core/models/register.model';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, RouterLink, ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrls: ['./register.css']
})

export class Register implements OnDestroy {

  registerForm: FormGroup;

  public passwordMismatchError: string | null = null;
  public registrationError: string | null = null;

  private destroy$ = new Subject<void>();

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', Validators.required]
    });
  }

  get firstName() {
    return this.registerForm.get('firstName');
  }

  get lastName() {
    return this.registerForm.get('lastName');
  }

  get email() {
    return this.registerForm.get('email');
  }

  get password() {
    return this.registerForm.get('password');
  }

  get confirmPassword() {
    return this.registerForm.get('confirmPassword');
  }

  onSubmit(): void {

    this.passwordMismatchError = null;
    this.registrationError = null;

    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    if (this.registerForm.value.password !== this.registerForm.value.confirmPassword) {
      console.error('Passwords does not match.');
      this.passwordMismatchError = "Passwords do not match!";
      return;
    }

    const userDataToSend: RegisterRequest = this.registerForm.value;

    this.authService.register(userDataToSend).pipe(
      takeUntil(this.destroy$),
      tap((response: RegisterResponse) => {
        alert('Successful registration');
        this.router.navigate(['/']);
      }),
      catchError(err => {
        if (err.status == 409 && err.error?.detail) {
          this.registrationError = err.error.detail;
          this.email?.reset();
        } else {
          this.registrationError = 'An unexpected error occurred. Please try again later.';
        }
        return [];
      })
    ).subscribe();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}