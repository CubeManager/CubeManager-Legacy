import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { VariableService } from '../variable.service';
import { ServerService } from '../core/services/serverApi.service';
import { Server } from '../server.model';
import { firstValueFrom } from 'rxjs';
import { GlobalVariables } from '../global';


@Component({
  selector: 'app-server-detail',
  templateUrl: './server-detail.component.html',
  styleUrls: ['./server-detail.component.scss']
})
export class ServerDetailComponent implements OnInit {

  // TODO: Add interface
  server: Server = new Server();

  tabs = [
    "Console",
    "Logs",
    "Players",
    "Whitelist & Bans",
    "Config",
    "Plugins",
  ]

  activeTab = 0;

  constructor(private route: ActivatedRoute, private readonly _variableService: VariableService, private _serverService: ServerService) { }

  ngOnInit(){
    const routeParams = this.route.snapshot.paramMap;
    const serverName = routeParams.get('serverName')  ?? '';
    const servers: Server[] = GlobalVariables.servers;
    this.server = servers.find(server => server.name == serverName) ?? new Server();

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
