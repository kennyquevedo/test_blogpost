using BlogPost.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogPost.AppCore
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Statuses)
                .WithOne(s => s.Post)
                .IsRequired();
        }

        public DbSet<Role> Role { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<PostStatus> PostStatus { get; set; }
    }
}
