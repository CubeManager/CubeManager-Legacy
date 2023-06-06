import { Component, Input } from '@angular/core';
import { Server } from '../core/models/server.model';

@Component({
  selector: 'app-server-detail-plugins',
  templateUrl: './server-detail-plugins.component.html',
  styleUrls: ['./server-detail-plugins.component.scss']
})
export class ServerDetailPluginsComponent {
  @Input() server!: Server

  plugins = [
    { name: 'Plugin1' },
    { name: 'Plugin2' },
    { name: 'Plugin3' },
    { name: 'Plugin4' },
    { name: 'Plugin5' },
  ];

  removePlugin() {
        //TODO Logic
  }

  addPlugin() {
        //TODO Logic
  }
}
