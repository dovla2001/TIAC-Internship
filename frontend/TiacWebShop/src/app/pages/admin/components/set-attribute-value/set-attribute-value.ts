import { Component, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { catchError, Observable, Subject, takeUntil, tap } from 'rxjs';
import { AttributeResponse } from '../../../../core/models/attributes.models';
import { AttributeService } from '../../../../core/services/attribute.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-set-attribute-value',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './set-attribute-value.html',
  styleUrl: './set-attribute-value.css'
})
export class SetAttributeValue implements OnDestroy {

  valueForm: FormGroup;
  attributes$!: Observable<AttributeResponse[]>;
  successMessage: string | null = null;
  errorMessage: string | null = null;
  private destroy$ = new Subject<void>();

  constructor(
    private fb: FormBuilder,
    private attributeService: AttributeService
  ) {
    this.valueForm = this.fb.group({
      attributeId: [null, [Validators.required]],
      value: ['', [Validators.required, Validators.minLength(1)]]
    });
  }

  ngOnInit(): void {
    this.attributes$ = this.attributeService.getAttributes();
  }

  public onSubmit(): void {
    this.successMessage = null;
    this.errorMessage = null;

    if (this.valueForm.invalid) {
      this.valueForm.markAllAsTouched();
      return;
    }

    const { attributeId, value } = this.valueForm.value;

    this.attributeService.addAttributeValue(attributeId, { value }).pipe(
      takeUntil(this.destroy$),
      tap(() => {
        this.successMessage = `Value "${value}" is successfully added!`;
        this.valueForm.get('value')?.reset();
      }),
      catchError((err: HttpErrorResponse) => {
        if (err.status == 409 && err.error?.detail) {
          this.errorMessage = err.error.detail;
        } else {
          this.errorMessage = "An unexpected error occurred.";
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
