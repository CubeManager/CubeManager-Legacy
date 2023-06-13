import { Component } from '@angular/core';
import { ApiService } from '../core/services/api.service';
import { Server } from 'src/app/core/models/server.model';
import { Subject, interval, takeUntil } from 'rxjs';
import { ServerApiService } from '../core/services/serverApi.service';

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
  public servers: Server[] = new Array<Server>();
  private $destroy: Subject<void> = new Subject<void>();


  constructor(apiService: ApiService, private serverApiService: ServerApiService){
    apiService.get<number>('http://localhost:4200/api/ram').subscribe(data => {
      this.maxMemory = data} );
      apiService.get<number>('http://localhost:4200/api/storage').subscribe(data => {
        this.storage = data} );
  }

  ngOnInit(): void {
    this.fetchRunningServers();

    interval(3000).pipe(takeUntil(this.$destroy)).subscribe(() => {
      this.fetchRunningServers();
    });
  }

  fetchRunningServers() {
    this.serverApiService.getAllServers().pipe(takeUntil(this.$destroy)).subscribe((data: Server[]) => {
      next: {
        this.servers = data.filter(server => server.isRunning);
        this.cpu = data.reduce((acc, server) => acc + server.cpu!, 0);
        this.currentMemory = data.reduce((acc, server) => acc + server.memory!, 0);
      }
    });
  }
}
