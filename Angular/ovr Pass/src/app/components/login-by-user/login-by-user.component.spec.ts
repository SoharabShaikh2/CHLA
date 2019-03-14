import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginByUserComponent } from './login-by-user.component';

describe('LoginByUserComponent', () => {
  let component: LoginByUserComponent;
  let fixture: ComponentFixture<LoginByUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LoginByUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginByUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
