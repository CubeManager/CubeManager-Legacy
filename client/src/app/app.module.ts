import { SharedModule } from './shared/shared.module';
import { CoreModule } from './core/core.module';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { MatIconModule } from '@angular/material/icon'
import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ServerlistComponent } from './serverlist/serverlist.component';
import { SettingsComponent } from './settings/settings.component';
import { NewserverComponent } from './newserver/newserver.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    ServerlistComponent,
    SettingsComponent,
    NewserverComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule,
    SharedModule,
    MatIconModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
