using HandHistoryAnalyzer.Core.Querying.Abstractions;

namespace HandHistoryAnalyzer.Core.Querying.Requests
{
    /// <summary>
    /// Get total amount of hands played by a given player.
    /// </summary>
    public class GetTotalHandsRequest : IRequest<int>
    {
        public string Username { get; }

        public GetTotalHandsRequest(string username)
        {
            Username = username;
        }
    }
}
