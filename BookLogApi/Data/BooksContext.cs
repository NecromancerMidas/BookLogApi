using BookLogApi.Model;
using Microsoft.EntityFrameworkCore;

namespace BookLogApi.Data
{
    public class BooksContext : DbContext
    {

        public BooksContext(DbContextOptions<BooksContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
    }
}
