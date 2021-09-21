using System;
using Flunt.Notifications;
using Flunt.Validations;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands
{
    public class CreateTodoCommand : Notifiable, ICommand
    {
        public CreateTodoCommand() { }

        public CreateTodoCommand(string title,string userId)
        {
            Title = title;
            UserId = userId;
        }

        public string Title { get; set; }
        public string UserId { get; set; }

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