import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServerDetailConfigComponent } from './server-detail-config.component';

describe('ServerDetailConfigComponent', () => {
  let component: ServerDetailConfigComponent;
  let fixture: ComponentFixture<ServerDetailConfigComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ServerDetailConfigComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ServerDetailConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
