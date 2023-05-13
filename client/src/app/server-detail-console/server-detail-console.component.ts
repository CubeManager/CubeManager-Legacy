import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-server-detail-console',
  templateUrl: './server-detail-console.component.html',
  styleUrls: ['./server-detail-console.component.scss']
})
export class ServerDetailConsoleComponent {
  @Input() server: any;
}
