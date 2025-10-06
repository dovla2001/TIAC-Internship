import { TestBed } from '@angular/core/testing';

import { OrderServices } from './order.services';

describe('OrderServices', () => {
  let service: OrderServices;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OrderServices);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
