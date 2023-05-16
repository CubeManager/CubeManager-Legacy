import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-server-config-advanced',
  templateUrl: './server-config-advanced.component.html',
  styleUrls: ['./server-config-advanced.component.scss']
})
export class ServerConfigAdvancedComponent implements OnInit {
  
  server = { name: "Server 1", memory: 1020, cpu: 5, currentPlayers: 4, maxPlayers: 10, state: "Started" };
  newConfig: any;

  constructor(private route: ActivatedRoute) {
    
  }

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    const serverName = (routeParams.get('serverName'));

    // this.server = serverService.getServer(serverName);
    this.newConfig = this.server;
  }

  cancel() {
    //TODO Logic
  }

  save() {
    //TODO Logic
  }
}
