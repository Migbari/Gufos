using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuario {
        public async Task<TipoUsuario> Alterar (TipoUsuario tipousuario) {
            using (GufosContext _context = new GufosContext ()) {
                _context.Entry (tipousuario).State = EntityState.Modified;
                await _context.SaveChangesAsync ();
            }
            return tipousuario;
        }
        public async Task<TipoUsuario> BuscarPorId (int id) {
            using (GufosContext _context = new GufosContext ()) {
                return await _context.TipoUsuario.FindAsync (id);
            }
        }
        public async Task<TipoUsuario> Excluir (TipoUsuario tipousuario) {
            using (GufosContext _context = new GufosContext ()) {
                _context.TipoUsuario.Remove (tipousuario);
                await _context.SaveChangesAsync ();
                return tipousuario;
            }
        }
        public async Task<List<TipoUsuario>> Listar () {
            using (GufosContext _context = new GufosContext ()) {
                return await _context.TipoUsuario.ToListAsync ();
            }
        }
        public async Task<TipoUsuario> Salvar (TipoUsuario tipousuario) {
            using (GufosContext _context = new GufosContext ()) {
                await _context.AddAsync (tipousuario);
                await _context.SaveChangesAsync ();
                return tipousuario;
            }
        }
    }
}