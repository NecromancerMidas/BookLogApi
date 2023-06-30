using BookLogApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace BookLogApi.Repositories
{
    public interface IBooksRepository
    {
        public Task<Book> GetBookById(Guid id);
        public Task<IEnumerable<Book>> GetAllBooks();

        public Task<Book> CreateBook(Book book);
        public Task DeleteBook(string guid);
    }
}
