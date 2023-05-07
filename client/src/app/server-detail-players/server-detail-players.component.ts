import { Component } from '@angular/core';

@Component({
  selector: 'app-server-detail-players',
  templateUrl: './server-detail-players.component.html',
  styleUrls: ['./server-detail-players.component.scss']
})
export class ServerDetailPlayersComponent {
  players = [
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
