import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { VariableService } from '../variable.service';


@Component({
  selector: 'app-server-detail',
  templateUrl: './server-detail.component.html',
  styleUrls: ['./server-detail.component.scss']
})
export class ServerDetailComponent implements OnInit {

  // TODO: Add interface
  server: any;
  servers = [{ name: "Server 1", memory: 1020, cpu: 5, currentPlayers: 4, maxPlayers: 10, state: "Started" }, { name: "Server 2", memory: 800, cpu: 10, currentPlayers: 2, maxPlayers: 10, state: "Stopped" }, { name: "Skyblock", memory: 200, cpu: 6, currentPlayers: 1, maxPlayers: 5, state: "Stopped" }, { name: "Server 4", memory: 2000, cpu: 25, currentPlayers: 5, maxPlayers: 15, state: "Started" }];

  tabs = [
    "Console",
    "Logs",
    "Players",
    "Whitelist & Bans",
    "Config",
    "Plugins",
  ]

  activeTab = 4;

  constructor(private route: ActivatedRoute, private readonly _variableService: VariableService) {
    
  }

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    const serverName = (routeParams.get('serverName'));

    // this.server = serverService.getServer(serverId)
    this.server = this.servers.find((server) => server.name === serverName);

    if (this._variableService.setConfigTabActive) {
      this.activeTab = 4;
      this._variableService.setConfigTabActive = false;
    }
  }

  switchTab(index: number) {
    this.activeTab = index;
  }

  startServer(serverName: string) {
    // TODO: Implement startServer()
  }

  stopServer(serverName: string) {
    // TODO: Implement stopServer()
  }

  restartServer(serverName: string) {
    this.stopServer(serverName);
    this.startServer(serverName);
  }
}
