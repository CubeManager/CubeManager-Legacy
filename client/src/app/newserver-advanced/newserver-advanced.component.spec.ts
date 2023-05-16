import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewserverAdvancedComponent } from './newserver-advanced.component';

describe('NewserverAdvancedComponent', () => {
  let component: NewserverAdvancedComponent;
  let fixture: ComponentFixture<NewserverAdvancedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewserverAdvancedComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NewserverAdvancedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
