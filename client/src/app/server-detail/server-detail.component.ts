import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { VariableService } from '../variable.service';
import { ServerApiService } from '../core/services/serverApi.service';
import { Server } from '../core/models/server.model';
import { Subject, interval, takeUntil } from 'rxjs';
import { SignalRService } from '../core/services/signalR.service';
import * as signalR from '@microsoft/signalr';


@Component({
  selector: 'app-server-detail',
  templateUrl: './server-detail.component.html',
  styleUrls: ['./server-detail.component.scss']
})
export class ServerDetailComponent implements OnInit, OnDestroy {
  public server: Server = {} as Server;
  // Subject for Server
  public serverSubject: Subject<Server> = new Subject<Server>();

  private $destroy: Subject<void> = new Subject<void>();

  tabs = [
    "Console",
    "Logs",
    "Players",
    "Whitelist & Bans",
    "Config",
    "Plugins",
  ]

  activeTab = 0;

  constructor(private route: ActivatedRoute, private readonly _variableService: VariableService, private serverApiService: ServerApiService, private SignalRService: SignalRService) {}

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    const serverName = (routeParams.get('serverName'));

    this.fetchServer(serverName!);

    interval(3000).pipe(takeUntil(this.$destroy)).subscribe(() => {
      this.fetchServer(serverName!);
    });

    this.SignalRService.startPerformanceConnection();
    this.SignalRService.addPerformanceListener((serverName: string, cpu: number, ram: number) => {
      console.log(serverName + "  " + " loool" + " " + cpu + " " + ram)
      if (serverName === this.server.serverName) {
        this.server.cpu = cpu;
        this.server.memory = ram;
      }
    });

    if (this._variableService.setConfigTabActive) {
      this.activeTab = 4;
      this._variableService.setConfigTabActive = false;
    }
  }

  ngOnDestroy(): void {
    this.$destroy.next();
    this.$destroy.complete();
  }

  switchTab(index: number) {
    this.activeTab = index;
  }

  startServer() {
    this.serverApiService.startServer(this.server.serverName!).pipe(takeUntil(this.$destroy)).subscribe(() => {
      this.fetchServer(this.server.serverName!);
    });
  }

  stopServer() {
    this.serverApiService.stopServer(this.server.serverName!).pipe(takeUntil(this.$destroy)).subscribe(() => {
      this.fetchServer(this.server.serverName!);
    });
  }

  restartServer() {
    this.stopServer();
    this.startServer();
  }

  private fetchServer(serverName: string) {
    this.serverApiService.getServerByName(serverName!).pipe(takeUntil(this.$destroy)).subscribe((data) => {
      next: this.server = data;
    });
  }
}
