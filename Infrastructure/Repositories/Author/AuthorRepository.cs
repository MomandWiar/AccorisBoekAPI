using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Author;

public class AuthorRepository(AppDbContext _context) : IAuthorRepository
{
    public async Task Create(AuthorEntity author)
    {
        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(AuthorEntity author)
    {
        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
    }

    public async Task<List<AuthorEntity>> Get()
    {
        return await _context.Authors.ToListAsync();
    }

    public async Task<AuthorEntity?> GetById(int id)
    {
        return await _context.Authors.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task Update(AuthorEntity author)
    {
        _context.Authors.Update(author);
        await _context.SaveChangesAsync();
    }
}