import { Component, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Form, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { catchError, Subject, takeUntil, tap } from 'rxjs';
import { CreateProductService } from '../../../../core/services/create-product.service';

@Component({
  selector: 'app-create-product',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './create-product.html',
  styleUrl: './create-product.css'
})

export class CreateProduct implements OnDestroy {
  productForm: FormGroup;
  selectedImageFile: File | null = null;
  successMessage: string | null = null;
  errorMessage: string | null = null;
  private destroy$ = new Subject<void>();

  constructor(
    private fb: FormBuilder,
    private productService: CreateProductService
  ) {
    this.productForm = this.fb.group({
      name: ['', [Validators.required]],
      description: ['', [Validators.required]],
      price: [null, [Validators.required]],
      image: [null, [Validators.required]]
    });
  }

  onFileSelected(event: Event): void {
    const element = event.currentTarget as HTMLInputElement;
    let fileList: FileList | null = element.files;

    if (fileList && fileList.length > 0) {
      this.selectedImageFile = fileList[0];
      this.productForm.patchValue({ image: this.selectedImageFile });
    }
  }

  public onSubmit(): void {
    this.successMessage = null;
    this.errorMessage = null;

    if (this.productForm.invalid || !this.selectedImageFile){
      this.productForm.markAllAsTouched();
      return;
    }

    const formData = new FormData();
    formData.append('name', this.productForm.get('name')?.value), //prvi argument mora biti isti kao na backend-u
    formData.append('description', this.productForm.get('description')?.value),
    formData.append('basePrice', this.productForm.get('price')?.value),
    formData.append('image', this.selectedImageFile, this.selectedImageFile.name);

    this.productService.createProduct(formData).pipe(
      takeUntil(this.destroy$),
      tap(() => {
        alert('Product is successfully created!');
        this.successMessage = 'Product is successfully created!';
        this.productForm.reset();
        this.selectedImageFile = null;
      }),
      catchError(error => {
        console.log(error);
        this.errorMessage = 'An error occurred while creating the attribute';
        return [];
      })
    ).subscribe();
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
