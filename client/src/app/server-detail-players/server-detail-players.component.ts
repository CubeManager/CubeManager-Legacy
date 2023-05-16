import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-server-detail-players',
  templateUrl: './server-detail-players.component.html',
  styleUrls: ['./server-detail-players.component.scss']
})
export class ServerDetailPlayersComponent {
  @Input() server: any;

  players = [
    { name: 'Player1' },
    { name: 'Player2' },
    { name: 'Player3' },
    { name: 'Player4' },
    { name: 'Player5' },
    { name: 'Player6' },
    { name: 'Player7' },
    { name: 'Player8' },
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
