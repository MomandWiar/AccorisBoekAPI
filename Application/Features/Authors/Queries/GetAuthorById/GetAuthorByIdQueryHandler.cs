using Domain.Entities;
using Infrastructure.Repositories.Authors;
using MediatR;

namespace Application.Features.Authors.Queries.GetAuthorById;

public sealed class GetAuthorByIdQueryHandler(IAuthorRepository _authorRepository) : IRequestHandler<GetAuthorByIdQuery, AuthorEntity>
{
    public async Task<AuthorEntity> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetById(request.Id);

        if (author == null)
        {
            throw new KeyNotFoundException("Author not found");
        }

        return author;
    }
}