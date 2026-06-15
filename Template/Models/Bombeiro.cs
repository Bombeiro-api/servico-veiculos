using System.Text.Json.Serialization; // <-- NECESSÁRIO PARA O JSON IGNORE

namespace Exemplo.Models
{
    public class Bombeiro
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Funcao { get; set; } = string.Empty; // Ex: "Motorista", "Comandante"
        public string Matricula { get; set; } = string.Empty;

        // --- VÍNCULO COM A CORPORAÇÃO (MÃE) ---
        public int CorporacaoId { get; set; }

        [JsonIgnore] // Impede loop infinito ao listar no Swagger
        public Corporacao? Corporacao { get; set; }
    }
}