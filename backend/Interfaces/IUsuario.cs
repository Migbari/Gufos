using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;

namespace backend.Interfaces
{
    public interface IUsuario
    {
        Task<List<Usuario>> Listar (); 
        Task<List<Usuario>> BuscarPorId (int id); 
        Task<List<Usuario>> Salvar (Usuario usuario); 
        Task<List<Usuario>> Alterar (Usuario usuario); 
        Task<List<Usuario>> Excluir (Usuario usuario);
    }
}