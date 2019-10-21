using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {
    // Definimos nossa rota do controller e dizemos que é um controlller de API
    [Route ("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase {
        GufosContext _contexto = new GufosContext ();

        // GET: api/Usuario
        [HttpGet]

        // async executa os métodos sem precisar que um outro tenha finalizado
        // Task = tarefa, actionResult = resultado da ação
        public async Task<ActionResult<List<Usuario>>> Get () // list chama toda a tabela
        {
            var usuarios = await _contexto.Usuario.ToListAsync ();

            if (usuarios == null) {
                return NotFound ();
            }
            return usuarios;
        }

        // GET: api/Usuario/2
        [HttpGet ("{id}")]
        public async Task<ActionResult<Usuario>> Get (int id) {
            // FindAsync = procura algo específico no banco
            // await 
            var usuario = await _contexto.Usuario.FindAsync (id);

            if (usuario == null) {
                return NotFound ();
            }
            return usuario;

        }

        // POST api/Usuario
        [HttpPost]
        public async Task<ActionResult<Usuario>> POST (Usuario usuario) {

            try {
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync (usuario);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                throw; // Mostra erro automaticamente // Mostra a Exception
            }

            return usuario;

        }

        [HttpPut ("{id}")]
        public async Task<ActionResult> Put (int id, Usuario usuario) {
            // Se o Id do objeto não existir ele retorna badrequest 400
            if (id != usuario.UsuarioId) {
                return BadRequest (); // Badrequest usuario errou
            }
            // Comparamos os atributos que foram modificados através do EF
            _contexto.Entry (usuario).State = EntityState.Modified;

            try {
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {

                // Verificamos se o objeto inserido realmente existe no banco
                var usuario_valido = await _contexto.Usuario.FindAsync (id);

                if (usuario_valido == null) {
                    return NotFound ();
                } else {
                    throw;
                }
            }
            // NoContent = Retorna 204 // 204 no content - sem conteudo
            return NoContent ();
        }

        // DELETE api/usuario/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> Delete(int id){

            var usuario = await _contexto.Usuario.FindAsync(id);
            if (usuario == null){
                return NotFound(); // notfound - não existe
            }

            _contexto.Usuario.Remove(usuario);
            await _contexto.SaveChangesAsync();

            return usuario;
        }
        
    }
}