import { Component } from '@angular/core';

@Component({
  selector: 'app-server-detail-plugins',
  templateUrl: './server-detail-plugins.component.html',
  styleUrls: ['./server-detail-plugins.component.scss']
})
export class ServerDetailPluginsComponent {
  plugins = [
    { name: 'Plugin1' },
    { name: 'Plugin2' },
    { name: 'Plugin3' },
    { name: 'Plugin4' },
    { name: 'Plugin5' },
  ];

  removePlugin() {
    
  }

  addPlugin() {
    
  }
}
