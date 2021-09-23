using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.AggregatesModel.TodoItemAggregate;
using static Todo.Api.Commands.CreateTodoItemCommand;

namespace Todo.Api.Commands
{
    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, CreateTodoItemDTO>
    {
        private readonly IMediator _mediator;
        private readonly ITodoItemRepository _todoItemRepository;

        public CreateTodoItemCommandHandler() { }

        public CreateTodoItemCommandHandler(IMediator mediator, ITodoItemRepository todoItemRepository)
        {
            _mediator = mediator;
            _todoItemRepository = todoItemRepository;
        }

        public  Task<CreateTodoItemDTO> Handle(CreateTodoItemCommand message, CancellationToken cancellationToken)
        {
            // Create TodoItem object (root aggreagte)
            var todoItem = new TodoItem(message.Title, message.UserId);

            // Add Todoitem through the repo
            _todoItemRepository.Create(todoItem);

            // Saves the todoitem
            //return await
            _todoItemRepository.UnitOfWork.SaveEntitiesAsync();

            return Task.FromResult(CreateTodoItemDTO.FromTodo(todoItem));
        }

    }

    public record CreateTodoItemDTO
    {
        public string Title { get; init; }
        public Guid CustomerId { get; init; }

        public static CreateTodoItemDTO FromTodo(TodoItem todoTIem)
        {
            return new CreateTodoItemDTO()
            {
                Title = todoTIem.Title,
                CustomerId = todoTIem.CustomerId
            };
        }
    }

}