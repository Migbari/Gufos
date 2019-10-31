using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using backend.Domains;
using backend.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {   
        // Chamamos nosso contexto da vase de dados
        GufosContext _context = new GufosContext();
        // Definimos uma variável para percorrer nossos métodos com as configurações obitidas no appsettings.json
        private IConfiguration _config;
        // O método contrutor não possui void / Por padrão não tem retorno / Deve ter o mesmo nome da classe
        // Definimos um método construtor para poder acessar estas configs 
        // Joga o valor do parametro dentro da variável _config
        public LoginController(IConfiguration config){
            _config = config; 
        }
        //Chamamos nosso método para validar o usuário da aplicação
        private Usuario ValidaUsuario (LoginViewModel login) {
            var usuario = _context.Usuario.FirstOrDefault (
                u => u.Email == login.Email && u.Senha == login.Senha);
            return usuario;
        }
        // Criamos nosso método para validar o usuário da aplicação
        // login - possui as informações passadas na tela
        // var usuario = _context.Usuario.FirstOrDefault - Traz o primeiro dado se a condição do where/lambda for satisfeita
        // u => u.Email == login.Email && u.Senha == login.Senha ); - Expressão lambda que representa o "WHERE do SQL"
      
        /*   private Usuario ValidaUsuario (Usuario login) {
            var usuario = _context.Usuario.FirstOrDefault (
                u => u.Email == login.Email && u.Senha == login.Senha);

            if (usuario != null) {
                usuario = login;
            }
            return usuario;
        }    */
        // Usamos essa anotação para ignorar a autenticação neste método, já que é ele quem fará isso  
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login ([FromBody] LoginViewModel login) {
            IActionResult response = Unauthorized ();
            var user = AuthenticateUser (login);

            if (user != null) {
                var tokenString = GenerateJSONWebToken (user);
                response = Ok (new { token = tokenString });
            }
            return response;
        }
        // Verificar abaixo
        private object GenerateJSONWebToken (object user) {
            throw new NotImplementedException ();
        }
        private object AuthenticateUser (LoginViewModel login) {
            throw new NotImplementedException ();
        }
        // Geramos o Token
        private string GerarToken (Usuario userInfo) {
            // Definimos a criptografia do nosso Token
            var securityKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (_config["Jwt:Key"]));

            var credentials = new SigningCredentials (securityKey, SecurityAlgorithms.HmacSha256);

            // Definimos nossas Claims (dados da sessão)
            var claims = new [] {
                new Claim (JwtRegisteredClaimNames.NameId, userInfo.Nome),
                new Claim (JwtRegisteredClaimNames.Email, userInfo.Nome),
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid ().ToString ())
            };
            //Configuramos nosso Token e seu tempo de vida
            var token = new JwtSecurityToken (
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires : DateTime.Now.AddMinutes (120),
                signingCredentials : credentials
            );
            return new JwtSecurityTokenHandler ().WriteToken (token);
        }
        // Usamos essa anotação para ignorar a autenticação nesse método
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login ([FromBody] Usuario login) {

            IActionResult response = Unauthorized ();
            var user = ValidaUsuario (login);

            if (user != null) {
                var tokenString = GerarToken (user);
                response = Ok (new { token = tokenString });
            }
            return response;
        }
        private object ValidaUsuario(Usuario login)
        {
            throw new NotImplementedException();
        }
        private object GerarToken (object user) {
            throw new NotImplementedException ();
        }
    }
}