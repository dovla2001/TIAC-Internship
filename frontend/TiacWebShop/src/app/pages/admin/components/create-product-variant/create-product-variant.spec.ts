import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateProductVariant } from './create-product-variant';

describe('CreateProductVariant', () => {
  let component: CreateProductVariant;
  let fixture: ComponentFixture<CreateProductVariant>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateProductVariant]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateProductVariant);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
