using Application.Features.Authors.Commands;
using Domain.Entities;
using Infrastructure.Repositories.Author;
using Moq;

public class CreateAuthorCommandHandlerTests
{
    private readonly Mock<IAuthorRepository> _authorRepositoryMock;
    private readonly CreateAuthorCommandHandler _handler;

    public CreateAuthorCommandHandlerTests()
    {
        _authorRepositoryMock = new Mock<IAuthorRepository>();
        _handler = new CreateAuthorCommandHandler(_authorRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Create_Author_And_Return_Id()
    {
        var command = new CreateAuthorCommand("John Doe");

        _authorRepositoryMock.Setup(repo => repo.Create(It.IsAny<AuthorEntity>()))
                             .Callback<AuthorEntity>(author => author.Id = 1);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(1, result);
        _authorRepositoryMock.Verify(repo => repo.Create(It.IsAny<AuthorEntity>()), Times.Once);
    }
}
