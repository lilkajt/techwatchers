import { Component, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PostService } from '../services/post/post.service';
import { Post } from '../models/post.model';
import { Subscription } from 'rxjs';
import { AppService } from '../services/app/app.service';

@Component({
  selector: 'app-post-details',
  standalone: false,
  
  templateUrl: './post-details.component.html',
  styleUrl: './post-details.component.css'
})
export class PostDetailsComponent {
  isLoggedIn = false;
  private subscription: Subscription = new Subscription();

  post = signal<Post | null>(null);
  message = signal<string | null>(null);

  constructor(private route: ActivatedRoute, private postService: PostService, private appService: AppService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const postId = params['id'];
      if (!postId || isNaN(postId)) {
        this.message.set('Invalid post id');
        return;
      }

      this.postService.getPostById(postId).subscribe(
        post => {
          if (post){
            this.message.set(null);
            this.post.set(post);
          }
          else {
            this.message.set('Post not found');
          }
        },
        error => {
          this.message.set('Error loading post');
        });
      });

      this.subscription = this.appService.isLoggedIn$.subscribe(isLoggedIn => {
        this.isLoggedIn = isLoggedIn;
      });
  
      this.checkUserSession();
  }
  toggleLike(): void {
    const currentPost = this.post();
    if (!currentPost) return;

    if (currentPost.likedByUser) {
      currentPost.likes--;
    } else {
      currentPost.likes++;
    }
    currentPost.likedByUser = !currentPost.likedByUser;

    this.post.set(currentPost);

    this.postService.toggleLike(currentPost.id, currentPost.likedByUser).subscribe(
      () => {
        console.log('Like state updated successfully');
      },
      error => {
        console.error('Error updating like state:', error);
        // Przywróć poprzedni stan w przypadku błędu
        currentPost.likedByUser = !currentPost.likedByUser;
        currentPost.likedByUser ? currentPost.likes++ : currentPost.likes--;
        this.post.set(currentPost);
      }
    );
  }

  checkUserSession() {
    this.appService.checkUser().subscribe(
      response => {
        const isLoggedIn = response.isLoggedIn;
        this.appService.updateUserStatus(isLoggedIn);
      },
      error => {
        console.error('Error checking user session:', error);
      }
    );
  }
}
