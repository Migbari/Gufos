using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {
    // Definimos nossa rota do controller e dizemos que é um controlller de API
    [Route ("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase {
        GufosContext _contexto = new GufosContext ();

        // GET: api/TipoUsuario
        [HttpGet]

        // async executa os métodos sem precisar que um outro tenha finalizado
        // Task = tarefa, actionResult = resultado da ação
        public async Task<ActionResult<List<TipoUsuario>>> Get () // list chama toda a tabela
        {
            var tipousuarios = await _contexto.TipoUsuario.ToListAsync ();

            if (tipousuarios == null) {
                return NotFound ();
            }
            return tipousuarios;
        }

        // GET: api/TipoUsuario/2
        [HttpGet ("{id}")]
        public async Task<ActionResult<TipoUsuario>> Get (int id) {
            // FindAsync = procura algo específico no banco
            // await 
            var tipousuario = await _contexto.TipoUsuario.FindAsync (id);

            if (tipousuario == null) {
                return NotFound ();
            }
            return tipousuario;

        }

        // POST api/TipoUsuario
        [HttpPost]
        public async Task<ActionResult<TipoUsuario>> POST (TipoUsuario tipousuario) {

            try {
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync (tipousuario);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                throw; // Mostra erro automaticamente // Mostra a Exception
            }

            return tipousuario;

        }

        [HttpPut ("{id}")]
        public async Task<ActionResult> Put (int id, TipoUsuario tipousuario) {
            // Se o Id do objeto não existir ele retorna badrequest 400
            if (id != tipousuario.TipoUsuarioId) {
                return BadRequest (); // Badrequest usuario errou
            }
            // Comparamos os atributos que foram modificados através do EF
            _contexto.Entry (tipousuario).State = EntityState.Modified;

            try {
            await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {

                // Verificamos se o objeto inserido realmente existe no banco
                var tipousuario_valido = await _contexto.TipoUsuario.FindAsync (id);

                if (tipousuario_valido == null) {
                    return NotFound ();
                } else {
                    throw;
                }
            }
            // NoContent = Retorna 204 // 204 no content - sem conteudo
            return NoContent ();
        }

        // DELETE api/tipousuario/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoUsuario>> Delete(int id){

            var tipousuario = await _contexto.TipoUsuario.FindAsync(id);
            if (tipousuario == null){
                return NotFound(); // notfound - não existe
            }

            _contexto.TipoUsuario.Remove(tipousuario);
            await _contexto.SaveChangesAsync();

            return tipousuario;
        }
        
    }
}