namespace HandHistoryAnalyzer.Core.Querying.Abstractions
{
    /// <summary>
    /// Marks a request as revertable,
    /// meaning that for such request there will be created an undo request
    /// and put into `DataContext.ReverseCommands`.
    /// </summary>
    public interface IRevertable : IRequest
    {
    }
}
