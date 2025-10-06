import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateAttribute } from './create-attribute';

describe('CreateAttribute', () => {
  let component: CreateAttribute;
  let fixture: ComponentFixture<CreateAttribute>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateAttribute]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateAttribute);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
