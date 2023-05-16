import { Component } from '@angular/core';
import { Router} from '@angular/router';

@Component({
  selector: 'app-drawer',
  templateUrl: './drawer.component.html',
  styleUrls: ['./drawer.component.scss']
})
export class DrawerComponent {
  constructor(private router: Router) {}

  home(){
    this.router.navigate(['/']);
  }

  settings(){
    this.router.navigate(['/settings']);
  }

  upload(){}

  servers(){
    this.router.navigate(['/servers']);
  }

  newserver(){
    this.router.navigate(['/newserver']);
  }
}
