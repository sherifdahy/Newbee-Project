import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailsCompany } from './details-company';

describe('DetailsCompany', () => {
  let component: DetailsCompany;
  let fixture: ComponentFixture<DetailsCompany>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DetailsCompany]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetailsCompany);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
