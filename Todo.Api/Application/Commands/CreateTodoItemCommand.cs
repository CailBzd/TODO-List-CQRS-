using MediatR;
using System;

namespace Todo.Api.Commands
{
    public class CreateTodoItemCommand : IRequest<CreateTodoItemDTO>
    {

        public string Title { get; set; }
        public Guid UserId { get; set; }

        public CreateTodoItemCommand() { }

        public CreateTodoItemCommand(string title,Guid userId)
        {
            Title = title;
            UserId = userId;
        }

    }
}