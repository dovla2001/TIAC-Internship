import { Component, OnDestroy } from '@angular/core';
import { catchError, Observable, Subject, switchMap, tap } from 'rxjs';
import { ProductDetailsResponse, Variant } from '../../core/models/productDetails.models';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { environment } from '../../../environments/environment';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ProductService } from '../../core/services/product.service';
import { CommonModule } from '@angular/common';
import { CartServices } from '../../core/services/cart.services';
import { of } from 'rxjs';

@Component({
  selector: 'app-product-details',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './product-details.html',
  styleUrl: './product-details.css'
})

export class ProductDetails implements OnDestroy {
  public productDetails$!: Observable<ProductDetailsResponse>;
  private product!: ProductDetailsResponse;
  public selectionMap: Map<string, Set<string>> = new Map();
  public variantSelectionForm: FormGroup;
  public currentPrice: number | null = null;
  public selectedVariant: Variant | null = null;
  public serverUrl = environment.apiUrl;
  private destroy$ = new Subject<void>();
  public successMessage: string | null = null;
  public errorMessage: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private fb: FormBuilder,
    private cartService: CartServices
  ) {
    this.variantSelectionForm = this.fb.group({});
  }

  public ngOnInit(): void {
    const productId = this.route.snapshot.paramMap.get('id');
    if (productId) {
      this.productDetails$ = this.productService.getProductDetails(+productId).pipe(
        tap(details => {
          this.product = details;
          this.buildSelectionMapAndForm();
          this.listenForFormChanges();
        })
      );
    }
  }

  private buildSelectionMapAndForm(): void {
    this.product.variants.forEach(variant => {
      variant.attributes.forEach(attr => {
        if (!this.selectionMap.has(attr.attributeName)) {
          this.selectionMap.set(attr.attributeName, new Set<string>());
        }
        this.selectionMap.get(attr.attributeName)!.add(attr.attributeValue);
      });
    });

    this.selectionMap.forEach((values, name) => {
      this.variantSelectionForm.addControl(name, new FormControl(null));
    });

    this.variantSelectionForm.addControl('quantity', new FormControl(1, [Validators.required, Validators.min(1)]));
  }

  // private listenForFormChanges(): void {
  //   this.variantSelectionForm.valueChanges.subscribe(formValues => {
  //     const matchingVariant = this.product.variants.find(variant => {
  //       return variant.attributes.every(attr => {
  //         return formValues[attr.attributeName] === attr.attributeValue;
  //       });
  //     });

  //     if (matchingVariant) {
  //       this.selectedVariant = matchingVariant;
  //       this.currentPrice = matchingVariant.price;
  //     } else {
  //       this.selectedVariant = null;
  //       this.currentPrice = null;
  //     }
  //   });
  // }

  private listenForFormChanges(): void {
  this.variantSelectionForm.valueChanges.subscribe(formValues => {
    const matchingVariant = this.product.variants.find(variant => {
      return variant.attributes.every(attr => {
        return formValues[attr.attributeName] === attr.attributeValue;
      });
    });

    if (matchingVariant) {
      this.selectedVariant = matchingVariant;
      this.currentPrice = matchingVariant.price;
    } else {
      this.selectedVariant = null;
      this.currentPrice = null;
    }
  });
}
//   private listenForFormChanges(): void {
//   this.variantSelectionForm.valueChanges.subscribe(formValues => {
//     // 1. Dobijamo imena svih atributa koje korisnik mora da izabere.
//     const attributeKeys = Array.from(this.selectionMap.keys());

//     // 2. Proveravamo da li je korisnik izabrao vrednost za svaki od atributa.
//     const allAttributesSelected = attributeKeys.every(key => formValues[key] != null);

//     // 3. Ako nisu sve opcije izabrane, resetujemo cenu i varijantu.
//     if (!allAttributesSelected) {
//       this.selectedVariant = null;
//       this.currentPrice = null;
//       return; // Prekidamo dalje izvršavanje
//     }

//     // 4. Ako su sve opcije izabrane, tek ONDA tražimo odgovarajuću varijantu.
//     const matchingVariant = this.product.variants.find(variant => {
//       return variant.attributes.every(attr => {
//         return formValues[attr.attributeName] === attr.attributeValue;
//       });
//     });

//     // Postavljamo pronađenu varijantu i cenu (ili null ako nema poklapanja)
//     this.selectedVariant = matchingVariant || null;
//     this.currentPrice = matchingVariant ? matchingVariant.price : null;
//   });
// }

  public addToCart(): void {
    if (this.selectedVariant && this.variantSelectionForm.valid) {
      const data = {
        productVariantId: this.selectedVariant?.variantId,
        quantity: this.variantSelectionForm.get('quantity')?.value
      };

      this.cartService.addItemToCart(data).pipe(
        tap(() => {
          this.successMessage = `Product successfully added to cart!`;
          alert('Product successfully added to cart!');
          this.variantSelectionForm.reset({ quantity: 1 });
        }),
        catchError(err => {
          console.error(err);
          return []; 
        })
      ).subscribe();
    } else {
      this.errorMessage = 'Please select a valid attribute combination and quantity!';
      alert('Please select a valid attribute combination and quantity!')
    }
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
