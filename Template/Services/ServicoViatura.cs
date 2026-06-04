using Exemplo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Template.Infra;

namespace Exemplo.Services
{
    // A Interface define QUAIS ações o serviço faz
    public interface IServicoViatura
    {
        Viatura CriarViatura(Viatura novaViatura);
        List<Viatura> ListarTodas();
        Viatura AtualizarStatus(int id, StatusViatura novoStatus); // Para o RF08
        string AlocarNovoTurno(int idViatura, List<int> idsBombeiros); // NOVO: Para o RF10
    }

    // A Classe implementa COMO as ações são feitas
    public class ServicoViatura : IServicoViatura
    {
        private DataContext _dataContext;

        public ServicoViatura()
        {
            _dataContext = GeradorDeServicos.CarregarContexto();
        }

        public Viatura CriarViatura(Viatura novaViatura)
        {
            _dataContext.Viaturas.Add(novaViatura);
            _dataContext.SaveChanges(); // Equivalente a um .save() no banco
            return novaViatura;
        }

        public List<Viatura> ListarTodas()
        {
            return _dataContext.Viaturas.ToList();
        }

        public Viatura AtualizarStatus(int id, StatusViatura novoStatus)
        {
            var viatura = _dataContext.Viaturas.FirstOrDefault(v => v.Id == id);
            if (viatura != null)
            {
                viatura.Status = novoStatus;
                _dataContext.SaveChanges();
            }
            return viatura;
        }

        // NOVO: Lógica de gerenciamento de guarnição por turnos
        public string AlocarNovoTurno(int idViatura, List<int> idsBombeiros)
        {
            var viatura = _dataContext.Viaturas.FirstOrDefault(v => v.Id == idViatura);
            if (viatura == null) return "Viatura não encontrada.";

            // 1. Encerra o turno de quem estava na viatura antes
            var escalasAbertas = _dataContext.Escalas
                .Where(e => e.ViaturaId == idViatura && e.DataFim == null)
                .ToList();

            foreach (var escalaAntiga in escalasAbertas)
            {
                escalaAntiga.DataFim = DateTime.Now;
            }

            // 2. Cria uma nova escala para cada bombeiro da lista nova
            foreach (var idBombeiro in idsBombeiros)
            {
                var novaEscala = new EscalaTurno
                {
                    ViaturaId = idViatura,
                    BombeiroId = idBombeiro,
                    DataInicio = DateTime.Now
                    // DataFim começa nula automaticamente
                };
                _dataContext.Escalas.Add(novaEscala);
            }

            // 3. Muda o status da viatura para indicar que ela está pronta com a guarnição
            viatura.Status = StatusViatura.DisponivelNaBase;

            _dataContext.SaveChanges();
            return "Turno iniciado e guarnição alocada com sucesso!";
        }
    }
}