import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ServerlistComponent } from './serverlist/serverlist.component';
import { SettingsComponent } from './settings/settings.component';
import { NewserverComponent } from './newserver/newserver.component';
import { ServerDetailComponent } from './server-detail/server-detail.component';
import { ServerConfigAdvancedComponent } from './server-config-advanced/server-config-advanced.component';
import { NewserverAdvancedComponent } from './newserver-advanced/newserver-advanced.component';

const routes: Routes = [  
  { path: '', component: DashboardComponent},
  { path: 'servers', component: ServerlistComponent},
  { path: 'servers/:serverName', component: ServerDetailComponent},
  { path: 'settings', component: SettingsComponent},
  { path: 'newserver', component: NewserverComponent},
  { path: 'newserver/advanced', component: NewserverAdvancedComponent},
  { path: 'servers/:serverName/advanced', component: ServerConfigAdvancedComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
