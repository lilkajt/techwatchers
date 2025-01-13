import { Component, OnInit, signal } from '@angular/core';
import { Post } from '../models/post.model';
import { ActivatedRoute } from '@angular/router';
import { PostService } from '../services/post/post.service';

@Component({
  selector: 'app-forum',
  standalone: false,
  
  templateUrl: './forum.component.html',
  styleUrl: './forum.component.css'
})

export class ForumComponent implements OnInit {
  category!: string | null;
  posts = signal<Post[]>([]);

  constructor(private route: ActivatedRoute, private postService: PostService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.category = params.get('category');
      this.loadPosts();
    });
  }

  loadPosts(): void {
    this.postService.getPostsByCategory(this.category!).subscribe(posts => {
      this.posts.set(posts);
    });
  }
}