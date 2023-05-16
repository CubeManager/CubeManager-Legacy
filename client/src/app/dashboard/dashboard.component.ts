import { Component } from '@angular/core';
import { ApiService } from '../core/services/api.service';
import { Server } from '../server.model';

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
    setInterval(() => {
    apiService.get<Server[]>('http://localhost:4200/api/servers').subscribe(data => {
      this.cpu = data.reduce((sum, current) => sum + current.cpu, 0);
      this.currentMemory = data.reduce((sum, current) => sum + current.ram, 0);
      this.servers = data.filter(server => server.running ? server : null);
      } );
  }, 1000);
}
}
