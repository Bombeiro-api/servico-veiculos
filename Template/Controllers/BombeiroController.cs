using Microsoft.AspNetCore.Mvc;
using Exemplo.Models;
using Exemplo.Services;

namespace Exemplo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BombeiroController : ControllerBase
    {
        private IServicoBombeiro _servicoBombeiro;

        public BombeiroController()
        {
            _servicoBombeiro = new ServicoBombeiro();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] Bombeiro bombeiro)
        {
            var criado = _servicoBombeiro.Cadastrar(bombeiro);
            return Ok(criado);
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_servicoBombeiro.ListarTodos());
        }
    }
}