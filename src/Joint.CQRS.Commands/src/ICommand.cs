namespace Joint.CQRS.Commands
{
    public interface ICommand
    {
    }

    public interface ICommand<T> : ICommand
    {
    }
}