using MediatR;

namespace Application.Features.Books.Commands;

public record DeleteBookCommand(int Id) : IRequest<Unit>;
