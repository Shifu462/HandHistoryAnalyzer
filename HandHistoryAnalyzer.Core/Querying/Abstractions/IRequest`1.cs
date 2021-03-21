namespace HandHistoryAnalyzer.Core.Querying.Abstractions
{
    /// <summary>
    /// Represents a request, for which the `TResult` will be returned. <para />
    /// May be just a query or a command with side effects.
    /// </summary>
    public interface IRequest<TResult> : IRequest
    {
    }
}
