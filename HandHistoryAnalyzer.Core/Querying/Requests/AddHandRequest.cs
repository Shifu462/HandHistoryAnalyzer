using HandHistoryAnalyzer.Core.Models;
using HandHistoryAnalyzer.Core.Querying.Abstractions;

namespace HandHistoryAnalyzer.Core.Querying.Requests
{
    /// <summary>
    /// Request for adding a new hand into an instance of DataContext.
    /// </summary>
    public class AddHandRequest : IRequest<Hand>, IRevertable
    {
        public Hand Hand { get; }

        public AddHandRequest(Hand hand)
        {
            Hand = hand;
        }
    }
}
