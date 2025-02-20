using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Book;

public class BookRepository(AppDbContext _context) : IBookRepository
{
    public async Task Create(BookEntity book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(BookEntity book)
    {
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }

    public async Task<List<BookEntity>> Get()
    {
        return await _context.Books.Include(b => b.Author).ToListAsync();
    }

    public async Task<BookEntity?> GetById(int id)
    {
        return await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Update(BookEntity book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }
}