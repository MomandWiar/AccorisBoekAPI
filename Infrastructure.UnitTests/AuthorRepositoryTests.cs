using Domain.Entities;
using Domain;
using Infrastructure.Repositories.Author;
using Microsoft.EntityFrameworkCore;

public class AuthorRepositoryTests
{
    private readonly AuthorRepository _authorRepository;
    private readonly AppDbContext _dbContext;

    public AuthorRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDatabaseAuthors")
            .Options;

        _dbContext = new AppDbContext(options);
        _authorRepository = new AuthorRepository(_dbContext);

        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
    }

    [Fact]
    public async Task Create_ShouldAddAuthorToDbContext()
    {
        var author = new AuthorEntity { Id = 1, Name = "John Doe" };

        await _authorRepository.Create(author);

        var addedAuthor = _dbContext.Authors.Where(a => a.Id == 1).FirstOrDefault();
        Assert.NotNull(addedAuthor);
        Assert.Equal("John Doe", addedAuthor.Name);
    }

    [Fact]
    public async Task Delete_ShouldRemoveAuthorFromDbContext()
    {
        var author = new AuthorEntity { Id = 1, Name = "John Doe" };
        await _authorRepository.Create(author);

        await _authorRepository.Delete(author);

        var deletedAuthor = _dbContext.Authors.Where(a => a.Id == 1).FirstOrDefault();
        Assert.Null(deletedAuthor);
    }

    [Fact]
    public async Task Get_ShouldReturnListOfAuthors()
    {
        var authors = new List<AuthorEntity>
        {
            new AuthorEntity { Id = 1, Name = "John Doe" },
            new AuthorEntity { Id = 2, Name = "Jane Doe" }
        };

        await _authorRepository.Create(authors[0]);
        await _authorRepository.Create(authors[1]);

        var result = await _authorRepository.Get();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetById_ShouldReturnAuthor_WhenAuthorExists()
    {
        var author = new AuthorEntity { Id = 1, Name = "John Doe" };
        await _authorRepository.Create(author);

        var result = await _authorRepository.GetById(1);

        Assert.NotNull(result);
        Assert.Equal("John Doe", result.Name);
    }

    [Fact]
    public async Task GetById_ShouldReturnNull_WhenAuthorDoesNotExist()
    {
        var result = await _authorRepository.GetById(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task Update_ShouldCallUpdateOnDbContext()
    {
        var author = new AuthorEntity { Id = 1, Name = "John Doe" };
        await _authorRepository.Create(author);

        author.Name = "Updated Name";
        await _authorRepository.Update(author);

        var updatedAuthor = _dbContext.Authors.Where(a => a.Id == 1).FirstOrDefault();
        Assert.Equal("Updated Name", updatedAuthor.Name);
    }
}