using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Lab.Api.Application.CQRS
{
    public class Dispatcher : IDispatcher
    {
        private readonly IServiceProvider _sp;
        public Dispatcher(IServiceProvider sp) => _sp = sp;

        public Task<TResult> Send<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>
        {
            var handler = (ICommandHandler<TCommand, TResult>?)_sp.GetService(typeof(ICommandHandler<TCommand, TResult>));
            if (handler == null) throw new InvalidOperationException($"Handler not registered for {typeof(TCommand).Name}");
            return handler.Handle(command);
        }

        public Task<TResult> Query<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var handler = (IQueryHandler<TQuery, TResult>?)_sp.GetService(typeof(IQueryHandler<TQuery, TResult>));
            if (handler == null) throw new InvalidOperationException($"Query handler not registered for {typeof(TQuery).Name}");
            return handler.Handle(query);
        }
    }
}
