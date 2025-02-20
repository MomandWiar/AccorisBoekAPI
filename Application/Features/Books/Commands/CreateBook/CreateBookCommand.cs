using MediatR;

namespace Application.Features.Books.Commands;

public record CreateBookCommand(string Title, int AuthorId, DateTime PublicationDate) : IRequest<int>;
