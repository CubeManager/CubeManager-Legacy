import { Component } from '@angular/core';
import { GlobalVariables } from './global';
import { ServerService } from './core/services/serverApi.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'client';

  constructor(private readonly _serverService: ServerService){
    setInterval(async () => {      
      GlobalVariables.servers = await this._serverService.getServers();
      GlobalVariables.storage = await this._serverService.getStorage();
      GlobalVariables.cpu =  GlobalVariables.servers.reduce((sum, current) => sum + current.cpu, 0);
      GlobalVariables.ram =  GlobalVariables.servers.reduce((sum, current) => sum + current.memory, 0);
    }, 1000);
  }
}
