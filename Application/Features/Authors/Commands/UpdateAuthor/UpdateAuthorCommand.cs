using Domain.Entities;
using MediatR;

namespace Application.Features.Authors.Commands;

public record UpdateAuthorCommand(int Id, AuthorEntity Author) : IRequest<Unit>;