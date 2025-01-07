import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Category } from '../../models/category.model';

export interface PostCreateDTO {
  categoryId: number;
  title: string;
  description: string;
}

@Injectable({
  providedIn: 'root'
})
export class HeaderService {
  private apiUrl = `${environment.apiUrl}/header`;
  constructor(private http: HttpClient) { }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.apiUrl);
  }

  createPost(postData: PostCreateDTO): Observable<any> {
    return this.http.post(this.apiUrl, postData);
  }
}
