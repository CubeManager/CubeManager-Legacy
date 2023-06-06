import { Component, Input } from '@angular/core';
import { Server } from '../core/models/server.model';

@Component({
  selector: 'app-server-detail-logs',
  templateUrl: './server-detail-logs.component.html',
  styleUrls: ['./server-detail-logs.component.scss']
})
export class ServerDetailLogsComponent {
  @Input() server!: Server
}
