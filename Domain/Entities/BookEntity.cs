using System.Text.Json.Serialization;
using Common.Extensions;

namespace Domain.Entities;

public class BookEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public DateTime PublicationDate { get; set; }
    public int PublicationWeekNr => PublicationDate.GetWeekNumber();

    [JsonIgnore]
    public AuthorEntity Author { get; set; } = null!;
}