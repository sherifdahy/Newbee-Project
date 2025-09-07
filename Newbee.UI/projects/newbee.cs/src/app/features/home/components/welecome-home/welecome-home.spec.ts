import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WelecomeHome } from './welecome-home';

describe('WelecomeHome', () => {
  let component: WelecomeHome;
  let fixture: ComponentFixture<WelecomeHome>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WelecomeHome]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WelecomeHome);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
