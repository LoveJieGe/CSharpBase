using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter33_BlogLib
{
    public partial class Category
    {
        public Category()
        {
            Blogs = new HashSet<Blog>();
        }
        public int CategoryID { get; set; }
        [Required]
        [StringLength(80)]
        public string CateGoryName { get; set; }

        public ICollection<Blog> Blogs { get; set; }
    }

    public partial class Blog
    {
        public int BlogID { get; set; }
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public int UserID { get; set; }
        public int CategoryID { get; set; }

        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }

    public partial class Comment
    {
        public int CommentID { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }

        public int UserID { get; set; }
        public int BlogID { get; set; }
        public User User { get; set; }
        public Blog Blog { get; set; }
    }
}
