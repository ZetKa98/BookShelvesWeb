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
        }
    }
}