namespace MangaShelf.Application.Abstractions;

public interface IQueryHandler<in TQuery, TResult>
{
    public Task<TResult> Handle(TQuery query, CancellationToken cancellationToken = default);
}