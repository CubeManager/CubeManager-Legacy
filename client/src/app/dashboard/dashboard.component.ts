import { Component } from '@angular/core';
import { ApiService } from '../core/services/api.service';
import { Server } from '../server.model';
import { GlobalVariables } from '../global';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
  public maxMemory: number = 16000;
  public referenceClass = GlobalVariables;

  constructor(apiService: ApiService){
    apiService.get<number>('http://localhost:4200/api/ram').subscribe(data => {this.maxMemory = data} );
    }
}
