namespace HandHistoryAnalyzer.Core.Querying.Models
{
    /// <summary>
    /// A model that includes player's Username and his total hands played count.
    /// </summary>
    public class UserWithHandsCount
    {
        public string Username { get; }
        public int HandsCount { get; }

        public UserWithHandsCount(string username, int handsCount)
        {
            Username = username;
            HandsCount = handsCount;
        }
    }
}
