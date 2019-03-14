import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminOneComponent } from './admin-one.component';

describe('AdminOneComponent', () => {
  let component: AdminOneComponent;
  let fixture: ComponentFixture<AdminOneComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminOneComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminOneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
