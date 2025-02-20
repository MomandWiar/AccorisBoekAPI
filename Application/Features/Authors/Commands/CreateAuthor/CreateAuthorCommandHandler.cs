using Domain.Entities;
using Infrastructure.Repositories.Author;
using MediatR;

namespace Application.Features.Authors.Commands;

public sealed class CreateAuthorCommandHandler(IAuthorRepository _authorRepository) : IRequestHandler<CreateAuthorCommand, int>
{
    public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = new AuthorEntity
        {
            Name = request.Name,
            BirthYear = request.BirthYear
        };

        await _authorRepository.Create(author);

        return author.Id;
    }
}