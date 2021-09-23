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
        private readonly ITodoItemsQueries _todoQueries;

        public TodoController( IMediator mediator, ITodoItemsQueries todoQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _todoQueries = todoQueries ?? throw new ArgumentNullException(nameof(todoQueries));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemSummary>>> GetTodoItemsFromCustomerAsync()
        {

            var clmIdentity = User.Identity as ClaimsIdentity;
            var userId = clmIdentity.Claims.Where(claim => claim.Type == "id").FirstOrDefault().Value;

            var todos = await _todoQueries.GetTodoItemsFromCustomerAsync(Guid.Parse(userId));
            return Ok(todos);
        }

        [HttpPost]
        public async Task<ActionResult<CreateTodoItemDTO>> Create([FromBody] CreateTodoItemCommand command)
        {
            //var identity = User.Identity as ClaimsIdentity;
            //var userId =
            //    identity
            //        .Claims
            //        .Where(claim => claim.Type == "id")
            //        .FirstOrDefault()
            //        .Value;

            //command.UserId = userId;

            return await _mediator.Send(command);
        }
    }
}
