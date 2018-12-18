using System;
using IntroToAPIs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IntroToAPIs
{
  public partial class SafariVacationContext : DbContext
  {
    public SafariVacationContext()
    {
    }

    public SafariVacationContext(DbContextOptions<SafariVacationContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {

        optionsBuilder.UseNpgsql("server=localhost;database=SafariVacation");
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");
    }
    public DbSet<SeenAnimals> SeenAnimals { get; set; }
  }
}
