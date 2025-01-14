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
}
