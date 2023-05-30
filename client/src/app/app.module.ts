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
import { ServerDetailComponent } from './server-detail/server-detail.component';
import { ServerDetailConsoleComponent } from './server-detail-console/server-detail-console.component';
import { ServerDetailLogsComponent } from './server-detail-logs/server-detail-logs.component';
import { ServerDetailPlayersComponent } from './server-detail-players/server-detail-players.component';
import { ServerDetailPlayerManagementComponent } from './server-detail-player-management/server-detail-player-management.component';
import { ServerDetailConfigComponent } from './server-detail-config/server-detail-config.component';
import { ServerDetailPluginsComponent } from './server-detail-plugins/server-detail-plugins.component';
import { ServerConfigAdvancedComponent } from './server-config-advanced/server-config-advanced.component';
import { NewserverAdvancedComponent } from './newserver-advanced/newserver-advanced.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    ServerlistComponent,
    SettingsComponent,
    NewserverComponent,
    ServerDetailComponent,
    ServerDetailConsoleComponent,
    ServerDetailLogsComponent,
    ServerDetailPlayersComponent,
    ServerDetailPlayerManagementComponent,
    ServerDetailConfigComponent,
    ServerDetailPluginsComponent,
    ServerConfigAdvancedComponent,
    NewserverAdvancedComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule,
    SharedModule,
    MatIconModule,
    HttpClientModule,
    ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
