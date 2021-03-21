using System.Collections.Generic;
using System.Linq;
using HandHistoryAnalyzer.Core.Models;
using Xunit;

namespace HandHistoryAnalyzer.Tests
{
    public class HandTests
    {
        [Fact]
        public void Hand_AddPlayer_AddsOnlyUniquePlayers()
        {
            var hand = new Hand(123);
            hand.AddPlayer(new Player("me"));
            hand.AddPlayer(new Player("opp"));
            hand.AddPlayer(new Player("opp"));

            var expectedPlayers = new[]
            {
                new Player("me"),
                new Player("opp"),
            };

            Assert.True(
                hand.Players.SequenceEqual(expectedPlayers),
                $"Hand players set differs from expected.\nExpected: {GetPlayersDisplay(expectedPlayers)}\nActual: {GetPlayersDisplay(hand.Players)}."
            );
        }

        private static string GetPlayersDisplay(IEnumerable<Player> players)
            => string.Join(", ",
                players.Select(p => p.ToString()));
    }
}
