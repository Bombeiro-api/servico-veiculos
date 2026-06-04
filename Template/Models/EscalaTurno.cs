using System;

namespace Exemplo.Models
{
    public class EscalaTurno
    {
        public int Id { get; set; }

        // Relacionamentos: Quem está em qual viatura?
        public int ViaturaId { get; set; }
        public int BombeiroId { get; set; }

        public Viatura? Viatura { get; set; }
        public Bombeiro? Bombeiro { get; set; }

        // Controle de Tempo do Turno
        public DateTime DataInicio { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; } // Fica nulo até o turno acabar
    }
}