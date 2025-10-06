import { TestBed } from '@angular/core/testing';

import { CreateProductService } from './create-product.service';

describe('CreateProductService', () => {
  let service: CreateProductService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CreateProductService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
