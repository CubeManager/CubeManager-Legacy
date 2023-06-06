import { Component} from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../core/services/api.service';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { ServerApiService } from '../core/services/serverApi.service';


@Component({
  selector: 'app-newserver',
  templateUrl: './newserver.component.html',
  styleUrls: ['./newserver.component.scss']
})
export class NewserverComponent {
  serverVersions: Array<string> = [
  ]
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
  }

  getServerJars() {
    this.http.get<string[]>('api/serverjars').subscribe(
      (response) => {
        this.serverVersions = response;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  serverCreateForm = this.fb.group({
    serverName: ['', [Validators.required]],
    serverVersion: ['', [Validators.required]],
    serverType: ['VANILLA', [Validators.required]],
    gamemode: ['SURVIVAL', [Validators.required]],
    difficulty: ['EASY', [Validators.required]],
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

  changeServerVersion(e: any) {
    this.serverCreateForm.get('serverVersion')?.setValue(e.target.value, {
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
    const body = {
      'serverName': this.serverCreateForm.get('serverName')?.value,
      'serverType': this.serverCreateForm.get('serverType')?.value,
      'exactVersion': this.serverCreateForm.get('serverVersion')?.value,
      'serverProperties': {
        'gamemode': this.serverCreateForm.get('gamemode')?.value,
        'difficulty': this.serverCreateForm.get('difficulty')?.value,
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
    }
    
    //this.apiService.post('http://localhost:4200/api/servers', JSON.stringify(body)).subscribe((data) => {});
    this.serverApiService.createServer().subscribe((data) => {});
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
    this.serverCreateForm.get('serverVersion')?.setValue('Vanilla x.xx.x', {
      onlySelf: true,
    });
    this.serverCreateForm.get('serverType')?.setValue('VANILLA', {
      onlySelf: true,
    });
    this.serverCreateForm.get('gamemode')?.setValue('EASY', {
      onlySelf: true,
    });
    this.serverCreateForm.get('difficulty')?.setValue('SURVIVAL', {
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
