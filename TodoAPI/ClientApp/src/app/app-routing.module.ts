import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { HomepageComponent } from './home/homepage.component';
import { TaskComponent } from './task/task.component';
import { CreateEditTaskComponent } from './task/create-edit-task/create-edit-task.component';

//import {}

const routes: Routes = [
  {
    path: 'home',
    component: HomepageComponent,
    //canActivate: [AuthGuard],
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'tasks',
    component: TaskComponent,
  },
  {
    path: 'create',
    component: CreateEditTaskComponent,
  },
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
