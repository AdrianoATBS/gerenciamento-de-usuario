using GerenciamentoDeUsuario.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeUsuario.Infrastructure.Persistencie;

public class AppDbContext : DbContext
{
   public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }

}
