import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  private apiUrl = `${environment.apiUrl}/register`;
    
  constructor(private http: HttpClient) { }
  register(data: any): Observable<any> {
    return this.http.post(this.apiUrl, data);
  }
}
