using HandHistoryAnalyzer.Core.Models;
using HandHistoryAnalyzer.Core.Querying.Abstractions;

namespace HandHistoryAnalyzer.Core.Querying.Requests
{
    /// <summary>
    /// Request for removing a hand from an instance of DataContext.
    /// </summary>
    public class RemoveHandRequest : IRequest<Hand?>
    {
        public long HandNumber { get; }

        public RemoveHandRequest(long handNumber)
        {
            HandNumber = handNumber;
        }
    }
}
