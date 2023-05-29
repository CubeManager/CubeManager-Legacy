import { Component } from '@angular/core';
import { ApiService } from './core/services/api.service';
import { ConsoleHubService } from './shared/console-hub.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'client';



  constructor(private consoleHubService: ConsoleHubService){
  this.consoleHubService.startConnection();
  this.consoleHubService.addListener();
  }
}
