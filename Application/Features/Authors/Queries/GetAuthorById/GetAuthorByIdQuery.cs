using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Features.Authors.Queries;

public record GetAuthorByIdQuery(int Id) : IRequest<AuthorDto>;