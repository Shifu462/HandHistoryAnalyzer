using System.Collections.Generic;

namespace HandHistoryAnalyzer.Core.Models
{
    /// <summary>
    /// Represents a single poker hand.
    /// </summary>
    public class Hand
    {
        public long Number { get; }

        public IList<Player> Players { get; }

        public Hand(long number)
        {
            Number = number;
            Players = new List<Player>();
        }

        public void AddPlayer(Player player)
        {
            if (Players.Contains(player)) return;

            Players.Add(player);
        }
    }
}
