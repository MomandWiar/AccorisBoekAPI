using Infrastructure.Repositories.Authors;
using MediatR;

namespace Application.Features.Authors.Commands;

public sealed class UpdateAuthorCommandHandler(IAuthorRepository _authorRepository) : IRequestHandler<UpdateAuthorCommand, Unit>
{
    public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetById(request.Id);

        if (author == null)
        {
            throw new KeyNotFoundException("Author not found");
        }

        author.Name = request.Author.Name;
        author.BirthYear = request.Author.BirthYear;

        await _authorRepository.Update(author);

        return Unit.Value;
    }
}