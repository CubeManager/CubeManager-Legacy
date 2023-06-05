import { Injectable } from '@angular/core';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class ServerService {
  constructor(private apiService: ApiService) { }

  public createServer() {
    this.apiService.post('http://localhost:4200/api/servers', { name: 'test' }).subscribe((data) => {});
  }
}
