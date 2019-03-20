import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DrModuleTabComponent } from './dr-module-tab.component';

describe('DrModuleTabComponent', () => {
  let component: DrModuleTabComponent;
  let fixture: ComponentFixture<DrModuleTabComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DrModuleTabComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DrModuleTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
