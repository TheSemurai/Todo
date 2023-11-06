import { environment } from './../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Task } from './model/task';

@Injectable({
  providedIn: 'root',
})
export class TaskClient {
  adress: string = '/task';

  constructor(private http: HttpClient) {}

  getAllTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(
      environment.apiUrl + this.adress + '/getAllTasks'
    );
  }
}
