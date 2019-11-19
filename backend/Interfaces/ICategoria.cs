using System.Collections.Generic;
using System.Threading.Tasks; 
using backend.Domains;

namespace backend.Interfaces {
    public interface ICategoria { 
        Task<List<Categoria>> Listar (); 
        Task<Evento> BuscarPorId (int id);
        Task<Evento> BuscarPorNome (string nome);
        Task<Categoria> Salvar (Categoria categoria); 
        Task<Categoria> Alterar (Categoria categoria); 
        Task<Categoria> Excluir (Categoria categoria);
    }
}