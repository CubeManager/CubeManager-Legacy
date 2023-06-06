import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VariableService {
  public setConfigTabActive = false;

  themeChange: Subject<string> = new Subject<string>();

  selectedTheme: string = '';

  changeTheme(selectedTheme: string): void {
    this.themeChange.next(selectedTheme);
    this.selectedTheme = selectedTheme;
  }
}
