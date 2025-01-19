import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.service';
import { Post } from '../../models/post.model';
import { PostComment } from '../../models/post-comment.model';

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

  getPostsByCategory(page: number, pageSize: number, category: string): Observable<{ posts: Post[]; totalCount: number}> {
    return this.http.get<{ posts: Post[]; totalCount: number}>(`${this.apiUrl}?category=${category}`, {
      params: {
        page: page.toString(),
        pageSize: pageSize.toString()
      }
    });
  }

  getPostById(id: number): Observable<Post> {
    return this.http.get<Post>(`${this.apiUrl}/${id}`);
  }

  getComments(postId: number): Observable<PostComment[]> {
    return this.http.get<PostComment[]>(`${this.apiUrl}/${postId}/comments`)
  }  
  
  addComment(postId: number, content: string): Observable<PostComment> {
    return this.http.post<PostComment>(`${this.apiUrl}/${postId}/comments`, { comment_content: content });
  }

  toggleLike(postId: number, liked: boolean) {
    return this.http.post(`${this.apiUrl}/${postId}/toggle-like`, { liked });
  }
  
}
