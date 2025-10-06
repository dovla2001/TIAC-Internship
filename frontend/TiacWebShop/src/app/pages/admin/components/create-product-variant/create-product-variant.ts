import { Component, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProductService } from '../../../../core/services/product.service';
import { catchError, Observable, Subject, tap } from 'rxjs';
import { AttributeService } from '../../../../core/services/attribute.service';
import { ProductVariant } from '../../../../core/services/product.variant';
import { ProductForAdmin } from '../../../../core/models/product.model';
import { AttributeWithValues } from '../../../../core/models/attributes.models';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-create-product-variant',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './create-product-variant.html',
  styleUrl: './create-product-variant.css'
})

export class CreateProductVariant implements OnDestroy {
  variantForm: FormGroup;
  products$!: Observable<ProductForAdmin[]>;
  attributes$!: Observable<AttributeWithValues[]>;
  private destroy$ = new Subject<void>();
  successMessage: string | null = null;
  errorMessage: string | null = null;

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private attributeService: AttributeService,
    private variantService: ProductVariant
  ) {
    this.variantForm = this.fb.group({
      productId: [null, Validators.required],
      price: [null, [Validators.required, Validators.min(0)]],
      variantValues: this.fb.array([])
    });
  }

  ngOnInit(): void {
    this.products$ = this.productService.getAllProductsForAdmin();
    this.attributes$ = this.attributeService.getAttributesWithValues();
    this.addVariantValue();
  }

  get variantValues(): FormArray {
    return this.variantForm.get('variantValues') as FormArray;
  }

  newVariantValueGroup(): FormGroup {
    return this.fb.group({
      attributeId: [null, Validators.required],
      valueId: [null, Validators.required]
    });
  }

  addVariantValue(): void {
    this.variantValues.push(this.newVariantValueGroup());
  }

  removeVariantValue(index: number): void {

    if (this.variantValues.length > 1) {
      this.variantValues.removeAt(index);
    }
  }

  public onSubmit(): void {
    this.successMessage = null;
    this.errorMessage = null;

    if (this.variantForm.invalid) {
      this.variantForm.markAllAsTouched();
      return;
    }

    const formValue = this.variantForm.value;
  
    const requestData = {
      productId: formValue.productId,
      price: formValue.price,
      attributeValueIds: formValue.variantValues.map((v: any) => v.valueId).filter((id: number | null) => id !== null) 
    };

   this.variantService.createVariant(requestData).pipe(
    tap(() => {
      this.successMessage = 'The variant has been successfully created!';
      this.variantForm.reset();
      this.variantValues.clear();
      this.addVariantValue();
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

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
