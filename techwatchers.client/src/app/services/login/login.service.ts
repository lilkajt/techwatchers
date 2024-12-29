import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private apiUrl = `${environment.apiUrl}/login`;
  
    constructor(private http: HttpClient) { }
  
    // create function to login
}
