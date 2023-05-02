import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
  public cpu: number = 1;
  public currentMemory: number = 1;
  public maxMemory: number = 1;
  public storage: number = 1;
}
