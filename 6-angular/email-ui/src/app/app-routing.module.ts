import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OktaAuthGuard, OktaCallbackComponent } from '@okta/okta-angular';
import { HomeComponent } from './home/home.component';
import { InboxComponent } from './inbox/inbox.component';
import { ProfileComponent } from './profile/profile.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login/callback', component: OktaCallbackComponent },
  { path: 'inbox', component: InboxComponent, canActivate: [OktaAuthGuard] },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [OktaAuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
