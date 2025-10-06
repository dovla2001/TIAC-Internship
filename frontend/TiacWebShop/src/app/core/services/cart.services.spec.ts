import { TestBed } from '@angular/core/testing';

import { CartServices } from './cart.services';

describe('CartServices', () => {
  let service: CartServices;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CartServices);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
