import { Component, Input } from '@angular/core';
import { Server } from '../core/models/server.model';

@Component({
  selector: 'app-server-detail-player-management',
  templateUrl: './server-detail-player-management.component.html',
  styleUrls: ['./server-detail-player-management.component.scss']
})
export class ServerDetailPlayerManagementComponent {

  @Input() server!: Server

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
    //TODO Logic
  }

  removePlayer() {
    //TODO Logic
  }

  banPlayer() {
    //TODO Logic
  }
}
