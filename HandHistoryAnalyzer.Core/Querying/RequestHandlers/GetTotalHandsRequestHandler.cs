using System.Linq;
using HandHistoryAnalyzer.Core.Data;
using HandHistoryAnalyzer.Core.Querying.Abstractions;
using HandHistoryAnalyzer.Core.Querying.Requests;

namespace HandHistoryAnalyzer.Core.Querying.RequestHandlers
{
    public class GetTotalHandsRequestHandler : IRequestHandler<GetTotalHandsRequest, int>
    {
        private readonly DataContext _dataContext;

        public GetTotalHandsRequestHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public int Handle(GetTotalHandsRequest request)
        {
            return _dataContext.HandSet
                .Count(h => h.Players.Any(p => p.Username == request.Username));
        }
    }
}
