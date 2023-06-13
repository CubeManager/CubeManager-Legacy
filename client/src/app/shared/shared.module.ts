import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DrawerComponent } from './drawer/drawer.component';
import { LoaderComponent } from './loader/loader.component';



@NgModule({
  declarations: [
    DrawerComponent,
    LoaderComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    DrawerComponent,
    LoaderComponent
  ]
})
export class SharedModule { }
