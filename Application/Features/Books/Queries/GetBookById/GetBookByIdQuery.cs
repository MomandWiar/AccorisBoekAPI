using Domain.Entities;
using MediatR;

namespace Application.Features.Books.Queries;

public record GetBookByIdQuery(int Id) : IRequest<BookEntity>;
