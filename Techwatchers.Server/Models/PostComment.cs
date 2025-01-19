public class PostComment
{
    public int id { get; set; }
    public int post_id { get; set; }
    public int user_id { get; set; }
    public string comment_content { get; set; }
    public DateTime dateCreation { get; set; }

    public User user { get; set; }
    public Post post { get; set; }
}
