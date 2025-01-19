import { Component, signal } from '@angular/core';
import { PostService } from '../services/post/post.service';
import { Post } from '../models/post.model';
import { AppService } from '../services/app/app.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: false,
  
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  isLoggedIn = false;

  posts = signal<Post[]>([]);
  Math = Math;
  currentPage = 1;
  pageSize = 5;
  totalPosts = 0;

  constructor(private postService: PostService, private appService: AppService, private router: Router) { }

  ngOnInit(): void {
    this.checkUserSession();
    this.loadPosts();
  }

  loadPosts(): void {
    this.postService.getPosts(this.currentPage, this.pageSize).subscribe((response) => {
      this.posts.set(response.posts);
      this.totalPosts = response.totalCount
    });
  }

  checkUserSession() {
    this.appService.checkUser().subscribe(response => {
      this.isLoggedIn = response.isLoggedIn;
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
