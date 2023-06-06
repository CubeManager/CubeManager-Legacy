import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Server } from '../models/server.model';

@Injectable({
  providedIn: 'root'
})
export class ServerApiService {
  constructor(private apiService: ApiService) { }

  public createServer() {
    this.apiService.post('http://localhost:4200/api/servers', { name: 'test' }).subscribe((data) => {});
  }

  public getServerByName(serverName: string) {
    return this.apiService.get<Server>(`http://localhost:4200/api/servers/${serverName}`);
  }

  public getALlServers() {
    return this.apiService.get<Server[]>('http://localhost:4200/api/servers');
  }
}
