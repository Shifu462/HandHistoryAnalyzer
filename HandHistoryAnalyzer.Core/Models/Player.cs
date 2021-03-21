namespace HandHistoryAnalyzer.Core.Models
{
    /// <summary>
    /// Represents a player in a hand.
    /// </summary>
    public record Player
    {
        public string Username { get; }

        public Player(string username)
        {
            Username = username;
        }
    }
}
