using Exemplo.Models;
using System.Collections.Generic;
using System.Linq;
using Template.Infra;

namespace Exemplo.Services
{
    public interface IServicoBombeiro
    {
        Bombeiro Cadastrar(Bombeiro bombeiro);
        List<Bombeiro> ListarTodos();
    }

    public class ServicoBombeiro : IServicoBombeiro
    {
        private DataContext _dataContext;

        public ServicoBombeiro()
        {
            _dataContext = GeradorDeServicos.CarregarContexto();
        }

        public Bombeiro Cadastrar(Bombeiro bombeiro)
        {
            _dataContext.Bombeiros.Add(bombeiro);
            _dataContext.SaveChanges();
            return bombeiro;
        }

        public List<Bombeiro> ListarTodos()
        {
            return _dataContext.Bombeiros.ToList();
        }
    }
}