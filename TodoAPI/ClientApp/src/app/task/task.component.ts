import { Component, OnInit } from '@angular/core';
import { TaskClient } from './task.client';
import { FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { Task } from './model/task';

@Component({
  selector: 'tasks',
  templateUrl: './task.component.html',
})
export class TaskComponent implements OnInit {
  tasks?: Observable<Task[]>;

  constructor(
    private formbulider: FormBuilder,
    private taskClient: TaskClient,
    private router: Router,
    private jwtHelper: JwtHelperService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.getAllUserTasks();
  }

  toggleCheck(item: Task) {
    item.isComplete = !item.isComplete;
  }

  getAllUserTasks() {
    this.tasks = this.taskClient.getAllTasks();
  }

  updateTask(item: Task) {
    this.taskClient.updateTask(item);
  }

  deleteTask(id: number) {
    console.log('start deleting..');
    this.taskClient.deleteTask(id);
  }

  public logOut = () => {
    localStorage.removeItem('jwt');
  };
}