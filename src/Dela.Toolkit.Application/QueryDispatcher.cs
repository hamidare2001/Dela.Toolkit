using Microsoft.Extensions.DependencyInjection;

namespace Dela.Toolkit.Application;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellationToken) where TQuery : IQuery
    {
        var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TQueryResult>>();
        return handler.Handle(query, cancellationToken);
    }
}