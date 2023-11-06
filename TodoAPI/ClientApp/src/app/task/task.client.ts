import { environment } from './../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TaskClient {
  adress: string = '/task';

  constructor(private http: HttpClient) {}

  getAllTasks(): Observable<any> {
    return this.http.get(environment.apiUrl + this.adress + '/getAllTasks');
  }
}
