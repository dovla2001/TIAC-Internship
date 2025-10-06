import { Component, inject, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../core/services/auth.service';
import { catchError, Subject, takeUntil, tap } from 'rxjs';
import { LoginResponse } from '../../core/models/login.model';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  imports: [CommonModule, RouterLink, ReactiveFormsModule],
  standalone: true,
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login implements OnDestroy {

  loginForm: FormGroup;
  
  public loginError: string | null = null;

  private destroy$ = new Subject<void>();

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]]
    });
  }

  get email() {
    return this.loginForm.get('email');
  }

  get password() {
    return this.loginForm.get('password');
  }

  onSubmit(): void {

    this.loginError = null;

    if (this.loginForm.invalid){
      this.loginForm.markAllAsTouched();
      return;
    }

    this.authService.login(this.loginForm.value).pipe(
      takeUntil(this.destroy$),
      tap((response : LoginResponse) => {
        alert('Successful login');
        if (response.isAdmin){
          this.router.navigate(['/admin']);
        }
        else {
          this.router.navigate(['/employee-dashboard']);
        }
      }),
      catchError((err: HttpErrorResponse) => {
        if (err.status == 401 && err.error){
          this.loginError = err.error.detail || err.error.message || 'Invalid email or password.';
          this.loginForm.reset();
        }
        else{
          this.loginError = 'An unexpected error occurred. Please try again later.';
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
