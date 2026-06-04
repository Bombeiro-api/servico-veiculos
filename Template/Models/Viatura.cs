namespace Exemplo.Models
{
    public class Viatura
    {
        public int Id { get; set; }

        // RF07 - Cadastro de Frota
        public string Tipo { get; set; } = string.Empty; // Ex: "ABTR", "ASU"
        public string Placa { get; set; } = string.Empty;
        public string IdentificadorRadio { get; set; } = string.Empty;
        public string EspecificacoesTecnicas { get; set; } = string.Empty;

        // RF08 - Controle de Status Operacional
        public StatusViatura Status { get; set; } = StatusViatura.DisponivelNaBase;

        // RF10 - Controle de Suprimentos Básicos
        public double NivelTanqueAgua { get; set; } // Em porcentagem ou litros
        public int CilindrosOxigenioCheios { get; set; }
        public double NivelCombustivel { get; set; } // Em porcentagem

        // RF09 - Gestão de Guarnição
        public List<Bombeiro> Guarnicao { get; set; } = new List<Bombeiro>();
    }
}