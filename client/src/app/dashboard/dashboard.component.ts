import { Component } from '@angular/core';

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
    },
    {
      name: "3",
      memory: 1020,
      cpu: 5,
      storage: 1000
    },
    {
      name: "4",
      memory: 880,
      cpu: 10,
      storage: 2000
    },
    {
      name: "5",
      memory: 1020,
      cpu: 5,
      storage: 1000
    },
    {
      name: "6",
      memory: 880,
      cpu: 10,
      storage: 2000
    },
    {
      name: "7",
      memory: 1020,
      cpu: 5,
      storage: 1000
    },
    {
      name: "8",
      memory: 880,
      cpu: 10,
      storage: 2000
    },
    {
      name: "6",
      memory: 880,
      cpu: 10,
      storage: 2000
    },
    {
      name: "7",
      memory: 1020,
      cpu: 5,
      storage: 1000
    },
  ];

  constructor() {

    for (let server of this.servers) {
      this.cpu += server.cpu;
      this.currentMemory += server.memory;
      this.storage += server.storage;
    }
  };

}
