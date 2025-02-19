using Microsoft.EntityFrameworkCore;
using NlwApi.Domain;

namespace NlwApi.Infraesructure;

public class NlwDbContext : DbContext
{
  public DbSet<User> Users { get; set; }
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite("Data Source=./Db/nlw.db");
  }
}