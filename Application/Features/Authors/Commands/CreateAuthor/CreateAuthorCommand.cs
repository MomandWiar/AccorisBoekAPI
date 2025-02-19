using MediatR;

namespace Application.Features.Authors.Commands;

public record CreateAuthorCommand(string Name, int BirthYear) : IRequest<int>;