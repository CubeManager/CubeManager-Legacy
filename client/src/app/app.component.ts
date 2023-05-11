import { Component } from '@angular/core';
import { ApiService } from './core/services/api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'client';

  constructor(private apiService: ApiService){
    this.apiService.get("http://localhost:4200/api/servers").pipe().subscribe((data) => console.log(data))
  }
}
