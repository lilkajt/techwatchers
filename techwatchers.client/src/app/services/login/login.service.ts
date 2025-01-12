import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.service';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private apiUrl = `${environment.apiUrl}/login`;
  private logoutApiUrl = `${environment.apiUrl}/logout`;
  
    constructor(private http: HttpClient) { }

    login(data: any): Observable<any> {
      return this.http.post(this.apiUrl, data);
    }
    
    logout(): Observable<any> {
      return this.http.post(this.logoutApiUrl, {});
    }
}
