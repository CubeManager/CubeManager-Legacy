import { Component } from '@angular/core';
import { VariableService } from '../variable.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent {
  availableThemes: Array<string> = ["Dark", "Light", "Aqua"];
  selectedTheme: string = '';

  constructor(private variableService: VariableService) {
    this.selectedTheme = this.variableService.selectedTheme;
  }

  changeTheme(event: any): void {
    this.variableService.changeTheme(event.target.value);
  }
}
