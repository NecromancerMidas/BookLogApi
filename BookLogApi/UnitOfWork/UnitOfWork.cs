using BookLogApi.Data;
using BookLogApi.Model;
using BookLogApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookLogApi.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BooksContext _context;
        public IBooksRepository Books { get; private set; }
        public UnitOfWork(BooksContext context)
        {
            _context = context;
            Books = new BooksRepository(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();

        }

        public async Task<Book> UpdateChangesAsync(Guid id, Book bookToChangeTo)
        {
            var existingBook = await Books.GetBookById(id);
            existingBook.AlterBook(bookToChangeTo);
            await _context.SaveChangesAsync();
            return existingBook;
        }

        public async Task<Book> AddNewBook(Book book)
        {
           var createdBook = await Books.CreateBook(book);
           return createdBook;
        }

        public async Task DeleteBook(string guid)
        {
           await Books.DeleteBook(guid);
           await SaveChangesAsync();
        }
    }
}
