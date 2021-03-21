using System.Collections.Generic;
using System.Linq;
using HandHistoryAnalyzer.Core.Data;
using HandHistoryAnalyzer.Core.Querying.Abstractions;
using HandHistoryAnalyzer.Core.Querying.Models;
using HandHistoryAnalyzer.Core.Querying.Requests;

namespace HandHistoryAnalyzer.Core.Querying.RequestHandlers
{
    public class GetTopUsersRequestHandler : IRequestHandler<GetTopUsersRequest, IEnumerable<UserWithHandsCount>>
    {
        private readonly DataContext _dataContext;

        public GetTopUsersRequestHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<UserWithHandsCount> Handle(GetTopUsersRequest request)
        {
            return _dataContext.HandSet
                .SelectMany(h => h.Players)
                .Select(p => p.Username)
                .GroupBy(x => x)
                .Select(g => new UserWithHandsCount(g.Key, g.Count()))
                .OrderByDescending(uh => uh.HandsCount)
                .Take(request.Count)
                .ToArray();
        }
    }
}
