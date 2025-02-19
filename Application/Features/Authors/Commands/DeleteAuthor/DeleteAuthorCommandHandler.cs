using Infrastructure.Repositories.Authors;
using MediatR;

namespace Application.Features.Authors.Commands;

public sealed class DeleteAuthorCommandHandler(IAuthorRepository _authorRepository) : IRequestHandler<DeleteAuthorCommand, Unit>
{
    public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine("reached");
        var author = await _authorRepository.GetById(request.Id);

        if (author == null)
        {
            throw new KeyNotFoundException("Author not found");
        }

        await _authorRepository.Delete(author);

        return Unit.Value;
    }
}