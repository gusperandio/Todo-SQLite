using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController]
    //[Route("/home")] //! Prefixo de rota
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("/")]
        public IActionResult Get([FromServices] AppDbContext context)
        => Ok(context.Todos.ToList());


        [HttpGet("/{id}")]
        public IActionResult GetById(int id, [FromServices] AppDbContext context)
        {
            var todo = context.Todos.FirstOrDefault(x => x.Id == id);

            if (todo is null)
                return NotFound("Não encontrado");

            return Ok(todo);
        }

        [HttpPost("/")]
        public IActionResult Posts(
            [FromBody] TodoModel todo,
            [FromServices] AppDbContext context)
        {
            context.Todos.Add(todo);
            context.SaveChanges();

            return Created($"/{todo.Id}", todo);
        }

        [HttpPut("/{id}")]
        public IActionResult Put(int id, [FromBody] TodoModel todo, [FromServices] AppDbContext context)
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);
            if (model is null)
                return NotFound("Não encontado");

            model.Title = todo.Title;
            model.Done = todo.Done;

            context.Todos.Update(model);
            context.SaveChanges();
            return Ok(model);
        }

        [HttpDelete("/{id}")]
        public IActionResult Delete(int id, [FromServices] AppDbContext context)
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);

            if (model is null)
                return NotFound("Não encontado");

            context.Todos.Remove(model);
            context.SaveChanges();
            return Ok(model);
        }

    }
}