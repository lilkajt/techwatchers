export interface Post {
    id: number;
    title: string;
    description: string;
    dateCreation: string;
    user: {
      username: string;
    };
    category: {
      name: string;
    };
  }
  