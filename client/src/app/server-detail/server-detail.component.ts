import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { VariableService } from '../variable.service';
import { ServerApiService } from '../core/services/serverApi.service';
import { Server } from '../core/models/server.model';


@Component({
  selector: 'app-server-detail',
  templateUrl: './server-detail.component.html',
  styleUrls: ['./server-detail.component.scss']
})
export class ServerDetailComponent implements OnInit {

  public server: Server = {} as Server;

  tabs = [
    "Console",
    "Logs",
    "Players",
    "Whitelist & Bans",
    "Config",
    "Plugins",
  ]

  activeTab = 0;

  constructor(private route: ActivatedRoute, private readonly _variableService: VariableService, private serverApiService: ServerApiService) {

  }

  ngOnInit(): void {
    // http://localhost:4200/servers/test
    // get server name from url (test in this case)
    const routeParams = this.route.snapshot.paramMap;
    const serverName = (routeParams.get('serverName'));
    // this.server = serverService.getServer(serverId)

    this.serverApiService.getServerByName(serverName!).subscribe((data) => {
      next: this.server = data;
    });

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
