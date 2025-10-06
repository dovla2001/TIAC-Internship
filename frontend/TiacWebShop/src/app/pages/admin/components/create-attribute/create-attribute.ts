import { Component, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { catchError, of, Subject, takeUntil, tap } from 'rxjs';
import { AttributeService } from '../../../../core/services/attribute.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-create-attribute',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './create-attribute.html',
  styleUrl: './create-attribute.css'
})

export class CreateAttribute implements OnDestroy {
  attributeForm: FormGroup;
  successMessage: string | null = null;
  errorMessage: string | null = null;
  private destroy$ = new Subject<void>();

  constructor(
    private fb: FormBuilder,
    private attributeService: AttributeService
  ) {
    this.attributeForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]]
    });
  }

  get name() {
    return this.attributeForm.get('name');
  }

  public onSubmit(): void {
    this.successMessage = null;
    this.errorMessage = null;

    if (this.attributeForm.invalid) {
      this.attributeForm.markAllAsTouched();
      return;
    }

    this.attributeService.createAttribute(this.attributeForm.value).pipe(
      takeUntil(this.destroy$),
      tap(() => {
        this.successMessage = `Attribute "${this.attributeForm.value.name}" is successfully created!!`;
        this.attributeForm.reset();
      }),
      catchError((err: HttpErrorResponse) => {
        if (err.status == 409 && err.error?.detail) {
          this.errorMessage = err.error.detail;
        } else {
          this.errorMessage = 'An unexpected error occurred.';
        }
      return [];
      })
    ).subscribe();
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
