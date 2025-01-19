import { Component, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PostService } from '../services/post/post.service';
import { Post } from '../models/post.model';

@Component({
  selector: 'app-post-details',
  standalone: false,
  
  templateUrl: './post-details.component.html',
  styleUrl: './post-details.component.css'
})
export class PostDetailsComponent {
  post = signal<Post | null>(null);
  message = signal<string | null>(null);
  constructor(private route: ActivatedRoute, private postService: PostService) { }

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
}
