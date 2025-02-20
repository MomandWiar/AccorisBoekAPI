using Application.Features.Authors.Queries;
using Application.DTOs;
using Domain.Entities;
using Infrastructure.Repositories.Author;
using Moq;

public class GetAuthorByIdQueryHandlerTests
{
    private readonly Mock<IAuthorRepository> _authorRepositoryMock;
    private readonly GetAuthorByIdQueryHandler _handler;

    public GetAuthorByIdQueryHandlerTests()
    {
        _authorRepositoryMock = new Mock<IAuthorRepository>();
        _handler = new GetAuthorByIdQueryHandler(_authorRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_AuthorDto_When_Author_Exists()
    {
        var authorId = 1;
        var command = new GetAuthorByIdQuery(authorId);
        var author = new AuthorEntity
        {
            Id = authorId,
            Name = "John Doe",
            Books = new[]
            {
                new BookEntity { Id = 1, Title = "Book 1", AuthorId = authorId, Author = new AuthorEntity { Name = "John Doe" } },
                new BookEntity { Id = 2, Title = "Book 2", AuthorId = authorId, Author = new AuthorEntity { Name = "John Doe" } }
            }.ToList()
        };

        _authorRepositoryMock.Setup(repo => repo.GetById(authorId))
                             .ReturnsAsync(author);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(authorId, result.Id);
        Assert.Equal("John Doe", result.Name);
        Assert.Equal(2, result.Books.Count);
        Assert.Equal("Book 1", result.Books[0].Title);
    }

    [Fact]
    public async Task Handle_Should_Throw_KeyNotFoundException_When_Author_Does_Not_Exist()
    {
        var authorId = 1;
        var command = new GetAuthorByIdQuery(authorId);

        _authorRepositoryMock.Setup(repo => repo.GetById(authorId))
                             .ReturnsAsync((AuthorEntity)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
