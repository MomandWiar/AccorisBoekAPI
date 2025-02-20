using Application.Features.Books.Commands;
using Domain.Entities;
using Infrastructure.Repositories.Author;
using Infrastructure.Repositories.Book;
using Moq;

public class CreateBookCommandHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly Mock<IAuthorRepository> _authorRepositoryMock;
    private readonly CreateBookCommandHandler _handler;

    public CreateBookCommandHandlerTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _authorRepositoryMock = new Mock<IAuthorRepository>();
        _handler = new CreateBookCommandHandler(_bookRepositoryMock.Object, _authorRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Create_Book_And_Return_Id()
    {
        var command = new CreateBookCommand("Sample Book", 1);
        var author = new AuthorEntity { Id = 1, Name = "John Doe" };

        _authorRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(author);

        _bookRepositoryMock.Setup(repo => repo.Create(It.IsAny<BookEntity>()))
                           .Callback<BookEntity>(book => book.Id = 1);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(1, result);
        _bookRepositoryMock.Verify(repo => repo.Create(It.IsAny<BookEntity>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_ArgumentException_When_Author_Not_Found()
    {
        var command = new CreateBookCommand("Sample Book", 999);  // Invalid AuthorId

        _authorRepositoryMock.Setup(repo => repo.GetById(999)).ReturnsAsync((AuthorEntity)null);

        await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
