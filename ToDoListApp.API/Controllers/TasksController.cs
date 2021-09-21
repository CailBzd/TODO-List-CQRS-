
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using MediatR;
using System.Collections.Generic;
using ToDoListApp.API.Application.Queries.Tasks;
using ToDoListApp.API.Application.Commands.Tasks;
using ToDoListApp.API.Services;

namespace ToDoListApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ITaskQueries _taskQueries;
        private readonly IIdentityService _identityService;

        public TasksController(IMediator mediator, ITaskQueries taskQueries, IIdentityService identityService)
        {
            Console.WriteLine("--> TasksController");
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _taskQueries = taskQueries ?? throw new ArgumentNullException(nameof(taskQueries));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<TaskItem>>> GetTaskssAsync()
        {
            Console.WriteLine("--> GetTaskssAsync");
            var userid = _identityService.GetCustomerIdentity();
            var orders = await _taskQueries.GetTasksFromCustomerAsync(userid);

            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateTaskAsync([FromBody] CreateTaskCommand createTaskCommand)
        {
            Console.WriteLine("--> CreateTaskAsync");
            return await _mediator.Send(createTaskCommand);
        }
    }
}
