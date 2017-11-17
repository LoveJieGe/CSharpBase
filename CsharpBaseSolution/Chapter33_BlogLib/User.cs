using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter33_BlogLib
{
    public partial class User
    {
        public User()
        {
            Roles = new HashSet<Role>();
            Comments = new HashSet<Comment>();
            Blogs = new HashSet<Blog>();
        }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        //这里先假设User模型使用哈希和加盐技术存储用户的密码，PasswordHash包含了用户提供密码的哈希值，
        public string PasswordHash{ get; set; }
        //SecurityStamp包含了盐值。如果使用其他任何的密码存储机制，可以相应更改User模型。
        public string SecurityStamp { get; set; }

        public ICollection<Role> Roles { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }

    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }
        public int RoleID { get; set; }
        [Required]
        [StringLength(80)]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
