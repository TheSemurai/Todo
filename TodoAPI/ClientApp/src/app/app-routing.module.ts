import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { HomepageComponent } from './home/homepage.component';
import { TaskComponent } from './task/task.component';
import { CreateEditTaskComponent } from './task/create-edit-task/create-edit-task.component';
import { AuthGuard } from './auth/auth.guard';

//all components routes
const routes: Routes = [
  {
    path: '',
    component: HomepageComponent,
    title: 'Home',
  },
  {
    path: 'tasks',
    component: TaskComponent,
    canActivate: [AuthGuard],
    title: 'Tasks',
    children: [],
  },
  {
    path: 'createTask',
    component: CreateEditTaskComponent,
    canActivate: [AuthGuard],
    title: 'Create task',
  },
  {
    path: 'login',
    component: LoginComponent,
    title: 'Login page',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
