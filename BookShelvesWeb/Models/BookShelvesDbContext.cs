using Microsoft.EntityFrameworkCore;

namespace BookShelvesWeb.Models
{
    public class BookShelvesDbContext : DbContext
    {
        public BookShelvesDbContext(DbContextOptions<BookShelvesDbContext> options) : base(options)
        { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasIndex(genre => genre.Name);

            modelBuilder.Entity<BookGenre>().HasKey(x => new {x.GenreId, x.BookId});

            modelBuilder.Entity<Author>().HasData(new Author { Id = 1, Name = "Jane Austen" }, new Author { Id = 2,  Name = "Emily Bronte" });
            modelBuilder.Entity<Genre>().HasData(new Genre { Id = 1, Name = "Novel" }, new Genre { Id = 2,  Name = "Poetry" });
        }
    }
}