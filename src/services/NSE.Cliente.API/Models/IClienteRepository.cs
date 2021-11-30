using System.Collections.Generic;
using System.Threading.Tasks;
using NSE.Core.Data;

namespace NSE.Cliente.API.Models
{
    public interface IClienteRepository : IRepository<Clientess>
    {
        void Adicionar(Clientess cliente);

        Task<IEnumerable<Clientess>> ObterTodos();
        Task<Clientess> ObterPorCpf(string cpf);
    }
}