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

  constructor(private postService: PostService, private appService: AppService) { }

  ngOnInit(): void {
    this.checkUserSession();
    this.loadPosts();
  }

  loadPosts(): void {
    this.postService.getPosts().subscribe((data: Post[]) => {
      this.posts.set(data);
    });
  }

  checkUserSession() {
    this.appService.checkUser().subscribe(response => {
      this.isLoggedIn = response.isLoggedIn;
      console.log('User is logged in: ', this.isLoggedIn);
    });
  }
}
