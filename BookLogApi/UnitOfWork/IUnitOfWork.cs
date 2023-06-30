using BookLogApi.Model;
using BookLogApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookLogApi.UnitOfWork
{
    public interface IUnitOfWork
    {
        IBooksRepository Books { get; }
        public Task<int> SaveChangesAsync();
        public Task<Book> UpdateChangesAsync(Guid id, Book book);
        public Task<Book> AddNewBook(Book book);
        public Task DeleteBook(string guid);
    }
}
