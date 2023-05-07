import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ServerlistComponent } from './serverlist/serverlist.component';
import { SettingsComponent } from './settings/settings.component';
import { NewserverComponent } from './newserver/newserver.component';
import { ServerDetailComponent } from './server-detail/server-detail.component';

const routes: Routes = [  
  { path: '', component: DashboardComponent},
  { path: 'servers', component: ServerlistComponent},
  { path: 'servers/:serverName', component: ServerDetailComponent},
  { path: 'settings', component: SettingsComponent},
  { path: 'newserver', component: NewserverComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
