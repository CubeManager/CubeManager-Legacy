import { Component, Input } from '@angular/core';
import { Server } from '../core/models/server.model';

@Component({
  selector: 'app-server-detail-config',
  templateUrl: './server-detail-config.component.html',
  styleUrls: ['./server-detail-config.component.scss']
})
export class ServerDetailConfigComponent {

  @Input() server!: Server;

  reset() {
    //TODO Logic
  }

  save() {
    //TODO Logic
  }
}
