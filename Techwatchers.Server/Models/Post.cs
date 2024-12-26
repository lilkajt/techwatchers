public class Post
{
    public int id { get; set; }
    public int user_id { get; set; }
    public string title { get; set; }
    public int category_id { get; set; }
    public string description { get; set; }
    public DateTime dateCreation { get; set; }
    public User user { get; set; }
    public Category category { get; set; }
}
