using Domain.Entities;
using MediatR;

namespace Application.Features.Books.Commands;

public record UpdateBookCommand(int Id, BookEntity Book) : IRequest<Unit>;
