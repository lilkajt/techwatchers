import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.service';

@Injectable({
  providedIn: 'root'
})

export class ProfileService {

  private apiUrl = `${environment.apiUrl}/profile`;

  constructor(private http: HttpClient) { }

  updatePassword(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/update-password`, data);
  }
}