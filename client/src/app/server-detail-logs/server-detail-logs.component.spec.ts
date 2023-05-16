import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServerDetailLogsComponent } from './server-detail-logs.component';

describe('ServerDetailLogsComponent', () => {
  let component: ServerDetailLogsComponent;
  let fixture: ComponentFixture<ServerDetailLogsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ServerDetailLogsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ServerDetailLogsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
