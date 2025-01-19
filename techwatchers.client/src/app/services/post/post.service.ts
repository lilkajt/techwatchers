import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.service';
import { Post } from '../../models/post.model';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  private apiUrl = `${environment.apiUrl}/posts`;

  constructor(private http: HttpClient) { }

  getPosts(page: number, pageSize: number): Observable<{ posts: Post[]; totalCount: number }> {
    return this.http.get<{ posts: Post[]; totalCount: number }>(`${this.apiUrl}`, {
      params: {
        page: page.toString(),
        pageSize: pageSize.toString()
      }
    });
  }

  getPostsByCategory(category: string): Observable<Post[]> {
    return this.http.get<Post[]>(`${this.apiUrl}?category=${category}`);
  }

  getPostById(id: number): Observable<Post> {
    return this.http.get<Post>(`${this.apiUrl}/${id}`);
  }

  toggleLike(postId: number, liked: boolean) {
    return this.http.post(`${this.apiUrl}/${postId}/toggle-like`, { liked });
  }
  
}
