using E_library.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace E_library.DAL;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserBooks> UserBooks { get; set; } = null!;
    public DbSet<UserRoles> UserRoles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(builder =>
        {
            builder.Property(x => x.Name).HasMaxLength(256);
            builder.Property(x => x.Surname).HasMaxLength(256);
            builder.Property(x => x.Genre).HasMaxLength(100);
            builder.Property(x => x.ShortBio).HasMaxLength(10000);
        });

        modelBuilder.Entity<Book>(builder =>
        {
            builder.Property(x => x.Description).HasMaxLength(10000);
            builder.Property(x => x.Title).HasMaxLength(1000);
            builder.Property(x => x.Genre).HasMaxLength(100);

            builder.HasOne(x => x.Author)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.AuthorId);

            builder.HasMany(x => x.Comments)
                .WithOne(x => x.Book)
                .HasForeignKey(x => x.BookId);
        });

        modelBuilder.Entity<Comment>(builder =>
        {
            builder.Property(x => x.Title).HasMaxLength(1000);
            builder.Property(x => x.Text).HasMaxLength(10000);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.UserId);
        });

        modelBuilder.Entity<Role>(builder =>
        {
            builder.Property(x => x.Name).HasMaxLength(128);
            builder.HasData(
                new Role { Id = 1, Name = Domain.Constants.Roles.Admin },
                new Role { Id = 2, Name = Domain.Constants.Roles.Customer }
            );
        });

        modelBuilder.Entity<User>(builder =>
        {
            builder.Property(x => x.Email).HasMaxLength(1000);
            builder.Property(x => x.Name).HasMaxLength(1000);

            builder.HasMany(x => x.Books)
                .WithMany(x => x.Users)
                .UsingEntity<UserBooks>(
                    userBook => userBook
                        .HasOne(x => x.Book)
                        .WithMany(x => x.UserBooks)
                        .HasForeignKey(x => x.BookId),
                    userBook => userBook
                        .HasOne(x => x.User)
                        .WithMany(x => x.UserBooks)
                        .HasForeignKey(x => x.UserId)
                );

            builder.HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity<UserRoles>(
                    userRole => userRole
                        .HasOne(x => x.Role)
                        .WithMany(x => x.UserRoles)
                        .HasForeignKey(x => x.RoleId),
                    userRole => userRole
                        .HasOne(x => x.User)
                        .WithMany(x => x.UserRoles)
                        .HasForeignKey(x => x.UserId)
                );
        }); 
    }
}
