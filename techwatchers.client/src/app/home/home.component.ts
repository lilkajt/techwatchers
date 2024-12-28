import { Component, signal } from '@angular/core';
import { PostService } from '../services/post/post.service';  // Zakładam, że masz post.service.ts w tej samej lokalizacji
import { Post } from '../models/post.model';

@Component({
  selector: 'app-home',
  standalone: false,
  
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  posts = signal<Post[]>([]);

  constructor(private postService: PostService) { }

  ngOnInit(): void {
    this.loadPosts();
  }

  loadPosts(): void {
    this.postService.getPosts().subscribe((data: Post[]) => {
      console.log(data);
      this.posts.set(data);
    });
  }
}
