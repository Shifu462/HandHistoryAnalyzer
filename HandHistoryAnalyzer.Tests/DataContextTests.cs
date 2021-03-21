using System.Linq;
using HandHistoryAnalyzer.Core.Data;
using Xunit;

namespace HandHistoryAnalyzer.Tests
{
    public class DataContextTests
    {
        private readonly long[] _handNumbers = new[]
        {
            91745277977,
            91745326330,
            91745423481,
            91745442980,
            91745502364,
        };

        private readonly string[] _usernames = new[]
        {
            "angrypaca",
            "gusutafu",
            "VASIL287",
            "MarcoGiovani",
            "gabypoker221",
        };

        [Fact]
        public void DataContext_LoadFrom()
        {
            var dataContext = new DataContext();

            long elapsedMs = -1;
            dataContext.LoadFrom("./histories/single-history/", ms => elapsedMs = ms);

            Assert.NotEqual(-1, elapsedMs);
            Assert.Equal(5, dataContext.HandSet.Count);

            Assert.True(
                dataContext.HandSet.Select(x => x.Number).SequenceEqual(_handNumbers),
                "Incorrect numbers were read from the history file."
            );

            Assert.All(dataContext.HandSet, h =>
                Assert.True(
                    h.Players.Select(p => p.Username).SequenceEqual(_usernames),
                    $"Incorrect usernames were read for hand #{h.Number}."
                )
            );
        }
    }
}
