import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Server } from '../core/models/server.model';
import { ServerApiService } from '../core/services/serverApi.service';
import { BehaviorSubject, Subject, takeUntil } from 'rxjs';
import * as e from 'express';

@Component({
  selector: 'app-server-detail-logs',
  templateUrl: './server-detail-logs.component.html',
  styleUrls: ['./server-detail-logs.component.scss']
})
export class ServerDetailLogsComponent implements OnInit, OnDestroy{
  @Input() server!: Server
  log: BehaviorSubject<string[]> = new BehaviorSubject<string[]>([]);

  private $destroy = new Subject<void>();

  constructor(private serverApiService: ServerApiService){}

  ngOnInit(): void {
    this.fetchLogs();
  }

  ngOnDestroy(): void {
    this.$destroy.next();
    this.$destroy.complete();
  }

  fetchLogs() {
    this.serverApiService.getServerLog(this.server.serverName!).pipe(takeUntil(this.$destroy)).subscribe({
      next: (data) => {
        this.log.next(data);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
}
