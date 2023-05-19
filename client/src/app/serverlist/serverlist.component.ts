import { Component } from '@angular/core';
import { VariableService } from '../variable.service';
import { ServerService } from '../core/services/serverApi.service';
import { GlobalVariables } from '../global';


@Component({
  selector: 'app-serverlist',
  templateUrl: './serverlist.component.html',
  styleUrls: ['./serverlist.component.scss']
})
export class ServerlistComponent {
  public classReference = GlobalVariables;

  constructor(private readonly _variableService: VariableService, private readonly _serverService: ServerService) {}

  startServer() {
    //TODO Logic
  }

  stopServer() {
    //TODO Logic
  }

  reloadServer() {
    //TODO Logic
  }

  deleteServer() {
    
  }

  setConfigTab() {
    this._variableService.setConfigTabActive = true;
  }

}
