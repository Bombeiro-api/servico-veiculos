namespace Exemplo.Models
{
    public class Bombeiro
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Funcao { get; set; } = string.Empty; // Ex: "Motorista", "Comandante"
        public string Matricula { get; set; } = string.Empty;
    }
}