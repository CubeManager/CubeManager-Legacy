import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: HubConnection;
  constructor() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('/api/hub/console')
      .build();
  }

  startConnection(): void {
    this.hubConnection.start().catch(err => console.error(err));
  }

  stopConnection(): void {
    this.hubConnection.stop().catch(err => console.error(err));
  }

  addMessageReceivedListener(callback: (serverName: string, message: string) => void): void {
    this.hubConnection.on('MessageReceived', (serverName: string, message: string) => {
      callback(serverName, message);
    });
  }

  sendMessage(serverName: string, message: string): void {
    this.hubConnection.invoke('SendMessage', serverName, message).catch(err => console.error(err));
  }
}
