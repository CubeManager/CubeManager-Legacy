import { Component } from '@angular/core';

@Component({
  selector: 'app-serverlist',
  templateUrl: './serverlist.component.html',
  styleUrls: ['./serverlist.component.scss']
})
export class ServerlistComponent {
  servers = [{ name: "Server 1", memory: 1020, cpu: 5, currentPlayers: 4, maxPlayers: 10, state: "Started" }, { name: "Server 2", memory: 800, cpu: 10, currentPlayers: 2, maxPlayers: 10, state: "Stopped" }, { name: "Server 2", memory: 800, cpu: 10, currentPlayers: 2, maxPlayers: 10, state: "Stopped" }, { name: "Server 2", memory: 800, cpu: 10, currentPlayers: 2, maxPlayers: 10, state: "Stopped" }]

  navigateToItem(server: object) {

  }

  startServer() {

  }

  stopServer() {

  }

  reloadServer() {

  }
}
