import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private apiUrl = `${environment.apiUrl}/login`;
  
    constructor(private http: HttpClient) { }

    login(credentials: {username: string, password: string}): Observable<any> {
      return this.http.post(this.apiUrl, credentials);
    }
}
