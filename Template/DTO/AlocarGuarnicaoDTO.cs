using System.Collections.Generic;

namespace Exemplo.DTO
{
    public class AlocarGuarnicaoDTO
    {
        // O comandante envia os IDs dos bombeiros que vão assumir o turno
        public List<int> BombeirosIds { get; set; } = new List<int>();
    }
}