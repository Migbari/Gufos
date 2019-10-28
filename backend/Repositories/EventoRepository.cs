using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories {
    public class EventoRepository : IEvento {
        public async Task<Evento> Alterar (Evento evento) {

            using (GufosContext _context = new GufosContext ()) {
                _context.Entry (evento).State = EntityState.Modified;
                await _context.SaveChangesAsync ();
            }
            return evento;
        }
        public async Task<Evento> BuscarPorId (int id) {
            using (GufosContext _context = new GufosContext ()) {
                return await _context.Evento.FindAsync (id);
            }
        }
        public async Task<Evento> Excluir (Evento evento) {
            using (GufosContext _context = new GufosContext ()) {
                _context.Evento.Remove (evento);
                await _context.SaveChangesAsync ();
                return evento;
            }
        }
        public async Task<List<Evento>> Listar () {
            using (GufosContext _context = new GufosContext ()) {
                return await _context.Evento.ToListAsync ();
            }
        }
        public async Task<Evento> Salvar (Evento evento) {
            using (GufosContext _context = new GufosContext ()) {
                await _context.AddAsync (evento);
                await _context.SaveChangesAsync ();
                return evento;
            }
        }
    }
}