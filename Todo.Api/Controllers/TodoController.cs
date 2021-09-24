using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Commands;
using Todo.Api.Queries;

namespace Todo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITodoItemQueries _todoQueries;

        public TodoController( IMediator mediator, ITodoItemQueries todoQueries)
        {
            Console.WriteLine("--> TodoController");

            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _todoQueries = todoQueries ?? throw new ArgumentNullException(nameof(todoQueries));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemSummary>>> GetTodoItemsFromCustomerAsync()
        {

            Console.WriteLine("--> TodoController : GetTodoItemsFromCustomerAsync");
            var clmIdentity = User.Identity as ClaimsIdentity;
            var userId = clmIdentity.Claims.Where(claim => claim.Type == "id").FirstOrDefault().Value;

            Console.WriteLine("--> TodoController : userId = " + userId);

            var todos = await _todoQueries.GetTodoItemsFromCustomerAsync(Guid.Parse(userId));
            return Ok(todos);
        }

        [HttpPost]
        public async Task<ActionResult<CreateTodoItemDTO>> Create([FromBody] CreateTodoItemCommand command)
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId =
                identity
                    .Claims
                    .Where(claim => claim.Type == "id")
                    .FirstOrDefault()
                    .Value;

            command.UserId = Guid.Parse(userId);

            return await _mediator.Send(command);
        }
    }
}
