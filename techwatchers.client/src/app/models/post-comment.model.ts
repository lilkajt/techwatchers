export interface PostComment {
    id: number;
    post_id: number;
    user_id: number;
    comment_content: string;
    dateCreation: string;
  
    user: {
      id: number;
      username: string;
    };
    post?: { 
      id: number;
    };
  }
  