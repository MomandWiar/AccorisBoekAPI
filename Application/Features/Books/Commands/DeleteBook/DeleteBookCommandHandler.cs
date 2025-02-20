using Infrastructure.Repositories.Book;
using MediatR;

namespace Application.Features.Books.Commands;

public sealed class DeleteBookCommandHandler(IBookRepository _bookRepository) : IRequestHandler<DeleteBookCommand, Unit>
{
    public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetById(request.Id);

        if (book == null)
        {
            throw new KeyNotFoundException("Book not found");
        }

        await _bookRepository.Delete(book);

        return Unit.Value;
    }
}