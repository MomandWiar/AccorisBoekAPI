using Application.Features.Books.Commands;
using Domain.Entities;
using Infrastructure.Repositories.Book;
using Moq;

public class DeleteBookCommandHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly DeleteBookCommandHandler _handler;

    public DeleteBookCommandHandlerTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _handler = new DeleteBookCommandHandler(_bookRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Delete_Book_When_Book_Exists()
    {
        var command = new DeleteBookCommand(1);
        var book = new BookEntity { Id = 1, Title = "Sample Book" };

        _bookRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(book);

        await _handler.Handle(command, CancellationToken.None);

        _bookRepositoryMock.Verify(repo => repo.Delete(book), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_KeyNotFoundException_When_Book_Not_Found()
    {
        var command = new DeleteBookCommand(1);

        _bookRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync((BookEntity)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
