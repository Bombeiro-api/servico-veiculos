using System.Collections.Generic;

namespace Exemplo.Models
{
    public class Corporacao
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool Ativo { get; set; } = true;

        // NOVO: As listas que guardam o que tem dentro da corporação
        public List<Viatura> Viaturas { get; set; } = new List<Viatura>();
        public List<Bombeiro> Bombeiros { get; set; } = new List<Bombeiro>();
    }
}