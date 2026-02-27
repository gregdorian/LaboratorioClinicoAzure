using System.Threading.Tasks;
using System.Collections.Generic;

namespace Lab.Api.Application.CQRS
{
    public interface ICommand<TResult> { }

    public interface ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        Task<TResult> Handle(TCommand command);
    }

    public interface IQuery<TResult> { }

    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<TResult> Handle(TQuery query);
    }

    public interface IDispatcher
    {
        Task<TResult> Send<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>;
        Task<TResult> Query<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}
