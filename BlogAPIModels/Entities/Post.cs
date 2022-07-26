using BlogAPIModels.Entities;
using System.ComponentModel.DataAnnotations;

namespace BlogAPIModels
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }   
        public string Text { get; set; }
        public bool IsPublished { get; set; }
        public int AuthorId { get; set; }
        public virtual User Author { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}