using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Commands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;

namespace Todo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<TodoItem> GetAll(
            [FromServices] ITodoRepository repository
        )
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return repository.GetAll(user);
        }


        [HttpPost]
        public GenericCommandResult Create(
            [FromBody] CreateTodoCommand command,
            [FromServices] TodoHandler handler
        )
        {

            var identity = User.Identity as ClaimsIdentity;
            var userId = identity.Claims.Where(claim => claim.Type == "id").FirstOrDefault().Value;

            return (GenericCommandResult)handler.Handle(command);
        }

     

    }
}