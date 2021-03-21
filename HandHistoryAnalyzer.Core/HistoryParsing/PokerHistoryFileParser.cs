using System.Collections.Generic;
using System.IO;
using HandHistoryAnalyzer.Core.Models;

namespace HandHistoryAnalyzer.Core.HistoryReading
{
    /// <summary>
    /// Parses poker history into the `Hand` model.
    /// </summary>
    public class PokerHistoryFileParser : IPokerHistoryFileParser
    {
        /// <summary>
        /// Takes `filePath`, parses all hands from that file
        /// and returns an `IEnumerable<Hand>` of hands parsed.
        /// </summary>
        public IEnumerable<Hand> Parse(string filePath)
        {
            var hands = new List<Hand>();

            var lines = File.ReadLines(filePath);

            Hand? hand = null;
            bool isReadingHandPlayers = false;
            foreach (var line in lines)
            {
                // PokerStars Hand #91745277977:
                if (line.StartsWith("PokerStars Hand #") || line.StartsWith("PokerStars Zoom Hand #"))
                {
                    if (hand != null)
                    {
                        hands.Add(hand);
                    }

                    var handNumber = long.Parse(
                        line[(line.IndexOf('#') + 1)..line.IndexOf(':')]
                    );

                    hand = new Hand(handNumber);
                    continue;
                }

                // Table 'Caia VII' 6-max Seat #5 is the button
                if (line.StartsWith("Table '"))
                {
                    isReadingHandPlayers = true;
                    continue;
                }

                // Seat 1: angrypaca ($25 in chips)
                if (isReadingHandPlayers)
                {
                    if (!line.StartsWith("Seat"))
                    {
                        isReadingHandPlayers = false;
                        continue;
                    }

                    var playerName = line[(line.IndexOf(':') + 2)..(line.IndexOf('(') - 1)];

                    var player = new Player(playerName);

                    hand!.AddPlayer(player);
                    continue;
                }
            }

            hands.Add(hand!);

            return hands;
        }
    }
}
