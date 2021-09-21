namespace ToDoListApp.API.Application.Commands
{
        public abstract class AddSingleEntityCommandBase : SingleEntityCommandBase
    {
        public bool IsAdding => Id == 0;
    }
}