import { Injectable } from '@angular/core';
import { BaseRoutingService } from './../base-routing.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService extends BaseRoutingService {
  protected authRoute: string = 'User';
  protected authUrl: string = this.baseUrl + this.authRoute;

  constructor(private http: HttpClient) {
    super();
  }

  public logIn(credentials: {
    username: string;
    password: string;
  }): Observable<any> {
    const url = `${this.authUrl}/login`;
    // Assuming your API expects a JSON body for authentication
    return this.http.post(url, credentials);
  }

  public singUp(credentials: {
    username: string;
    password: string;
  }): Observable<any> {
    const url = `${this.authUrl}/login`;
    // Assuming your API expects a JSON body for authentication
    return this.http.post(url, credentials);
  }
}
