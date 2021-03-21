using System;
using System.Linq;
using HandHistoryAnalyzer.Core.Data;
using HandHistoryAnalyzer.Core.Models;
using HandHistoryAnalyzer.Core.Querying.Requests;
using Xunit;

namespace HandHistoryAnalyzer.Tests
{
    public class CommandTests
    {
        private Hand CreateDefaultHand()
        {
            var hand = new Hand(123);
            hand.AddPlayer(new Player("some-player"));
            return hand;
        }

        [Fact]
        public void AddHandRequest_AddsHand()
        {
            var dataContext = new DataContext();
            var hand = this.CreateDefaultHand();

            var addedHand = dataContext.Query<AddHandRequest, Hand>(new(hand));

            Assert.Equal(hand, addedHand);
            Assert.Contains(hand, dataContext.HandSet);
        }

        [Fact]
        public void RemoveHandRequest_RemovesHand_And_CreatesProperReverseCommand()
        {
            var dataContext = new DataContext();
            var hand = this.CreateDefaultHand();
            dataContext.HandSet.Add(hand);

            var removedHand = dataContext.Query<RemoveHandRequest, Hand>(new(hand.Number));

            Assert.Equal(hand, removedHand);
            Assert.DoesNotContain(hand, dataContext.HandSet);

            var reverseCommand = dataContext.ReverseCommands.Single();

            if (reverseCommand is not AddHandRequest addHandRequest)
            {
                throw new Exception($"Reverse command created is not of type {nameof(AddHandRequest)}.");
            }

            Assert.Equal(hand, addHandRequest.Hand);
        }

        [Fact]
        public void RemoveHandRequest_IsReversible()
        {
            var dataContext = new DataContext();
            var hand = this.CreateDefaultHand();
            dataContext.HandSet.Add(hand);

            dataContext.Query<RemoveHandRequest, Hand>(new(hand.Number));

            var executedRequest = dataContext.RevertLastCommand();

            Assert.True(executedRequest is AddHandRequest);
            Assert.Contains(hand, dataContext.HandSet);
        }
    }
}
