using System.Text.Json.Serialization;

namespace Domain.Entities;

public class BookEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int AuthorId { get; set; }
    [JsonIgnore]
    public AuthorEntity Author { get; set; } = null!;
}