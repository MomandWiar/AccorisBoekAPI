using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<AuthorEntity> Authors { get; set; }
}