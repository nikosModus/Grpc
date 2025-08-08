using Microsoft.EntityFrameworkCore;
using People.Service.Models;

public class PeopleContext : DbContext
{
    public PeopleContext(DbContextOptions<PeopleContext> options) : base(options)
    {
    }
    public DbSet<PersonEntity> People { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonEntity>(entity =>
        {
            entity.ToTable("Person"); // Table name matches your DB
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Surname).IsRequired();
            entity.Property(e => e.Age).IsRequired();
            entity.Property(e => e.DateOfBirth).IsRequired();
        });
    }
}
