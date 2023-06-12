import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: HubConnection;
  private performanceHubConnection: HubConnection;

  constructor() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('/api/hub/console')
      .build();

    this.performanceHubConnection = new HubConnectionBuilder()
    .withUrl('/api/hub/performance')
    .build();
  }

  startConnection(): void {
    this.hubConnection.start().catch(err => console.error(err));
  }

  startPerformanceConnection(): void {
    this.performanceHubConnection.start().catch(err => console.error(err));

  }
  
  stopConnection(): void {
    this.hubConnection.stop().catch(err => console.error(err));
  }

  addMessageReceivedListener(callback: (serverName: string, message: string) => void): void {
    this.hubConnection.on('MessageReceived', (serverName: string, message: string) => {
      callback(serverName, message);
    });
  }

  addPerformanceListener(callback: (serverName: string, cpu: number, ram: number) => void): void {
    this.performanceHubConnection.on('performanceReceived', (serverName: string, cpu: number, ram: number) => {
      callback(serverName, cpu, ram);
    });
  }

  sendMessage(serverName: string, message: string): void {
    this.hubConnection.invoke('SendMessage', serverName, message).catch(err => console.error(err));
  }
}
