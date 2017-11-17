using Chapter33_BlogLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Chapter33_BlogDal
{
    public class CategoryDal : BaseDal<Category>, ICategoryDal
    {
        public CategoryDal() : base(new BlogContext())
        {
        }
    }
    public class CommentDal : BaseDal<Comment>, ICommentDal
    {
        public CommentDal() : base(new BlogContext())
        {
        }
    }
    public class BlogDal : BaseDal<Blog>, IBlogDal
    {
        public BlogDal() : base(new BlogContext())
        {
        }
    }
}
