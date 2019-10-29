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
    public class CategoriaController : ControllerBase {
        // GufosContext _repositorio = new GufosContext ();
        CategoriaRepository _repositorio = new CategoriaRepository();
        
        // GET: api/Categoria
        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> Get () // list chama toda a tabela
        {
            var categorias = await _repositorio.Listar();

            if (categorias == null) {
                return NotFound ();
            }
            return categorias;
        }
        // GET: api/Categoria/2
        [HttpGet ("{id}")]
        public async Task<ActionResult<Categoria>> Get (int id) {
            // FindAsync = procura algo específico no banco
            var categoria = await _repositorio.BuscarPorId(id);

            if (categoria == null) {
                return NotFound();
            }
            return categoria;
        }
        // POST api/Categoria
        [HttpPost]
        public async Task<ActionResult<Categoria>> POST (Categoria categoria) {
            try {
                // Tratamos contra ataques de SQL Injection
                await _repositorio.Salvar (categoria);
               return categoria;

            } catch (DbUpdateConcurrencyException) {
                return BadRequest();
            }
        }
        [HttpPut ("{id}")]
        public async Task<ActionResult> Put (int id, Categoria categoria) {
            // Se o Id do objeto não existir ele retorna badrequest 400
            if (id != categoria.CategoriaId) {
                return BadRequest (); // Badrequest usuario errou
            }
            try {
                await _repositorio.Alterar (categoria);
            } catch (DbUpdateConcurrencyException) {

                // Verificamos se o objeto inserido realmente existe no banco
                var categoria_valido = await _repositorio.BuscarPorId(id);

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
        [HttpDelete ("{id}")]
        public async Task<ActionResult<Categoria>> Delete (int id) {

            var categoria = await _repositorio.BuscarPorId(id);
            if (categoria == null) {
                return NotFound (); // notfound - não existe
            }
            categoria = await _repositorio.Excluir(categoria);

            return categoria;
        }
    }
}