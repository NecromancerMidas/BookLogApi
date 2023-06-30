using System.Data.SqlClient;
using BookLogApi.Data;
using BookLogApi.Model;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookLogApi.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        /*private readonly string _connectiongString;
    
        public BooksRepository(string connectiongString)
        {
            _connectiongString = connectiongString;
        }*/
        private readonly BooksContext _context;

        public BooksRepository(BooksContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
          /* await using var connection = new SqlConnection(_connectiongString); // Sooooo, with Entity framework core I dont have to write my SQL, hmm odd but neat.
            return await connection.QueryAsync<Book>("SELECT * FROM Books");*/
          return await _context.Books.ToListAsync();
        }
        public async Task<Book> GetBookById(Guid id) //might not need action result.
        {
         /*   const string sqlQuery = @$"SELECT * FROM Books WHERE Id = @Id";
            await using var connection = new SqlConnection(_connectiongString);
            return await connection.QueryFirstOrDefaultAsync<Book>(sqlQuery, new { Id = id });*/
         return await _context.Books.FindAsync(id);
        }

        public async Task<Book> CreateBook(Book book)
        {
          var createdBook = new Book
            (book.Title, book.SubTitle,
                book.Author, book.Publisher,
                book.Genre, book.Subject,
                book.Description, book.Rating, book.Image);
          _context.Books.Add(createdBook);
          await _context.SaveChangesAsync();
          return createdBook;
        }

        public async Task DeleteBook(string guid)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == Guid.Parse(guid));
            _context.Books.Remove(book);
         await _context.SaveChangesAsync();

        }
    }
}
