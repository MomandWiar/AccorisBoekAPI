using Domain.Entities;

namespace Infrastructure.Repositories.Book;

public interface IBookRepository
{
    public Task<List<BookEntity>> Get();
    public Task<BookEntity?> GetById(int id);
    public Task Create(BookEntity book);
    public Task Update(BookEntity book);
    public Task Delete(BookEntity book);
}