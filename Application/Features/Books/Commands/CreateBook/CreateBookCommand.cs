using Domain.Entities;
using MediatR;

namespace Application.Features.Books.Commands;

public record CreateBookCommand(BookEntity Book) : IRequest<int>;