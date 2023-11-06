import { AuthenticationService } from './auth/services/authentication.service';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { TaskClient } from './task/task.client';

@Component({
  selector: 'start-page',
  templateUrl: './start-page.component.html',
})
export class StartPageComponent implements OnInit {
  public tasks: Observable<any> = this.taskClient.getAllTasks();

  constructor(
    private authenticationService: AuthenticationService,
    private taskClient: TaskClient
  ) {}

  ngOnInit(): void {}

  logout(): void {
    this.authenticationService.logout();
  }
}
