using Domain.Entities;
using MediatR;

namespace Application.Features.Authors.Queries.GetAuthorById;

public record GetAuthorByIdQuery(int Id) : IRequest<AuthorEntity>;