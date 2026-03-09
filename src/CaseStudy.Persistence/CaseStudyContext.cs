namespace CaseStudy.Persistence;

using CaseStudy.Domain.Models;
using Microsoft.EntityFrameworkCore;

public class CaseStudyContext : DbContext
{
    private readonly string connectionString;

    public CaseStudyContext(string connectionString = "DataSource=../data/localdb.db")
    {
        this.connectionString = connectionString;
        this.Database.Migrate();
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(connectionString);
    }
}
