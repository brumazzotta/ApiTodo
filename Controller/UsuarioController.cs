using MeuTodo.Models;
using MeuTodo.Repositories;
using Microsoft.AspNetCore.Mvc;
using MeuTodo.Models.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Security.Claims;
using System.Security.Cryptography;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text;

namespace MeuTodo.Controllers
{
    // localhost/usuario
    [ApiController]
    [Route("usuario")] 
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody]Usuario model, [FromServices]IUsuarioRepository repository)
        {
            if(!ModelState.IsValid)

                return BadRequest();

            repository.Create(model);

                return Ok();
        }

        [HttpPost]
        [Route("login")]

        public IActionResult Login([FromBody]UsuarioLogin model, [FromServices]IUsuarioRepository repository)
        {
            if(!ModelState.IsValid)
                return BadRequest();

                Usuario usuario = repository.Read(model.Email, model.Senha);

                if(usuario == null)
                    return Unauthorized();

                usuario.Senha ="";    

                return Ok(new {
                        usuario = usuario,
                        token = GenerateToken(usuario)
                });

        }

        private string GenerateToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            //token inserido manualmente mas o ideal é ter o certificado em outro arquivo
            //var key = "TokenzãoGrandãoParaDificultarDescriptografar"; 
            var keyByte = Encoding.ASCII.GetBytes("TokenzãoGrandãoParaDificultarDescriptografar");

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, usuario.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);

        }

    }
}