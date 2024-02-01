using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using MeuTodo.Repositories;
using System;
using MeuTodo.Models;
using Microsoft.AspNetCore.Authorization;

namespace MeuTodo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("tarefa")]
        public class TarefaController : ControllerBase
    {
        [HttpGet]
        // [AllowAnonymous]   //o allow AllowAnonymous permite que faça o a requisição (crud - create, read, update, delete) sem estar autenticado
        public IActionResult Read([FromServicesAttribute] ITarefaRepository repository) 
        { 
            var id = new Guid(User.Identity.Name);
            var tarefas = repository.Read(id);
            return Ok(tarefas);
        }

        [HttpPost]

        public IActionResult Create([FromBody]Tarefa model, [FromServices]ITarefaRepositoriy repositoriy)
        {
            if(!ModelState.IsValid)
               return BadRequest();

            model.UsuarioId = new Guid(User.Identity.Name);


            repositoriy.Create(model);

            return Ok();   
        }

        [HttpPut("{id}")]

        public IActionResult Create(string id, [FromBody]Tarefa model, [FromServices]ITarefaRepositoriy repositoriy)
        {
            if(!ModelState.IsValid)
            return BadRequest();

            repositoriy.Update(new Guid(id), model);

            return Ok();
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(string id, [FromServices]ITarefaRepositoriy repositoriy)
        {
            repositoriy.Delete(new Guid(id));
            return Ok();
        }

        
    }
}