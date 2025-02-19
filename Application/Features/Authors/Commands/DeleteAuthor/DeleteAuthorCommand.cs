using MediatR;

namespace Application.Features.Authors.Commands;

public record DeleteAuthorCommand(int Id) : IRequest<Unit>;