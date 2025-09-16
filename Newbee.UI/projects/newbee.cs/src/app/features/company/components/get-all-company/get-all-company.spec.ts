import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetAllCompany } from './get-all-company';

describe('GetAllCompany', () => {
  let component: GetAllCompany;
  let fixture: ComponentFixture<GetAllCompany>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetAllCompany]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetAllCompany);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
