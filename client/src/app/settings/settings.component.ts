import { Component } from '@angular/core';
import { VariableService } from '../variable.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent {
  availableThemes: Array<string> = ["light", "dark", "cupcake", "bumblebee", "emerald", "corporate", "synthwave", "retro", "cyberpunk", "valentine", "halloween", "garden", "forest", "aqua", "lofi", "pastel", "fantasy", "wireframe", "black", "luxury", "dracula", "cmyk", "autumn", "business", "acid", "lemonade", "night", "coffee", "winter"];
  selectedTheme: string = '';

  constructor(private variableService: VariableService) {
    this.selectedTheme = this.variableService.selectedTheme;
  }

  changeTheme(event: any): void {
    this.variableService.changeTheme(event.target.value);
  }
}
