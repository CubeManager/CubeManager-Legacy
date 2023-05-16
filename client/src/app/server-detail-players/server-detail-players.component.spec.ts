import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServerDetailPlayersComponent } from './server-detail-players.component';

describe('ServerDetailPlayersComponent', () => {
  let component: ServerDetailPlayersComponent;
  let fixture: ComponentFixture<ServerDetailPlayersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ServerDetailPlayersComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ServerDetailPlayersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
