import { Component} from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../core/services/api.service';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { ServerApiService } from '../core/services/serverApi.service';
import { Server } from 'src/app/core/models/server.model';
import { ServerPropertiesInput } from '../core/models/serverPropertiesInput.model';
import { ServerInput } from '../core/models/serverInput.model';
import { HttpHeaders } from '@angular/common/http';


@Component({
  selector: 'app-newserver',
  templateUrl: './newserver.component.html',
  styleUrls: ['./newserver.component.scss']
})
export class NewserverComponent {
  serverJars: Array<string> = []
  server!: ServerInput;

  serverTypes: Array<string> = [
    "Vanilla",
    "Spigot",
    "Paper",
    "Forge",
    "Fabric",
  ];

  gamemodes: Array<string> = [
    "Survival",
    "Creative",
    "Adventure",
    "Spectator",
  ];

  difficulties: Array<string> = [
    "Peaceful",
    "Easy",
    "Normal",
    "Hard",
  ];

  serverPropertiesKey = [
    'gamemode',
    'difficulty',
    'worldName',
    'maxPlayers',
    'viewDistance',
    'simulationDistance',
    'pvp',
    'structures',
    'commandBlock',
    'forceGamemode',
    'hardcore',
    'whitelist',
    'npcs',
    'animals',
    'monsters',
    'motd',
    'worldFile',
  ];

  serverProperties = {};

  constructor(private router: Router, private apiService: ApiService, private serverApiService: ServerApiService , private fb: FormBuilder, private http: HttpClient) {
    this.getServerJars();
    this.initializeServer();
  }

