export interface Post {
    id: number;
    title: string;
    description: string;
    dateCreation: string;
    likes: number;
    likedByUser: boolean;
    user: {
      username: string;
    };
    category: {
      name: string;
    };
  }
  