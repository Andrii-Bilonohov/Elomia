using Application.Abstractions.Services.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ElomiaContext : DbContext, IUnitOfWork
{
    public ElomiaContext(DbContextOptions<ElomiaContext> options) :  base(options) {}
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ElomiaContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}