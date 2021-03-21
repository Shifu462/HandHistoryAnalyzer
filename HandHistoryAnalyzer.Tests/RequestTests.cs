using System.Collections.Generic;
using System.Linq;
using HandHistoryAnalyzer.Core.Data;
using HandHistoryAnalyzer.Core.Models;
using HandHistoryAnalyzer.Core.Querying.Models;
using HandHistoryAnalyzer.Core.Querying.Requests;
using Xunit;

namespace HandHistoryAnalyzer.Tests
{
    public class RequestTests
    {
        private DataContext CreateDefaultDataContext()
        {
            var dataContext = new DataContext();
            var player1 = new Player("some-player");
            var hand1 = new Hand(1);
            var hand2 = new Hand(2);
            hand1.AddPlayer(player1);
            hand2.AddPlayer(player1);
            dataContext.HandSet.Add(hand1);
            dataContext.HandSet.Add(hand2);
            dataContext.HandSet.Add(new Hand(3));

            return dataContext;
        }

        [Fact]
        public void GetTotalHandsRequest_ReturnsCorrectTotal()
        {
            var dataContext = CreateDefaultDataContext();
            var player1 = dataContext.HandSet.First().Players.Single();

            var totalBySomePlayer = dataContext.Query<GetTotalHandsRequest, int>(new GetTotalHandsRequest(player1.Username));

            Assert.Equal(2, totalBySomePlayer);
        }

        [Fact]
        public void GetTopUsersRequest_ReturnsCorrectTop()
        {
            var dataContext = CreateDefaultDataContext();
            var player1 = dataContext.HandSet.First().Players.Single();

            var count = 2;
            var usersTop = dataContext.Query<GetTopUsersRequest, IEnumerable<UserWithHandsCount>>(new GetTopUsersRequest(count));

            var singleTopUser = usersTop.Single();
            Assert.Equal(player1.Username, singleTopUser.Username);
            Assert.Equal(2, singleTopUser.HandsCount);
        }
    }
}
