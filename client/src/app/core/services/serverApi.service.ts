import { Injectable } from '@angular/core';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class ServerService {
  constructor(private apiService: ApiService) { }

  public createServer() {
    this.apiService.post('http://localhost:3000/api/server', { name: 'test' }).subscribe((data) => {});
  }
}
