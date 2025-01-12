import { Component, signal } from '@angular/core';
import { PostService } from '../services/post/post.service';  // Zakładam, że masz post.service.ts w tej samej lokalizacji
import { Post } from '../models/post.model';
import { AppService } from '../services/app/app.service';

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

  constructor(private postService: PostService, private appService: AppService) { }

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

  changePage(page: number): void {
    this.currentPage = page;
    this.loadPosts();
  }
}
