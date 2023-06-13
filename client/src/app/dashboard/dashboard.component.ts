import { Component } from '@angular/core';
import { ApiService } from '../core/services/api.service';
import { Server } from 'src/app/core/models/server.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
  public cpu: number = 0;
  public currentMemory: number = 0;
  public maxMemory: number = 16000;
  public storage: number = 0;
  public servers: Server[] = new Array<Server>();

  constructor(apiService: ApiService){
    apiService.get<number>('http://localhost:4200/api/ram').subscribe(data => {
      this.maxMemory = data} );
      apiService.get<number>('http://localhost:4200/api/storage').subscribe(data => {
        this.storage = data} );
}
}
