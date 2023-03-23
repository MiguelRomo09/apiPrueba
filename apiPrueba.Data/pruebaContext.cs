using Microsoft.EntityFrameworkCore;

namespace apiPrueba.Data
{
    public class pruebaContext : DbContext
    {
        public pruebaContext(DbContextOptions<pruebaContext> options):base(options) { }


        public DbSet<Humano> Humanos { get; set; }
    }
}