namespace HandHistoryAnalyzer.Core.Querying.Abstractions
{
    /// <summary>
    /// Handles `TRequest` and returns `TResult`.
    /// </summary>
    public interface IRequestHandler<TRequest, TResult>
    {
        TResult Handle(TRequest request);
    }
}
