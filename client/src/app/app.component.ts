import { Component, OnInit } from '@angular/core';
import { ApiService } from './core/services/api.service';
import { VariableService } from './variable.service';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { SignalRService } from './core/services/signalR.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent  {
  title = 'client';
  selectedTheme = "dark";
  connection: any;

  constructor(private apiService: ApiService, private variableService: VariableService, private signalRService: SignalRService) {
    this.apiService.get("http://localhost:4200/api/servers").pipe().subscribe((data) => console.log(data));
    this.variableService.themeChange.subscribe(theme => this.selectedTheme = theme);
  }
}
