import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DrModuleComponent } from './dr-module.component';

describe('DrModuleComponent', () => {
  let component: DrModuleComponent;
  let fixture: ComponentFixture<DrModuleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DrModuleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DrModuleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
