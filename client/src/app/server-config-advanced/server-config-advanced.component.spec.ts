import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServerConfigAdvancedComponent } from './server-config-advanced.component';

describe('ServerConfigAdvancedComponent', () => {
  let component: ServerConfigAdvancedComponent;
  let fixture: ComponentFixture<ServerConfigAdvancedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ServerConfigAdvancedComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ServerConfigAdvancedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
