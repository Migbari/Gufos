using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories {
    public class CategoriaRepository : ICategoria {
        public async Task<Categoria> Alterar (Categoria categoria) {
            using (GufosContext _context = new GufosContext ()) {
                _context.Entry (categoria).State = EntityState.Modified;
                await _context.SaveChangesAsync ();
            }
            return categoria;
        }
        public async Task<Categoria> BuscarPorId (int id) {
            using (GufosContext _context = new GufosContext ()) {
                return await _context.Categoria.FindAsync (id);
            }
        }
         public async Task<Categoria> BuscarPorNome (string nome) {
            using (GufosContext _context = new GufosContext ()) {
                return await _context.Categoria.FindAsync (nome);
            }
        }
        public async Task<Categoria> Excluir (Categoria categoria) {
            using (GufosContext _context = new GufosContext ()) {
                _context.Categoria.Remove (categoria);
                await _context.SaveChangesAsync ();
                return categoria;
            }
        }
        public async Task<List<Categoria>> Listar () {
            using (GufosContext _context = new GufosContext ()) {
                return await _context.Categoria.ToListAsync ();
            }
        }
        public async Task<Categoria> Salvar (Categoria categoria) {
            using (GufosContext _context = new GufosContext ()) {
                await _context.AddAsync (categoria);
                await _context.SaveChangesAsync ();
                return categoria;
            }
        }

        Task<Evento> ICategoria.BuscarPorId(int id)
        {
            throw new System.NotImplementedException();
        }

        Task<Evento> ICategoria.BuscarPorNome(string nome)
        {
            throw new System.NotImplementedException();
        }
    }
}