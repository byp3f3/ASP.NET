using Microsoft.EntityFrameworkCore;
namespace Nekrasova.Models

{
    public class AppDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<PublishingHouse> PublishingHouse { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<BookAuthor> BookAuthor { get; set; }
        public DbSet<BookGenre> BookGenre { get; set; }
        public DbSet<BookOrder> BookOrder { get; set; }
        public DbSet<BookPBH> BookPBH { get; set; }
        public DbSet<LogPass> LogPass { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
            Database.EnsureCreated();
        }

    }
}
