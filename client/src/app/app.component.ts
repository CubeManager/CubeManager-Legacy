import { Component } from '@angular/core';

import { GlobalVariables } from './global';
import { ServerService } from './core/services/serverApi.service';
import { ApiService } from './core/services/api.service';
import { VariableService } from './variable.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'client';
  selectedTheme = "dark";

  constructor(private apiService: ApiService, private variableService: VariableService, private readonly _serverService: ServerService) {
    this.apiService.get("http://localhost:4200/api/servers").pipe().subscribe((data) => console.log(data));
    this.variableService.themeChange.subscribe(theme => this.selectedTheme = theme);
    setInterval(async () => {      
      GlobalVariables.servers = await this._serverService.getServers();
      GlobalVariables.storage = await this._serverService.getStorage();
      GlobalVariables.cpu =  GlobalVariables.servers.reduce((sum, current) => sum + current.cpu, 0);
      GlobalVariables.ram =  GlobalVariables.servers.reduce((sum, current) => sum + current.memory, 0);
    }, 1000);
  }
}
