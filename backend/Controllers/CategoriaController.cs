using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Repositories;
using backend.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {
    // Definimos nossa rota do controller e dizemos que é um controlller de API
    [Route ("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase {
        //GufosContext _repositorio = new GufosContext ();
        // Cria uma instância para chamada de métodos do repositório
        CategoriaRepository _repositorio = new CategoriaRepository();

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> Get(){
            List<Categoria> categorias = await _repositorio.Listar();

            if (categorias == null)
            {
                return NotFound();
            }

            return categorias;
        }

        // GET: api/Categorias/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> Get(int id){
            Categoria categoria = await _repositorio.BuscarPorID(id);

            // Caso a categoria seja nula, retorna uma mensagem personalizada
            if (categoria == null)
            {
                return NotFound(new {mensagem = "Nenhuma categoria encontrada para o ID informado"});
            }
    
            return categoria;
        }

        // POST: api/Categorias
        [HttpPost]
        public async Task<ActionResult<Categoria>> Post(Categoria categoria){
            try
            {
                await _repositorio.Salvar(categoria);
            }
            catch (System.Exception)
            {
                throw;
            }

            return categoria;
        }

        // PUT: api/Categorias/4
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Categoria categoria){
            // Se o ID do objeto não existir, retorna erro 400 - BadRequest
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }

            try
            {
                await _repositorio.Alterar(categoria);
            }
            catch (System.Exception)
            {
                // Verifica se o objeto inserido existe no banco
                Categoria categoria_valida = await _repositorio.BuscarPorID(id);

                if (categoria_valida == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // NoContent retorna 204 - Sem conteúdo
            return NoContent();
        }

        // DELETE: api/Categorias/4
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> Delete(int id){
            Categoria categoria_buscada = await _repositorio.BuscarPorID(id);

            if (categoria_buscada == null)
            {
                return NotFound(new {mensagem = "Nenhuma categoria encontrada para o ID informado"});
            }

            await _repositorio.Excluir(categoria_buscada);

            return categoria_buscada;
        }

        // GET: api/Categorias/FiltrarPorNome
        [HttpGet("FiltrarPorNome")]
        public ActionResult<List<Categoria>> GetFiltrar(FiltroViewModel filtro){

            List<Categoria> categorias_filtradas = _repositorio.FiltrarPorNome(filtro);

            return categorias_filtradas;
        }

        // GET: api/Categorias/Ordenar
        [HttpGet("Ordenar")]
        public ActionResult<List<Categoria>> GetOrdenar(){

            List<Categoria> categorias_ordenadas = _repositorio.Ordenar();

            return categorias_ordenadas;
        }
    }
}