using System;
using Flunt.Notifications;
using Flunt.Validations;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands
{
    public class CreateTodoCommand : Notifiable, ICommand
    {
        public CreateTodoCommand() { }

        public CreateTodoCommand(string title)
        {
            Title = title;
        }

        public string Title { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMinLen(Title, 3, "Title", "No title!")
            );
        }
    }
}