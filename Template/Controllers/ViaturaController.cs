using Microsoft.AspNetCore.Mvc;
using Exemplo.Models;
using Exemplo.Services;
using Exemplo.DTO; // Adicionado para reconhecer o AlocarGuarnicaoDTO
using System.Collections.Generic;

namespace Exemplo.Controllers
{
    [Route("api/[controller]")] // A rota será: http://localhost:PORTA/api/Viatura
    [ApiController]
    public class ViaturaController : ControllerBase
    {
        private IServicoViatura _servicoViatura;

        public ViaturaController()
        {
            _servicoViatura = new ServicoViatura();
        }

        [HttpPost] // RF08 - Cadastro de Frota
        public IActionResult Cadastrar([FromBody] Viatura viatura)
        {
            var criada = _servicoViatura.CriarViatura(viatura);
            return Ok(criada);
        }

        [HttpGet] // Para visualizar a frota completa
        public IActionResult Listar()
        {
            return Ok(_servicoViatura.ListarTodas());
        }

        [HttpPatch("{id}/status")] // RF09 - Controle de Status Operacional
        public IActionResult AtualizarStatus(int id, [FromBody] StatusViatura novoStatus)
        {
            var atualizada = _servicoViatura.AtualizarStatus(id, novoStatus);
            if (atualizada == null) return NotFound("Viatura não encontrada.");
            return Ok(atualizada);
        }

        [HttpPost("{id}/iniciar-turno")] // RF10 - Gestão de Guarnição (Tripulação) por Turno
        public IActionResult IniciarTurno(int id, [FromBody] AlocarGuarnicaoDTO dto)
        {
            var resultado = _servicoViatura.AlocarNovoTurno(id, dto.BombeirosIds);

            if (resultado == "Viatura não encontrada.")
                return NotFound(resultado);

            return Ok(new { Mensagem = resultado });
        }
    }
}