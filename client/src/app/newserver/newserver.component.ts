import { Component, HostListener, OnInit } from '@angular/core';

@Component({
  selector: 'app-newserver',
  templateUrl: './newserver.component.html',
  styleUrls: ['./newserver.component.scss']
})
export class NewserverComponent implements OnInit {

  screenSmall: boolean = false;
  
  ngOnInit(): void {
    console.log(window.innerWidth);
    
    if (window.innerHeight < 1080) {
      this.screenSmall = true;
    }
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    if (window.innerHeight < 1080) {
      this.screenSmall = true;
    } else {
      this.screenSmall = false;
    }
}

  create() {
    
  }
  
  cancel() {

  }


}
