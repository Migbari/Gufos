using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using backend.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories {
    public class CategoriaRepository : ICategoria {
       // Cria uma instância do contexto para chamada dos métodos do EF Core
        GufosContext _contexto = new GufosContext();
        public async Task<Categoria> Alterar(Categoria categoria)
        {
            // Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(categoria).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
            return categoria;
        }

        public async Task<Categoria> BuscarPorID(int id)
        {
            // FindAsync procura algo específico no banco
            return await _contexto.Categoria.FindAsync(id);
        }

        public Task<Categoria> BuscarPorId(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Categoria> Excluir(Categoria categoria)
        {
            _contexto.Categoria.Remove(categoria);
            await _contexto.SaveChangesAsync();
            return categoria;
        }

        public List<Categoria> FiltrarPorNome(FiltroViewModel filtro)
        {
            // Lista categorias que comecem com o filtro inserido
            // List<Categoria> categorias = _contexto.Categoria.Where(c => c.Titulo.StartsWith(filtro.Palavra)).ToList();

            // Lista categorias que contenham o filtro inserido em qualquer lugar do Titulo
            List<Categoria> categorias = _contexto.Categoria.Where(c => c.Titulo.Contains(filtro.Palavra)).ToList();

            return categorias;
        }

        public async Task<List<Categoria>> Listar()
        {
            return await _contexto.Categoria.ToListAsync();
        }

        public List<Categoria> Ordenar()
        {
            List <Categoria> categorias = _contexto.Categoria.OrderByDescending(c => c.Titulo).ToList();

            return categorias;
        }

        public async Task<Categoria> Salvar(Categoria categoria)
        {
            // Tratamento contra ataques SQL Injection
            await _contexto.AddAsync(categoria);

            // Salva o objeto no banco
            await _contexto.SaveChangesAsync();
            return categoria;
        }

    }
}