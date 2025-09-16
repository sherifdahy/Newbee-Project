import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainFormCompany } from './main-form-company';

describe('MainFormCompany', () => {
  let component: MainFormCompany;
  let fixture: ComponentFixture<MainFormCompany>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MainFormCompany]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MainFormCompany);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
