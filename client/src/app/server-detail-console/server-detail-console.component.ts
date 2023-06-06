import { Component, Input } from '@angular/core';
import { Server } from '../core/models/server.model';
import { SignalRService } from '../core/services/signalR.service';
import { Subject } from 'rxjs';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-server-detail-console',
  templateUrl: './server-detail-console.component.html',
  styleUrls: ['./server-detail-console.component.scss']
})
export class ServerDetailConsoleComponent {
  @Input() server!: Server
  consoleMessages: string[] = [];

  chatBox: FormControl = new FormControl('');

  constructor(private signalRService: SignalRService) { }

  ngOnInit() {
    this.signalRService.startConnection();
    this.signalRService.addMessageReceivedListener((serverName: string, message: string) => {
      if (serverName === this.server.serverName) {
        this.consoleMessages.push(message);
      }
    });
  }

  sendMessage() {
    this.signalRService.sendMessage(this.server.serverName!, this.chatBox.value);
    this.chatBox.setValue('');
  }

  handleKeyUp(e: { keyCode: number; }){
    if(e.keyCode === 13){
      this.sendMessage();
    }
  }
}
