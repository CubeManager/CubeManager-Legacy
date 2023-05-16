import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServerDetailConsoleComponent } from './server-detail-console.component';

describe('ServerDetailConsoleComponent', () => {
  let component: ServerDetailConsoleComponent;
  let fixture: ComponentFixture<ServerDetailConsoleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ServerDetailConsoleComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ServerDetailConsoleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
