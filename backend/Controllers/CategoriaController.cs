using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {
    // Definimos nossa rota do controller e dizemos que é um controlller de API
    [Route ("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase {
        GufosContext _contexto = new GufosContext ();

        // GET: api/Categoria
        [HttpGet]

        // async executa os métodos sem precisar que um outro tenha finalizado
        // Task = tarefa, actionResult = resultado da ação
        public async Task<ActionResult<List<Categoria>>> Get () // list chama toda a tabela
        {
            var categorias = await _contexto.Categoria.ToListAsync ();

            if (categorias == null) {
                return NotFound ();
            }
            return categorias;
        }

        // GET: api/Categoria/2
        [HttpGet ("{id}")]
        public async Task<ActionResult<Categoria>> Get (int id) {
            // FindAsync = procura algo específico no banco
            // await 
            var categoria = await _contexto.Categoria.FindAsync (id);

            if (categoria == null) {
                return NotFound ();
            }
            return categoria;

        }

        // POST api/Categoria
        [HttpPost]
        public async Task<ActionResult<Categoria>> POST (Categoria categoria) {

            try {
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync (categoria);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                throw; // Mostra erro automaticamente // Mostra a Exception
            }

            return categoria;

        }
        
        [HttpPut ("{id}")]
        public async Task<ActionResult> Put (int id, Categoria categoria) {
            // Se o Id do objeto não existir ele retorna badrequest 400
            if (id != categoria.CategoriaId) {
                return BadRequest (); // Badrequest usuario errou
            }
            // Comparamos os atributos que foram modificados através do EF
            _contexto.Entry (categoria).State = EntityState.Modified;

            try {
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {

                // Verificamos se o objeto inserido realmente existe no banco
                var categoria_valido = await _contexto.Categoria.FindAsync (id);

                if (categoria_valido == null) {
                    return NotFound ();
                } else {
                    throw;
                }
            }
            // NoContent = Retorna 204 // 204 no content - sem conteudo
            return NoContent ();
        }

        // DELETE api/categoria/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> Delete(int id){

            var categoria = await _contexto.Categoria.FindAsync(id);
            if (categoria == null){
                return NotFound(); // notfound - não existe
            }

            _contexto.Categoria.Remove(categoria);
            await _contexto.SaveChangesAsync();

            return categoria;
        }
        
    }
}