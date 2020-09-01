using System.Threading.Tasks;

namespace Joint.CQRS.Commands
{
    public interface ICommandDispatcher
    {
        Task SendAsync<T>(T command) where T : class, ICommand;
        Task<TResult> SendAsync<TResult>(ICommand<TResult> command);
        Task<TResult> SendAsync<TCommand, TResult>(TCommand command) where TCommand : class, ICommand<TResult>;
    }
}