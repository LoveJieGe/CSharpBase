using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter33_BlogLib
{
    public class BlogContext:DbContext
    {
        public BlogContext():base("name=ConnectionString")
        { }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Role> Role { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Category和Blog的一对多关系
            modelBuilder.Entity<Category>().HasMany(c => c.Blogs)
                .WithRequired(b => b.Category)
                .HasForeignKey(b => b.CategoryID)
                .WillCascadeOnDelete(false);
            //Blog和Comment的多对一关系
            modelBuilder.Entity<Blog>().HasMany(b => b.Comments)
                .WithRequired(c => c.Blog)
                .HasForeignKey(c => c.BlogID)
                .WillCascadeOnDelete(false);
            //User和Blog之间的多对一
            modelBuilder.Entity<User>().HasMany(u => u.Blogs)
                .WithRequired(b => b.User)
                .HasForeignKey(b => b.UserID)
                .WillCascadeOnDelete(false);
            //User和Comment之间多对一
            modelBuilder.Entity<User>().HasMany(u => u.Comments)
                .WithRequired(c => c.User)
                .HasForeignKey(c => c.UserID)
                .WillCascadeOnDelete(false);
            //User和role的多对多关系
            modelBuilder.Entity<User>().HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .Map(m =>
                {
                    m.MapLeftKey("UserID");
                    m.MapRightKey("RoleID").ToTable("UserRoles");
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
