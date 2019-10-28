using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;

namespace backend.Interfaces {
    public interface ICategoria { 
        Task<List<Categoria>> Listar (); 
        Task<List<Categoria>> BuscarPorId (int id); 
        Task<List<Categoria>> Salvar (Categoria categoria); 
        Task<List<Categoria>> Alterar (Categoria categoria); 
        Task<List<Categoria>> Excluir (Categoria categoria);
    }
}