import { Component, OnInit, signal } from '@angular/core';
import { Post } from '../models/post.model';
import { ActivatedRoute, Router } from '@angular/router';
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

  Math = Math;
  currentPage = 1;
  pageSize = 5;
  totalPosts = 0;

  constructor(private route: ActivatedRoute, private postService: PostService, private router: Router) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.category = params.get('category');
      this.loadPosts();
    });
  }

  loadPosts(): void {
    this.postService.getPostsByCategory(this.currentPage, this.pageSize, this.category!).subscribe(res => {
      this.posts.set(res.posts);
      this.totalPosts = res.totalCount
    });
  }
  navigateToPost(post: Post): void {
    this.router.navigate([`/forum/${post.category.name}/${post.id}`]);
  }

  changePage(page: number): void {
    this.currentPage = page;
    this.loadPosts();
  }
}