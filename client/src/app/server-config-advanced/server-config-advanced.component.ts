import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Server } from '../core/models/server.model';

@Component({
  selector: 'app-server-config-advanced',
  templateUrl: './server-config-advanced.component.html',
  styleUrls: ['./server-config-advanced.component.scss']
})
export class ServerConfigAdvancedComponent implements OnInit {

  @Input() server!: Server
  newConfig: any;

  constructor(private route: ActivatedRoute) {

  }

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    const serverName = (routeParams.get('serverName'));

    // this.server = serverService.getServer(serverName);
    this.newConfig = this.server;
  }

  cancel() {
    //TODO Logic
  }

  save() {
    //TODO Logic
  }
}
