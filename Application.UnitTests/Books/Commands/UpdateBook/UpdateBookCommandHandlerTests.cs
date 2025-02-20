using Application.Features.Books.Commands;
using Domain.Entities;
using Infrastructure.Repositories.Book;
using Moq;

public class UpdateBookCommandHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly UpdateBookCommandHandler _handler;

    public UpdateBookCommandHandlerTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _handler = new UpdateBookCommandHandler(_bookRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Update_Book_When_Book_Exists()
    {
        var command = new UpdateBookCommand(1, new BookEntity { Title = "Updated Book", AuthorId = 1 });
        var existingBook = new BookEntity { Id = 1, Title = "Old Book", AuthorId = 1 };

        _bookRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(existingBook);

        await _handler.Handle(command, CancellationToken.None);

        Assert.Equal("Updated Book", existingBook.Title);
        _bookRepositoryMock.Verify(repo => repo.Update(existingBook), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_KeyNotFoundException_When_Book_Not_Found()
    {
        var command = new UpdateBookCommand(1, new BookEntity { Title = "Updated Book", AuthorId = 1 });

        _bookRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync((BookEntity)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
