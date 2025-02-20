using Application.Features.Books.Queries;
using Domain.Entities;
using Infrastructure.Repositories.Book;
using Moq;

public class GetBookByIdQueryHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly GetBookByIdQueryHandler _handler;

    public GetBookByIdQueryHandlerTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _handler = new GetBookByIdQueryHandler(_bookRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_BookDto_When_Book_Exists()
    {
        var command = new GetBookByIdQuery(1);
        var book = new BookEntity { Id = 1, Title = "Sample Book", AuthorId = 1, Author = new AuthorEntity { Name = "John Doe" } };

        _bookRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(book);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("Sample Book", result.Title);
        Assert.Equal(1, result.AuthorId);
    }

    [Fact]
    public async Task Handle_Should_Throw_KeyNotFoundException_When_Book_Not_Found()
    {
        var command = new GetBookByIdQuery(1);

        _bookRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync((BookEntity)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
