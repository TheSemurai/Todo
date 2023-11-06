import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StartPageComponent } from './start-page.component';
//import {}

const routes: Routes = [
  // {
  //   path: '',
  //   component: StartPageComponent,
  //   canActivate: [AuthGuard],
  // },
  // {
  //   path: 'login',
  //   component: LogInComponent,
  // },
  // {
  //   path: 'singup',
  //   component: SingUpComponent,
  // },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
