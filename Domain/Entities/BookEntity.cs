namespace Domain.Entities;

public class BookEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int PublicationYear { get; set; }
    public int AuthorId { get; set; }
    public AuthorEntity Author { get; set; }
}