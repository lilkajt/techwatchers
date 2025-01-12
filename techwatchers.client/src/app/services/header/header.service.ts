import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Category } from '../../models/category.model';
import { Router } from '@angular/router';

export interface PostCreateDTO {
  categoryId: number;
  title: string;
  description: string;
}

@Injectable({
  providedIn: 'root'
})
export class HeaderService {
  private router = inject(Router);
  private apiUrl = `${environment.apiUrl}/header`;
  private loginUrl = `${environment.apiUrl}/login`
  constructor(private http: HttpClient) { }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.apiUrl);
  }

  createPost(postData: PostCreateDTO): Observable<any> {
    return this.http.post(this.apiUrl, postData);
  }

  logout(): void {
    this.http.post(`${this.loginUrl}/logout`, {}).subscribe({
      next: () => {
        console.log('User logged out successfully');
      },
      error: (err) => {
        console.error('Error during logout:', err);
      },
      complete: () => {
        this.router.navigate(['/home']);
      }
    });
  }
}
