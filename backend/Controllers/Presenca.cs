using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {
    // Definimos nossa rota do controller e dizemos que é um controlller de API
    [Route ("api/[controller]")]
    [ApiController]
    public class PresencaController : ControllerBase {
        GufosContext _contexto = new GufosContext ();

        // GET: api/Presenca
        [HttpGet]

        // async executa os métodos sem precisar que um outro tenha finalizado
        // Task = tarefa, actionResult = resultado da ação
        public async Task<ActionResult<List<Presenca>>> Get () // list chama toda a tabela
        {
            var presencas = await _contexto.Presenca.ToListAsync ();

            if (presencas == null) {
                return NotFound ();
            }
            return presencas;
        }

        // GET: api/Presenca/2
        [HttpGet ("{id}")]
        public async Task<ActionResult<Presenca>> Get (int id) {
            // FindAsync = procura algo específico no banco
            // await 
            var presenca = await _contexto.Presenca.FindAsync (id);

            if (presenca == null) {
                return NotFound ();
            }
            return presenca;

        }

        // POST api/Presenca
        [HttpPost]
        public async Task<ActionResult<Presenca>> POST (Presenca presenca) {

            try {
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync (presenca);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                throw; // Mostra erro automaticamente // Mostra a Exception
            }

            return presenca;

        }

        [HttpPut ("{id}")]
        public async Task<ActionResult> Put (int id, Presenca presenca) {
            // Se o Id do objeto não existir ele retorna badrequest 400
            if (id != presenca.PresencaId) {
                return BadRequest (); // Badrequest usuario errou
            }
            // Comparamos os atributos que foram modificados através do EF
            _contexto.Entry (presenca).State = EntityState.Modified;

            try {
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {

                // Verificamos se o objeto inserido realmente existe no banco
                var presenca_valido = await _contexto.Presenca.FindAsync (id);

                if (presenca_valido == null) {
                    return NotFound ();
                } else {
                    throw;
                }
            }
            // NoContent = Retorna 204 // 204 no content - sem conteudo
            return NoContent ();
        }

        // DELETE api/presenca/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Presenca>> Delete(int id){

            var presenca = await _contexto.Presenca.FindAsync(id);
            if (presenca == null){
                return NotFound(); // notfound - não existe
            }

            _contexto.Presenca.Remove(presenca);
            await _contexto.SaveChangesAsync();

            return presenca;
        }
        
    }
}