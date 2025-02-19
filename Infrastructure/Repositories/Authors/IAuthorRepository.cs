using Domain.Entities;

namespace Infrastructure.Repositories.Authors;

public interface IAuthorRepository
{
    public Task<List<AuthorEntity>> Get();
    public Task<AuthorEntity?> GetById(int id);
    public Task Create(AuthorEntity author);
    public Task Update(AuthorEntity author);
    public Task Delete(AuthorEntity author);
}