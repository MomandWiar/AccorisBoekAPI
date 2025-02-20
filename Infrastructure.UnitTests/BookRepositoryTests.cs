using Domain.Entities;
using Domain;
using Infrastructure.Repositories.Book;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories.Author;

public class BookRepositoryTests
{
    private readonly BookRepository _bookRepository;
    private readonly AppDbContext _dbContext;
    private readonly AuthorRepository _authorRepository;

    public BookRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDatabaseBooks")
            .Options;

        _dbContext = new AppDbContext(options);
        _bookRepository = new BookRepository(_dbContext);
        _authorRepository = new AuthorRepository(_dbContext);

        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
    }

    [Fact]
    public async Task Create_ShouldAddBookToDbContext()
    {
        var author = new AuthorEntity { Id = 1, Name = "John Doe" };
        await _authorRepository.Create(author);

        var book = new BookEntity { Id = 1, Title = "C# Programming", AuthorId = 1 };

        await _bookRepository.Create(book);

        var addedBook = _dbContext.Books.Where(b => b.Id == 1).FirstOrDefault();
        Assert.NotNull(addedBook);
        Assert.Equal("C# Programming", addedBook.Title);
        Assert.Equal(1, addedBook.AuthorId);  // Ensure the AuthorId is correct
    }

    [Fact]
    public async Task Delete_ShouldRemoveBookFromDbContext()
    {
        var author = new AuthorEntity { Id = 1, Name = "John Doe" };
        await _authorRepository.Create(author);

        var book = new BookEntity { Id = 1, Title = "C# Programming", AuthorId = 1 };
        await _bookRepository.Create(book);

        await _bookRepository.Delete(book);

        var deletedBook = _dbContext.Books.Where(b => b.Id == 1).FirstOrDefault();
        Assert.Null(deletedBook);
    }

    [Fact]
    public async Task Get_ShouldReturnListOfBooks()
    {
        var author = new AuthorEntity { Id = 1, Name = "John Doe" };
        await _authorRepository.Create(author);

        var books = new List<BookEntity>
        {
            new BookEntity { Id = 1, Title = "C# Programming", AuthorId = 1 },
            new BookEntity { Id = 2, Title = "ASP.NET Core", AuthorId = 1 }
        };

        await _bookRepository.Create(books[0]);
        await _bookRepository.Create(books[1]);

        var result = await _bookRepository.Get();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetById_ShouldReturnBook_WhenBookExists()
    {
        var author = new AuthorEntity { Id = 1, Name = "John Doe" };
        await _authorRepository.Create(author);

        var book = new BookEntity { Id = 1, Title = "C# Programming", AuthorId = 1 };
        await _bookRepository.Create(book);

        var result = await _bookRepository.GetById(1);

        Assert.NotNull(result);
        Assert.Equal("C# Programming", result.Title);
    }

    [Fact]
    public async Task GetById_ShouldReturnNull_WhenBookDoesNotExist()
    {
        var result = await _bookRepository.GetById(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task Update_ShouldCallUpdateOnDbContext()
    {
        var author = new AuthorEntity { Id = 1, Name = "John Doe" };
        await _authorRepository.Create(author);

        var book = new BookEntity { Id = 1, Title = "C# Programming", AuthorId = 1 };
        await _bookRepository.Create(book);

        book.Title = "Advanced C# Programming";
        await _bookRepository.Update(book);

        var updatedBook = _dbContext.Books.Where(b => b.Id == 1).FirstOrDefault();
        Assert.Equal("Advanced C# Programming", updatedBook.Title);
    }
}
