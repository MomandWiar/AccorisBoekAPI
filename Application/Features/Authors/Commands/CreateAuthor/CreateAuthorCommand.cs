using MediatR;

namespace Application.Features.Authors.Commands.CreateAuthor;

public record CreateAuthorCommand(string Name, int BirthYear) : IRequest<int>;