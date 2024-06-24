using APIRest.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace APIRest.Persistencia.Context
{
    public class AplicationDbContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<LogMonitoreo> logMonitoreo { get; set; }
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
    }
}