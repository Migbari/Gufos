using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Para adicionar a árvore de objetos adicionamos uma nova biblioteca JSON
// dotnet add package Microsoft.AspNetCore.MVC.NewtonsoftJson

namespace backend.Controllers {
    // Definimos nossa rota do controller e dizemos que é um controlller de API
    [Route ("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase 
    {
        GufosContext _contexto = new GufosContext ();
        // GET: api/Evento
        // async executa os métodos sem precisar que um outro tenha finalizado
        // Task = tarefa, actionResult = resultado da ação
        /// <summary>
        /// Pegamos todos os eventos cadastrados
        /// </summary>
        /// <returns>Lista de eventos</returns>
        [HttpGet]
        public async Task<ActionResult<List<Evento>>> Get () // list chama toda a tabela
        {   
            // Include("") = Adiciona efetivamente a árvore de objetos relacionados
            var eventos = await _contexto.Evento.Include ("Categoria").Include("Localizacao").ToListAsync();

            if (eventos == null) {
                return NotFound ();
            }
            return eventos;
        }

        // GET: api/Evento/2
        [HttpGet ("{id}")]
        public async Task<ActionResult<Evento>> Get (int id) {
            // FindAsync = procura algo específico no banco
            // await 
            var evento = await _contexto.Evento.Include("Categoria").Include("Localizacao").FirstOrDefaultAsync(e => e.EventoId == id);

            if (evento == null) {
                return NotFound ();
            }
            return evento;

        }

        // POST api/Evento
        [HttpPost]
        public async Task<ActionResult<Evento>> POST (Evento evento) {

            try {
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync (evento);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                throw; // Mostra erro automaticamente // Mostra a Exception
            }

            return evento;

        }

        [HttpPut ("{id}")]
        public async Task<ActionResult> Put (int id, Evento evento) {
            // Se o Id do objeto não existir ele retorna badrequest 400
            if (id != evento.EventoId) {
                return BadRequest (); // Badrequest usuario errou
            }
            // Comparamos os atributos que foram modificados através do EF
            _contexto.Entry (evento).State = EntityState.Modified;

            try {
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {

                // Verificamos se o objeto inserido realmente existe no banco
                var evento_valido = await _contexto.Evento.FindAsync (id);

                if (evento_valido == null) {
                    return NotFound ();
                } else {
                    throw;
                }
            }
            // NoContent = Retorna 204 // 204 no content - sem conteudo
            return NoContent ();
        }

        // DELETE api/evento/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Evento>> Delete(int id){

            var evento = await _contexto.Evento.FindAsync(id);
            if (evento == null){
                return NotFound(); // notfound - não existe
            }

            _contexto.Evento.Remove(evento);
            await _contexto.SaveChangesAsync();

            return evento;
        }
        
    }
}