  getServerJars() {
    this.http.get<string[]>('api/serverjars').subscribe(
      (response) => {
        this.serverJars = response;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  initializeServer(): void {
    this.server = new ServerInput(
      null,
      null,
      null,
      null,
      new ServerPropertiesInput(),
      null
    );
  }

  serverCreateForm = this.fb.group({
    serverName: ['', [Validators.required]],
    serverJar: ['', [Validators.required]],
    serverType: ['Vanilla', [Validators.required]],
    gamemode: ['Survival', [Validators.required]],
    difficulty: ['Easy', [Validators.required]],
    worldName: [''],
    maxPlayers: [20],
    viewDistance: [10],
    simulationDistance: [10],
    pvp: [false],
    structures: [true],
    commandBlock: [false],
    forceGamemode: [false],
    hardcore: [false],
    whitelist: [false],
    npcs: [true],
    animals: [true],
    monsters: [true],
    motd: [''],
    worldFile: [null],
  });

  changeServerJar(e: any) {
    this.serverCreateForm.get('serverJar')?.setValue(e.target.value, {
      onlySelf: true,
    });
  }

  changeServerType(e: any) {
    this.serverCreateForm.get('serverType')?.setValue(e.target.value, {
      onlySelf: true,
    });
  }  

  changeGamemode(e: any) {
    this.serverCreateForm.get('gamemode')?.setValue(e.target.value, {
      onlySelf: true,
    });
  }

  changeDifficulty(e: any) {
    this.serverCreateForm.get('difficulty')?.setValue(e.target.value, {
      onlySelf: true,
    });
  }

  changeFile(e: any) {
    if (e.target.files && e.target.files.length) {
      const file = e.target.files[0];
      this.serverCreateForm.get('worldFile')?.setValue(file, {
        onlySelf: true,
      });
    }
  }

  onSubmit() {
    debugger;

    const body = {
        'serverName': this.serverCreateForm.get('serverName')?.value,
        'serverFileName': this.serverCreateForm.get('serverJar')?.value,
        'serverType': this.serverCreateForm.get('serverType')?.value || 'Vanilla',
        'exactVersion': this.serverCreateForm.get('serverVersion')?.value || '1.19.1',
        'serverProperties': {
          'gamemode': this.serverCreateForm.get('gamemode')?.value || 'Survival',
          'difficulty': this.serverCreateForm.get('difficulty')?.value || 'Easy',
          'worldName': this.serverCreateForm.get('worldName')?.value,
          'maxPlayers': this.serverCreateForm.get('maxPlayers')?.value,
          'viewDistance': this.serverCreateForm.get('viewDistance')?.value,
          'simulationDistance': this.serverCreateForm.get('simulationDistance')?.value,
          'pvp': this.serverCreateForm.get('pvp')?.value,
          'structures': this.serverCreateForm.get('structures')?.value,
          'commandBlock': this.serverCreateForm.get('commandBlock')?.value,
          'forceGamemode': this.serverCreateForm.get('forceGamemode')?.value,
          'hardcore': this.serverCreateForm.get('hardcore')?.value,
          'whitelist': this.serverCreateForm.get('whitelist')?.value,
          'npcs': this.serverCreateForm.get('npcs')?.value,
          'animals': this.serverCreateForm.get('animals')?.value,
          'monsters': this.serverCreateForm.get('monsters')?.value,
          'motd': this.serverCreateForm.get('motd')?.value,
          'worldFile': this.serverCreateForm.get('worldFile')?.value,
        }
    };
    console.log(JSON.stringify(body));
    
    this.apiService.post('http://localhost:4200/api/servers', JSON.stringify(body), new HttpHeaders({
      'Content-Type': 'application/json'
    }))
      .subscribe((data) => {
        next: console.log(data)
        error: console.error(data)
      });
    // this.serverApiService.createServer(this.server).pipe().subscribe((data) => {
    //   next: console.log(data)
    //   error: console.error(data)
    // });
    //this.router.navigate(['/servers']);
  }
  
  cancel() {
    console.log(this.serverCreateForm.dirty)
    if (this.serverCreateForm.dirty) {
      this.resetForm();
    } else {
      this.router.navigate(['/']);
    }
  };

  resetForm() {
    this.serverCreateForm.get('serverName')?.setValue('', {
      onlySelf: true,
    });
    this.serverCreateForm.get('serverJar')?.setValue('Vanilla 1.19.1', {
      onlySelf: true,
    });
    this.serverCreateForm.get('serverType')?.setValue('Vanilla', {
      onlySelf: true,
    });
    this.serverCreateForm.get('gamemode')?.setValue('Survival', {
      onlySelf: true,
    });
    this.serverCreateForm.get('difficulty')?.setValue('Easy', {
      onlySelf: true,
    });
    this.serverCreateForm.get('worldName')?.setValue('', {
      onlySelf: true,
    });
    this.serverCreateForm.get('maxPlayers')?.setValue(20, {
      onlySelf: true,
    });
    this.serverCreateForm.get('viewDistance')?.setValue(10, {
      onlySelf: true,
    });
    this.serverCreateForm.get('simulationDistance')?.setValue(10, {
      onlySelf: true,
    });
    this.serverCreateForm.get('pvp')?.setValue(false, {
      onlySelf: true,
    });
    this.serverCreateForm.get('structures')?.setValue(true, {
      onlySelf: true,
    });
    this.serverCreateForm.get('commandBlock')?.setValue(false, {
      onlySelf: true,
    });
    this.serverCreateForm.get('forceGamemode')?.setValue(false, {
      onlySelf: true,
    });
    this.serverCreateForm.get('hardcore')?.setValue(false, {
      onlySelf: true,
    });
    this.serverCreateForm.get('whitelist')?.setValue(false, {
      onlySelf: true,
    });
    this.serverCreateForm.get('npcs')?.setValue(true, {
      onlySelf: true,
    });
    this.serverCreateForm.get('animals')?.setValue(true, {
      onlySelf: true,
    });
    this.serverCreateForm.get('monsters')?.setValue(true, {
      onlySelf: true,
    });
    this.serverCreateForm.get('motd')?.setValue('', {
      onlySelf: true,
    });
    this.serverCreateForm.get('worldFile')?.setValue(null, {
      onlySelf: true,
    });
  }
}
