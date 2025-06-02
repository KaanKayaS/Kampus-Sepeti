using KampüsSepeti.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Persistence.Context
{
    public class AppDbContext : IdentityDbContext<User,Role,int>
    {
        public AppDbContext()
        {    
        }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFollower> UserFollowers { get; set; }
        public DbSet<FavoritePost> FavoritePosts { get; set; }

        // public DbSet<Role> Roles { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //Persistence katmanında bizim için configuration dosyalarını bulup uygulayacaktır.

            modelBuilder.Entity<UserFollower>()
                .HasKey(uf => new { uf.FollowerId, uf.FollowedId }); // Birleşik anahtar

            modelBuilder.Entity<FavoritePost>()
                .HasKey(fp => new { fp.UserId, fp.PostId });

            modelBuilder.Entity<UserFollower>()
                .HasOne(uf => uf.Follower)
                .WithMany(u => u.Following)
                .HasForeignKey(uf => uf.FollowerId)
                .OnDelete(DeleteBehavior.Restrict); // Silme davranışı için

            modelBuilder.Entity<UserFollower>()
                .HasOne(uf => uf.Followed)
                .WithMany(u => u.Followers)
                .HasForeignKey(uf => uf.FollowedId)
                .OnDelete(DeleteBehavior.Restrict); // Silme davranışı için

            modelBuilder.Entity<User>()
               .HasMany(a => a.Announcements)
               .WithOne(c => c.User)
               .HasForeignKey(c => c.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
              .HasMany(a => a.Posts)
              .WithOne(c => c.User)
              .HasForeignKey(c => c.UserId)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
               .HasMany(a => a.Comments)
               .WithOne(c => c.User)
               .HasForeignKey(c => c.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Announcement>()
                .HasOne(a => a.User)
                .WithMany(u => u.Announcements)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Cascade yerine Restrict

            modelBuilder.Entity<Announcement>()
               .HasOne(a => a.Location)
               .WithMany(u => u.Announcements)
               .HasForeignKey(a => a.LocationId)
               .OnDelete(DeleteBehavior.Restrict); // Cascade yerine Restrict

            modelBuilder.Entity<Announcement>()
                .HasMany(a => a.Comments)
                .WithOne(c => c.Announcement)
                .HasForeignKey(c => c.AnnouncementId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade 

            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict); // ON DELETE NO ACTION

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Location)
                .WithMany(l => l.Posts)
                .HasForeignKey(p => p.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
        }

       


    }
}
