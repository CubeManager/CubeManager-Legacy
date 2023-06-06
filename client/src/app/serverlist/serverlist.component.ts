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
    // start interval to fetch servers every 10 seconds
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
    this.serverApiService.getALlServers().pipe(takeUntil(this.$destroy)).subscribe((data) => {
      next: this.servers = data;
    });
  }

  startServer() {
    //TODO Logic
  }

  stopServer() {
    //TODO Logic
  }

  reloadServer() {
    //TODO Logic
  }

  deleteServer() {

  }

  setConfigTab() {
    this._variableService.setConfigTabActive = true;
  }

}
