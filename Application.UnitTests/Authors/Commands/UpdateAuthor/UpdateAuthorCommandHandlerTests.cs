using Application.Features.Authors.Commands;
using Domain.Entities;
using Infrastructure.Repositories.Author;
using Moq;

public class UpdateAuthorCommandHandlerTests
{
    private readonly Mock<IAuthorRepository> _authorRepositoryMock;
    private readonly UpdateAuthorCommandHandler _handler;

    public UpdateAuthorCommandHandlerTests()
    {
        _authorRepositoryMock = new Mock<IAuthorRepository>();
        _handler = new UpdateAuthorCommandHandler(_authorRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Update_Author_When_Author_Exists()
    {
        var authorId = 1;
        var command = new UpdateAuthorCommand(authorId, new AuthorEntity { Name = "Updated Name" });
        var existingAuthor = new AuthorEntity { Id = authorId, Name = "Old Name" };

        _authorRepositoryMock.Setup(repo => repo.GetById(authorId))
                             .ReturnsAsync(existingAuthor);

        await _handler.Handle(command, CancellationToken.None);

        Assert.Equal("Updated Name", existingAuthor.Name);
        _authorRepositoryMock.Verify(repo => repo.Update(existingAuthor), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_KeyNotFoundException_When_Author_Does_Not_Exist()
    {
        var authorId = 1;
        var command = new UpdateAuthorCommand(authorId, new AuthorEntity { Name = "Updated Name" });

        _authorRepositoryMock.Setup(repo => repo.GetById(authorId))
                             .ReturnsAsync((AuthorEntity)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
