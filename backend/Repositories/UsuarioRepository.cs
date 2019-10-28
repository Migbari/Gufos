using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories {
    public class UsuarioRepository : IUsuario {
        public async Task<Usuario> Alterar (Usuario usuario) {
            using (GufosContext _context = new GufosContext ()) {
                _context.Entry (usuario).State = EntityState.Modified;
                await _context.SaveChangesAsync ();
            }
            return usuario;
        }
        public async Task<Usuario> BuscarPorId (int id) {
            using (GufosContext _context = new GufosContext ()) {
                return await _context.Usuario.FindAsync (id);
            }
        }
        public async Task<Usuario> Excluir (Usuario usuario) {
            using (GufosContext _context = new GufosContext ()) {
                _context.Usuario.Remove (usuario);
                await _context.SaveChangesAsync ();
                return usuario;
            }
        }
        public async Task<List<Usuario>> Listar () {
            using (GufosContext _context = new GufosContext ()) {
                return await _context.Usuario.ToListAsync ();
            }
        }
        public async Task<Usuario> Salvar (Usuario usuario) {
            using (GufosContext _context = new GufosContext ()) {
                await _context.AddAsync (usuario);
                await _context.SaveChangesAsync ();
                return usuario;
            }
        }
    }
}