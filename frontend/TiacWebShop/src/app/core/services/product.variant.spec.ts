import { TestBed } from '@angular/core/testing';

import { ProductVariant } from './product.variant';

describe('ProductVariant', () => {
  let service: ProductVariant;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProductVariant);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
