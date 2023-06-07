import { Component, OnDestroy, OnInit } from '@angular/core';
import { VariableService } from '../variable.service';
import { ServerApiService } from '../core/services/serverApi.service';
import { Server } from '../core/models/server.model';
import { Subject, interval, takeUntil } from 'rxjs';


@Component({
  selector: 'app-serverlist',
  templateUrl: './serverlist.component.html',
  styleUrls: ['./serverlist.component.scss']
})
export class ServerlistComponent implements OnInit, OnDestroy {
  public servers?: Server[];

  private $destroy = new Subject<void>();

  constructor(private readonly _variableService: VariableService, private serverApiService: ServerApiService) {
  }


  ngOnInit(): void {
    this.fetchServers();

    interval(300).pipe(takeUntil(this.$destroy)).subscribe(() => {
      this.fetchServers();
    });
  }

  ngOnDestroy(): void {
    this.$destroy.next();
    this.$destroy.complete();
  }

  fetchServers() {
    this.serverApiService.getAllServers().pipe(takeUntil(this.$destroy)).subscribe((data) => {
      next: this.servers = data;
    });
  }

  startServer(serverName: any) {
    this.serverApiService.startServer(serverName);
  }

  stopServer(serverName: any) {
    this.serverApiService.stopServer(serverName);
  }

  reloadServer(serverName: any) {
    this.stopServer(serverName);
    this.startServer(serverName);
  }

  deleteServer(serverName: any) {
    this.serverApiService.deleteServerByName(serverName);
  }

  setConfigTab() {
    this._variableService.setConfigTabActive = true;
  }

}
