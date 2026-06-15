using Exemplo.Models;
using Microsoft.EntityFrameworkCore;

namespace Template.Infra
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Viatura> Viaturas { get; set; }
        public DbSet<Bombeiro> Bombeiros { get; set; }
        public DbSet<EscalaTurno> Escalas { get; set; }
        public DbSet<Corporacao> Corporacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. Popula a Corporação Mãe
            modelBuilder.Entity<Corporacao>().HasData(
                new Corporacao
                {
                    Id = 1,
                    Nome = "1º Batalhão de Bombeiros Militar",
                    Endereco = "Centro, Içara - SC",
                    Latitude = -28.7283,
                    Longitude = -49.3015,
                    Ativo = true
                }
            );

            // 2. Popula os Bombeiros (Múltiplos integrantes para formar diferentes equipes)
            modelBuilder.Entity<Bombeiro>().HasData(
                new Bombeiro { Id = 1, Nome = "Sargento João", Funcao = "Motorista", Matricula = "BM-111", CorporacaoId = 1 },
                new Bombeiro { Id = 2, Nome = "Tenente Maria", Funcao = "Comandante", Matricula = "BM-222", CorporacaoId = 1 },
                new Bombeiro { Id = 3, Nome = "Cabo Silva", Funcao = "Socorrista", Matricula = "BM-333", CorporacaoId = 1 },
                new Bombeiro { Id = 4, Nome = "Soldado Santos", Funcao = "Socorrista", Matricula = "BM-444", CorporacaoId = 1 },
                new Bombeiro { Id = 5, Nome = "Sargento Carlos", Funcao = "Motorista", Matricula = "BM-555", CorporacaoId = 1 },
                new Bombeiro { Id = 6, Nome = "Cabo Oliveira", Funcao = "Resgatista", Matricula = "BM-666", CorporacaoId = 1 }
            );

            // 3. Popula as Viaturas (ABTR para incêndio e ASU para atendimento médico)
            modelBuilder.Entity<Viatura>().HasData(
                new Viatura
                {
                    Id = 1,
                    Tipo = "ABTR",
                    Placa = "MKI-9876",
                    IdentificadorRadio = "Alfa-1",
                    EspecificacoesTecnicas = "Caminhão de combate a incêndio e salvamento",
                    Status = StatusViatura.DisponivelNaBase,
                    NivelTanqueAgua = 100,
                    CilindrosOxigenioCheios = 4,
                    NivelCombustivel = 100,
                    CorporacaoId = 1
                },
                new Viatura
                {
                    Id = 2,
                    Tipo = "ASU",
                    Placa = "OKI-1234",
                    IdentificadorRadio = "Bravo-1",
                    EspecificacoesTecnicas = "Ambulância de resgate e atendimento pré-hospitalar",
                    Status = StatusViatura.DisponivelNaBase,
                    NivelTanqueAgua = 0,
                    CilindrosOxigenioCheios = 2,
                    NivelCombustivel = 100,
                    CorporacaoId = 1
                }
            );
        }
    }
}