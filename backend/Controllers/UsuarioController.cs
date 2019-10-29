using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {
    // Definimos nossa rota do controller e dizemos que é um controlller de API
    [Route ("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase {
        //GufosContext _contexto = new GufosContext ();
        UsuarioRepository _repositorio = new UsuarioRepository();

        // GET: api/Usuario
        [HttpGet]
         public async Task<ActionResult<List<Usuario>>> Get () // list chama toda a tabela
        {   
            // Include("") = Adiciona efetivamente a árvore de objetos relacionados
            var usuarios = await _repositorio.Listar();

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
            var usuario = await _repositorio.BuscarPorId(id);

            if (usuario == null) {
                return NotFound ();
            }
            return usuario;
        }
        // POST api/Usuario
        // POST api/Usuario
        [HttpPost]
        public async Task<ActionResult<Usuario>> POST (Usuario usuario) {
            try {
                // Tratamos contra ataques de SQL Injection
                // await _contexto.AddAsync (usuario);
                // Salvamos efetivamente o nosso objeto no banco de dados
                // await _contexto.SaveChangesAsync ();
                await _repositorio.Salvar (usuario);
               return usuario;
            } catch (DbUpdateConcurrencyException) {
                return BadRequest();     
              //  throw; // Mostra erro automaticamente // Mostra a Exception
            }
        }
        [HttpPut ("{id}")]
        public async Task<ActionResult> Put (int id, Usuario usuario) {
            // Se o Id do objeto não existir ele retorna badrequest 400
            if (id != usuario.UsuarioId) {
                return BadRequest (); // Badrequest usuario errou
            }
            // Comparamos os atributos que foram modificados através do EF
            // _contexto.Entry (usuario).State = EntityState.Modified;
            try {
              await _repositorio.Alterar (usuario);
            } catch (DbUpdateConcurrencyException) {

                // Verificamos se o objeto inserido realmente existe no banco
                var usuario_valido = await _repositorio.BuscarPorId(id);

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

            var usuario = await _repositorio.BuscarPorId(id);
            if (usuario == null){
                return NotFound(); // notfound - não existe
            }
            usuario = await _repositorio.Excluir(usuario);
            // _contexto.Usuario.Remove(usuario);
            // await _contexto.SaveChangesAsync();
            return usuario;
        }
    }
}