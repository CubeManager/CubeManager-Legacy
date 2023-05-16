import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServerDetailPluginsComponent } from './server-detail-plugins.component';

describe('ServerDetailPluginsComponent', () => {
  let component: ServerDetailPluginsComponent;
  let fixture: ComponentFixture<ServerDetailPluginsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ServerDetailPluginsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ServerDetailPluginsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
