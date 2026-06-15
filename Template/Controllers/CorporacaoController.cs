using Microsoft.AspNetCore.Mvc;
using Exemplo.Models;
using Exemplo.Services;

namespace Exemplo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorporacaoController : ControllerBase
    {
        private IServicoCorporacao _servicoCorporacao;

        public CorporacaoController()
        {
            _servicoCorporacao = new ServicoCorporacao();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] Corporacao corporacao)
        {
            var criada = _servicoCorporacao.Cadastrar(corporacao);
            return Ok(criada);
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_servicoCorporacao.ListarTodas());
        }
    }
}