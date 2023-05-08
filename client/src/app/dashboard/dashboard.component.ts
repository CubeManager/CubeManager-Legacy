import { Component } from '@angular/core';
import {HttpClient } from "@angular/common/http";
import { ApiService } from '../core/services/api.service';
import { ServerService } from '../core/services/serverApi.service';

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
  public servers = [
    {
      name: "1",
      memory: 1020,
      cpu: 5,
      storage: 1000
    },
    {
      name: "2",
      memory: 880,
      cpu: 10,
      storage: 2000
    }
  ]
  ;

  constructor(private http: HttpClient){

    for (let server of this.servers) {
        this.cpu += server.cpu;
        this.currentMemory += server.memory;
        this.storage += server.storage;
    }
    

    let testlol = async () => {
      let apiService = new ApiService(http);
      let serverApi = new ServerService(apiService);
      return serverApi.getServerList();
    }

    
    let process1 = testlol();
    console.log(process1);

    // this.servers[0].name = process1;
    // this.servers[0].cpu = 
    // this.servers[0].memory =
    // this.servers[0].storage = 
  };

}
