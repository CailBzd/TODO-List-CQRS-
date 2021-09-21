using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using ToDoListApp.Domain.AggregatesModel.TaskAggregate;
using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.API.Application.Commands.Tasks
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, bool>
    {

        private readonly ITaskItemRepository _taskRepository;
        private readonly IMediator _mediator;
        public CreateTaskCommandHandler(IMediator mediator, ITaskItemRepository taskRepository)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(CreateTaskCommand message, CancellationToken cancellationToken)
        {
            var task = new TaskItem(message.NameTask, message.CustomerId);

            _taskRepository.Add(task);

            return await _taskRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

    }

    // public record CreateTaskDTO
    // {
    //    [Required]
    //     public string name { get; set; }

    // }
}