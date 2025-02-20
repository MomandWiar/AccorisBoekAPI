using System.Text.Json.Serialization;

namespace Domain.Entities;

public class AuthorEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    public ICollection<BookEntity> Books { get; set; } = new List<BookEntity>();
}