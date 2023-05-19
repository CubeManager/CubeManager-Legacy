import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Server } from 'src/app/server.model';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ServerService {
  constructor(private apiService: ApiService) { }

  public servers: Server[] = [];
  public server: Server = new Server();

  public createServer() {
    this.apiService.post('http://localhost:4200/api/servers', { name: 'test' }).subscribe((data) => {});
  }

  public async getServers(): Promise<Server[]>{
     return firstValueFrom (this.apiService.get<Server[]>('http://localhost:4200/api/servers'));
    }

  public async getStorage(): Promise<number>{
    return firstValueFrom (this.apiService.get<number>('http://localhost:4200/api/storage'));
  }


  // public async getServerByName(name: string): Promise<Server>{
  //   return firstValueFrom (this.apiService.get<Server>('http://localhost:4200/api/servers/' + name));
  //   }
}
