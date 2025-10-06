import { TestBed } from '@angular/core/testing';

import { AttributeService } from './attribute.service';

describe('Attribute', () => {
  let service: AttributeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AttributeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
