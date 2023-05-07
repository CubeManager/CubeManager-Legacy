import { Component } from '@angular/core';

@Component({
  selector: 'app-server-detail-player-management',
  templateUrl: './server-detail-player-management.component.html',
  styleUrls: ['./server-detail-player-management.component.scss']
})
export class ServerDetailPlayerManagementComponent {

  whiteList = [
    { name: 'Player1' },
    { name: 'Player2' },
    { name: 'Player3' },
    { name: 'Player4' },
    { name: 'Player5' },
  ];

  banList = [
    { name: 'Player1' },
    { name: 'Player2' },
    { name: 'Player3' },
    { name: 'Player4' },
    { name: 'Player5' },
  ];

  addPlayer() {

  }

  removePlayer() {

  }

  banPlayer() {

  }
}
