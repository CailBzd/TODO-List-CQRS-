using System;
using MediatR;

namespace ToDoListApp.API.Application.Commands.Tasks
{
    public class CreateTaskCommand : IRequest<bool>
    {
        public string CustomerId { get; set; }
        public string NameTask { get; set; }
        public string State { get; set; }

        public CreateTaskCommand(string customerId, string nameTask, string state) 
        {
                CustomerId = customerId;
                NameTask = nameTask;
                State = state;
        }
    }
}