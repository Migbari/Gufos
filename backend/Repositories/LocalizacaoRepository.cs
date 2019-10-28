using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories {
    public class LocalizacaoRepository : ILocalizacao {
        public async Task<Localizacao> Alterar (Localizacao localizacao) {

            using (GufosContext _context = new GufosContext ()) {
                _context.Entry (localizacao).State = EntityState.Modified;
                await _context.SaveChangesAsync ();
            }
            return localizacao;
        }
        public async Task<Localizacao> BuscarPorId (int id) {
            using (GufosContext _context = new GufosContext ()) {
                return await _context.Localizacao.FindAsync (id);
            }
        }
        public async Task<Localizacao> Excluir (Localizacao localizacao) {
            using (GufosContext _context = new GufosContext ()) {
                _context.Localizacao.Remove (localizacao);
                await _context.SaveChangesAsync ();
                return localizacao;
            }
        }

        public Task<Presenca> Excluir(Presenca presenca)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Localizacao>> Listar () {
            using (GufosContext _context = new GufosContext ()) {
                return await _context.Localizacao.ToListAsync ();
            }
        }
        public async Task<Localizacao> Salvar (Localizacao localizacao) {
            using (GufosContext _context = new GufosContext ()) {
                await _context.AddAsync (localizacao);
                await _context.SaveChangesAsync ();
                return localizacao;
            }
        }
    }
}