import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CartIcon } from './cart-icon';

describe('CartIcon', () => {
  let component: CartIcon;
  let fixture: ComponentFixture<CartIcon>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CartIcon]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CartIcon);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
