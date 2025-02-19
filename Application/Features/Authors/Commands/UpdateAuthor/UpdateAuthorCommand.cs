using Domain.Entities;
using MediatR;

namespace Application.Features.Authors.Commands.UpdateAuthor;

public record UpdateAuthorCommand(int Id, AuthorEntity Author) : IRequest<Unit>;