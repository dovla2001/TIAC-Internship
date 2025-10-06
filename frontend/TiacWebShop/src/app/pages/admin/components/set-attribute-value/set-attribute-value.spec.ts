import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SetAttributeValue } from './set-attribute-value';

describe('SetAttributeValue', () => {
  let component: SetAttributeValue;
  let fixture: ComponentFixture<SetAttributeValue>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SetAttributeValue]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SetAttributeValue);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
