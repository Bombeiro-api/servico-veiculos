using Microsoft.EntityFrameworkCore;
using Exemplo.Models;

namespace Exemplo
{

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // Esta linha cria a tabela "Viaturas" no banco de dados
        public DbSet<Viatura> Viaturas { get; set; }
        public DbSet<Bombeiro> Bombeiros { get; set; }
        public DbSet<EscalaTurno> Escalas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Viatura>().HasKey(p => p.Id); // Define o Id como Chave Primária
            base.OnModelCreating(modelBuilder);
        }
    }
}