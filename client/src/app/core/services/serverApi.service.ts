import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Server } from '../models/server.model';
import { ServerInput } from '../models/serverInput.model';

@Injectable({
  providedIn: 'root'
})
export class ServerApiService {
  constructor(private apiService: ApiService) { }

  public getServerByName(serverName: string) {
    return this.apiService.get<Server>(`http://localhost:4200/api/servers/${serverName}`);
  }

  public getALlServers() {
    return this.apiService.get<Server[]>('http://localhost:4200/api/servers');
  }

  public startServer(serverName: string) {
    return this.apiService.post(`http://localhost:4200/api/servers/${serverName}/start`, {});
  }

  public stopServer(serverName: string) {
    return this.apiService.delete(`http://localhost:4200/api/servers/${serverName}/stop`, {});
  }

  public createServer(server: ServerInput) {
    return this.apiService.post('http://localhost:4200/api/servers', server);
  }

  public updateServer(server: Server) {
    return this.apiService.post('http://localhost:4200/api/servers', server);
  }

  public getServerLog(serverName: string) {
    return this.apiService.get<string[]>(`http://localhost:4200/api/servers/${serverName}/log`);
  }

}
