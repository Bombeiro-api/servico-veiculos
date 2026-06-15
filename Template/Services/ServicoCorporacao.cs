using Exemplo.Models;
using Microsoft.EntityFrameworkCore; // <-- NECESSÁRIO PARA O INCLUDE FUNCIONAR
using System.Collections.Generic;
using System.Linq;
using Template.Infra;

namespace Exemplo.Services
{
    public interface IServicoCorporacao
    {
        Corporacao Cadastrar(Corporacao corporacao);
        List<Corporacao> ListarTodas();
    }

    public class ServicoCorporacao : IServicoCorporacao
    {
        private DataContext _dataContext;

        public ServicoCorporacao()
        {
            _dataContext = GeradorDeServicos.CarregarContexto();
        }

        public Corporacao Cadastrar(Corporacao corporacao)
        {
            _dataContext.Corporacoes.Add(corporacao);
            _dataContext.SaveChanges();
            return corporacao;
        }

        public List<Corporacao> ListarTodas()
        {
            // O Include garante que a consulta no banco traga os filhos junto com a mãe
            return _dataContext.Corporacoes
                .Include(c => c.Viaturas)
                .Include(c => c.Bombeiros)
                .ToList();
        }
    }
}