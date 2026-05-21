using Microsoft.EntityFrameworkCore;
using RequestService.Domain.Models;

namespace RequestService.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<DocumentRequest> Requests => Set<DocumentRequest>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<DocumentRequest>()
                .HasIndex(dr => new { dr.EmployeeName, dr.DocumentType, dr.Status });
        }
    }
}
