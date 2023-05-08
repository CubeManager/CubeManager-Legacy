import { Injectable } from '@angular/core';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class ServerService {
  constructor(private apiService: ApiService) { }

  public createServer() {
    this.apiService.post('http://localhost:3000/api/server', { name: 'test' }).subscribe((data) => {});

    // Because were using generics in the ApiService, we can and SHOULD also do this:
    // this.apiService.post<Model>('http://localhost:3000/api/server', { ... } ).subscribe((data) => {});
  }

  public getServerList() {
    return this.apiService.get('http://localhost:8080/servers').subscribe((data) => {});

    // Because were using generics in the ApiService, we can and SHOULD also do this:
    // this.apiService.post<Model>('http://localhost:3000/api/server', { ... } ).subscribe((data) => {});
  }
}
