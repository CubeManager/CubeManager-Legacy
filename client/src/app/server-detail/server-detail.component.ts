import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { VariableService } from '../variable.service';
import { ServerApiService } from '../core/services/serverApi.service';
import { Server } from '../core/models/server.model';
import { Subject, interval, takeUntil } from 'rxjs';
import { SignalRService } from '../core/services/signalR.service';
import { ServerDetailConsoleComponent } from '../server-detail-console/server-detail-console.component';


@Component({
  selector: 'app-server-detail',
  templateUrl: './server-detail.component.html',
  styleUrls: ['./server-detail.component.scss']
})
export class ServerDetailComponent implements OnInit, OnDestroy {
  @ViewChild('console') console?: ServerDetailConsoleComponent;

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

  constructor(private route: ActivatedRoute,  private router: Router,private readonly _variableService: VariableService, private serverApiService: ServerApiService, private SignalRService: SignalRService) {}

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    const serverName = (routeParams.get('serverName'));

    this.fetchServer(serverName!);

    interval(3000).pipe(takeUntil(this.$destroy)).subscribe(() => {
      this.fetchServer(serverName!);
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
    this.console?.clearConsole();
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

  deleteServer() {
    this.serverApiService.deleteServerByName(this.server.serverName!).pipe(takeUntil(this.$destroy)).subscribe(() => {
      this.router.navigate(['/servers']);
    });
  }

  private fetchServer(serverName: string) {
    this.serverApiService.getServerByName(serverName!).pipe(takeUntil(this.$destroy)).subscribe((data) => {
      next: this.server = data;
      if (!this.server.isRunning) {
        this.server.memory = 0;
        this.server.cpu = 0;
        this.server.currentPlayers = 0;
      }
    });
  }
}
