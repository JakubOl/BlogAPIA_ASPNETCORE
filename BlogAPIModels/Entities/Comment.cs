using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPIModels.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? AuthorId { get; set; }
        public virtual User Author { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
