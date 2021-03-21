using System.Collections.Generic;
using HandHistoryAnalyzer.Core.Querying.Abstractions;
using HandHistoryAnalyzer.Core.Querying.Models;

namespace HandHistoryAnalyzer.Core.Querying.Requests
{
    /// <summary>
    /// Get top 10 players with most hands. The result in a form of table “Nickname - Hands”.
    /// </summary>
    public class GetTopUsersRequest : IRequest<IEnumerable<UserWithHandsCount>>
    {
        public int Count { get; }

        public GetTopUsersRequest(int count)
        {
            Count = count;
        }
    }
}
