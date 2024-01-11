import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { TaskClient } from './task.client';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { Task } from './model/task';

@Component({
  selector: 'tasks',
  templateUrl: './task.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TaskComponent implements OnInit {
  tasks?: Observable<Task[]>;

  constructor(
    private formbulider: FormBuilder,
    private taskClient: TaskClient,
    private router: Router,
    private jwtHelper: JwtHelperService,
    private toastr: ToastrService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit() {
    this.getAllUserTasks();
  }

  onSubmit(item: Task) {
    this.taskClient.updateTask(item);
  }

  toggleIsComplete(values: any, task: Task) {
    console.log(values.currentTarget.checked);
    task.isComplete = values.currentTarget.checked;
  }

  getAllUserTasks() {
    this.tasks = this.taskClient.getAllTasks();
  }

  updateTitle(title: string, task: Task) {
    console.warn('before = ' + task.title);
    task.title = title;
    console.warn('after = ' + task.title);
  }

  updateContent(content: string, task: Task) {
    console.warn('before = ' + task.content);
    task.content = content;
    console.warn('after = ' + task.content);
  }

  deleteTask(id: number) {
    console.log('start deleting..');
    this.taskClient.deleteTask(id);
  }

  public logOut = () => {
    localStorage.removeItem('jwt');
  };
}
