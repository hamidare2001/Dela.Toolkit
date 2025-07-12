namespace Dela.Toolkit.Application;

public interface IQuery
{
}

public interface IQueryHandler<in TQuery, TQueryResult> where TQuery :IQuery
{
    Task<TQueryResult> Handle(TQuery query, CancellationToken cancellationToken);
}

public interface IQueryDispatcher
{
    Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellationToken) where TQuery :IQuery;
}
