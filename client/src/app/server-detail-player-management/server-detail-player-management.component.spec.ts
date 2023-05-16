import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServerDetailPlayerManagementComponent } from './server-detail-player-management.component';

describe('ServerDetailPlayerManagementComponent', () => {
  let component: ServerDetailPlayerManagementComponent;
  let fixture: ComponentFixture<ServerDetailPlayerManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ServerDetailPlayerManagementComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ServerDetailPlayerManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
