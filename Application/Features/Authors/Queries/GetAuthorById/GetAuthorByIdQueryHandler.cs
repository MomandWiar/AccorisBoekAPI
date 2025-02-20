using Application.DTOs;
using Infrastructure.Repositories.Author;
using MediatR;

namespace Application.Features.Authors.Queries;

public sealed class GetAuthorByIdQueryHandler(IAuthorRepository _authorRepository) : IRequestHandler<GetAuthorByIdQuery, AuthorDto>
{
    public async Task<AuthorDto> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetById(request.Id);

        if (author == null)
        {
            throw new KeyNotFoundException("Author not found");
        }

        var authorDto = new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
            Books = author.Books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                AuthorId = b.AuthorId,
                AuthorName = b.Author.Name
            }).ToList()
        };

        return authorDto;
    }
}