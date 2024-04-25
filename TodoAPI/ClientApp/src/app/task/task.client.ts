import { environment } from './../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Task } from './model/task';
import { EditCreateTask } from './model/create-edit-task';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class TaskClient {
  adress: string = '/api/Task';

  constructor(private http: HttpClient, private router: Router) {}

  public getAllTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(
      environment.apiUrl + this.adress + '/GetAllTasks'
    );
  }

  public createTask(task: EditCreateTask) {
    this.http
      .post(environment.apiUrl + this.adress + '/CreateTask', task)
      .subscribe(
        (response) => {
          console.log('Task created successfully', response);
          this.router.navigate(['/tasks']);
        },
        (error) => {
          console.error('Error creating task', error);
        }
      );
  }

  public updateTask(task: Task) {
    this.http
      .patch(
        environment.apiUrl + this.adress + '/UpdateTask/' + task.id,
        task,
        {
          headers: new HttpHeaders({
            'Content-Type': 'application/json',
          }),
        }
      )
      .subscribe(
        (response) => {
          console.log('Task updating successfully', response);
        },
        (error) => {
          console.error('Error updating task', error);
        }
      );
  }

  public deleteTask(id: number) {
    this.http
      .delete(environment.apiUrl + this.adress + '/DeleteTask/' + id)
      .subscribe(
        (response) => {
          console.log('Task deleting successfully', response);
        },
        (error) => {
          console.error('Error deleting task', error);
        }
      );
  }
}
