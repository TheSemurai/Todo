import { Injectable } from '@angular/core';
import { environment } from './../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class BaseRoutingService {
  private apiUrl: string = environment.apiUrl;
  private apiDefault: string = '/api';
  protected baseUrl: string = this.apiUrl + this.apiDefault;
}
