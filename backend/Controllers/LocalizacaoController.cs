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
    public class LocalizacaoController : ControllerBase {
        //GufosContext _contexto = new GufosContext ();
        LocalizacaoRepository _repositorio = new LocalizacaoRepository();
        // GET: api/Localizacao
        [HttpGet]
         public async Task<ActionResult<List<Localizacao>>> Get () // list chama toda a tabela
        {   
            // Include("") = Adiciona efetivamente a árvore de objetos relacionados
            var localizacaos = await _repositorio.Listar();

            if (localizacaos == null) {
                return NotFound ();
            }
            return localizacaos;
        }
        // GET: api/Localizacao/2
        [HttpGet ("{id}")]
        public async Task<ActionResult<Localizacao>> Get (int id) {
            // FindAsync = procura algo específico no banco
            // await 
            var localizacao = await _repositorio.BuscarPorId(id);

            if (localizacao == null) {
                return NotFound ();
            }
            return localizacao;
        }
        // POST api/Localizacao
        // POST api/Localizacao
        [HttpPost]
        public async Task<ActionResult<Localizacao>> POST (Localizacao localizacao) {
            try {
                // Tratamos contra ataques de SQL Injection
                // await _contexto.AddAsync (localizacao);
                // Salvamos efetivamente o nosso objeto no banco de dados
                // await _contexto.SaveChangesAsync ();
                await _repositorio.Salvar (localizacao);
               return localizacao;
            } catch (DbUpdateConcurrencyException) {
                return BadRequest();     
              //  throw; // Mostra erro automaticamente // Mostra a Exception
            }
        }
        [HttpPut ("{id}")]
        public async Task<ActionResult> Put (int id, Localizacao localizacao) {
            // Se o Id do objeto não existir ele retorna badrequest 400
            if (id != localizacao.LocalizacaoId) {
                return BadRequest (); // Badrequest usuario errou
            }
            // Comparamos os atributos que foram modificados através do EF
            // _contexto.Entry (localizacao).State = EntityState.Modified;

            try {
              await _repositorio.Alterar (localizacao);
            } catch (DbUpdateConcurrencyException) {

                // Verificamos se o objeto inserido realmente existe no banco
                var localizacao_valido = await _repositorio.BuscarPorId(id);

                if (localizacao_valido == null) {
                    return NotFound ();
                } else {
                    throw;
                }
            }
            // NoContent = Retorna 204 // 204 no content - sem conteudo
            return NoContent ();
        }

        // DELETE api/localizacao/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Localizacao>> Delete(int id){

            var localizacao = await _repositorio.BuscarPorId(id);
            if (localizacao == null){
                return NotFound(); // notfound - não existe
            }
            localizacao = await _repositorio.Excluir(localizacao);
            // _contexto.Localizacao.Remove(localizacao);
            // await _contexto.SaveChangesAsync();
            return localizacao;
        }
    }
}