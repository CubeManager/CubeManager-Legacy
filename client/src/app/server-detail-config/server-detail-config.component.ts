import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-server-detail-config',
  templateUrl: './server-detail-config.component.html',
  styleUrls: ['./server-detail-config.component.scss']
})
export class ServerDetailConfigComponent {

  @Input() server: any;

  reset() {
    //TODO Logic
  }

  save() {
    //TODO Logic
  }
}
