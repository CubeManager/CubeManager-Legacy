import { Component } from '@angular/core';
import { VariableService } from '../variable.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent {
  availableThemes: Array<string> = ["Dark", "Light", "Aqua"];
  selectedTheme: string = "dark";

  constructor(private variableService: VariableService) {}

  changeTheme(event: any): void {
    this.variableService.changeTheme(event.target.value);
  }
}
