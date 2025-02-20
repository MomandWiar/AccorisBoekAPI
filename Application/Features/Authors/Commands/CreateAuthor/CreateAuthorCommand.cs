using MediatR;

namespace Application.Features.Authors.Commands;

public record CreateAuthorCommand(string Name) : IRequest<int>;