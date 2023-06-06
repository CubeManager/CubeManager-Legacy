import { AfterViewInit, Component, ElementRef, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Server } from '../core/models/server.model';
import { SignalRService } from '../core/services/signalR.service';
import { BehaviorSubject, Subject, takeUntil } from 'rxjs';
import { FormControl, FormGroup } from '@angular/forms';
import { ServerApiService } from '../core/services/serverApi.service';

@Component({
  selector: 'app-server-detail-console',
  templateUrl: './server-detail-console.component.html',
  styleUrls: ['./server-detail-console.component.scss']
})
export class ServerDetailConsoleComponent implements OnDestroy, OnInit{
  @Input() server!: Server;
  @ViewChild('logsContainer') logsContainer!: ElementRef<HTMLDivElement>;

  public $consoleMessages: BehaviorSubject<string[]> = new BehaviorSubject<string[]>([]);
  public $preLog: BehaviorSubject<string[]> = new BehaviorSubject<string[]>([]);

  public commandLine: FormGroup = new FormGroup({
    command: new FormControl('')
  });

  private $destroy = new Subject<void>();

  constructor(private signalRService: SignalRService, private serverApiService: ServerApiService) {}

  ngOnInit() {
    this.signalRService.startConnection();
    if(this.server.isRunning) {
      this.fetchLogs();
    }
    this.signalRService.addMessageReceivedListener((serverName: string, message: string) => {
      if (serverName === this.server.serverName) {
        this.$consoleMessages.next([...this.$consoleMessages.value, message]);
        this.scrollToBottom();
      }
    });
  }

  ngOnDestroy(): void {
    this.signalRService.stopConnection();
    this.$destroy.next();
    this.$destroy.complete();
  }

  public submit() {
    this.sendMessage();
  }

  public clearConsole() {
    this.$consoleMessages.next([]);
  }

  private sendMessage() {
    this.signalRService.sendMessage(this.server.serverName!, this.command!.value);
    this.command!.setValue('');
  }

  private fetchLogs() {
    this.serverApiService.getServerLog(this.server.serverName!).pipe(takeUntil(this.$destroy)).subscribe({
      next: (data) => {
        this.$consoleMessages.next(data);
        this.scrollToBottom();
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  private scrollToBottom(): void {
    if (this.logsContainer) {
      this.logsContainer.nativeElement.scrollTop = this.logsContainer.nativeElement.scrollHeight;
    }
  }

  get command() {
    return this.commandLine.get('command');
  }
}
