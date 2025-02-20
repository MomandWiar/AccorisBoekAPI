﻿using Infrastructure.Repositories.Author;
using Infrastructure.Repositories.Book;
using Domain.Entities;
using MediatR;

namespace Application.Features.Books.Commands;

public sealed class CreateBookCommandHandler(IBookRepository _bookRepository, IAuthorRepository _authorRepository) : IRequestHandler<CreateBookCommand, int>
{
    public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetById(request.Book.AuthorId);
            
        if (author == null)
        {
            throw new ArgumentException("Book cannot be created. Author not found.");
        }

        var book = new BookEntity
        {
            Title = request.Book.Title,
            AuthorId = request.Book.AuthorId,
            PublicationYear = request.Book.PublicationYear
        };

        await _bookRepository.Create(book);

        return book.Id;
    }
}