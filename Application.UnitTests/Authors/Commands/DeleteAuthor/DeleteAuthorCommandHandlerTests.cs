using Application.Features.Authors.Commands;
using Domain.Entities;
using Infrastructure.Repositories.Author;
using Moq;

public class DeleteAuthorCommandHandlerTests
{
    private readonly Mock<IAuthorRepository> _authorRepositoryMock;
    private readonly DeleteAuthorCommandHandler _handler;

    public DeleteAuthorCommandHandlerTests()
    {
        _authorRepositoryMock = new Mock<IAuthorRepository>();
        _handler = new DeleteAuthorCommandHandler(_authorRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Delete_Author_When_Author_Exists()
    {
        var authorId = 1;
        var command = new DeleteAuthorCommand(authorId);
        var author = new AuthorEntity { Id = authorId, Name = "John Doe" };

        _authorRepositoryMock.Setup(repo => repo.GetById(authorId))
                             .ReturnsAsync(author);

        await _handler.Handle(command, CancellationToken.None);

        _authorRepositoryMock.Verify(repo => repo.Delete(author), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_KeyNotFoundException_When_Author_Does_Not_Exist()
    {
        var authorId = 1;
        var command = new DeleteAuthorCommand(authorId);

        _authorRepositoryMock.Setup(repo => repo.GetById(authorId))
                             .ReturnsAsync((AuthorEntity)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
