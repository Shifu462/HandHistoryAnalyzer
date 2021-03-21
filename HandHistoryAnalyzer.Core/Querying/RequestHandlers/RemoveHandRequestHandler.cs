using System.Linq;
using HandHistoryAnalyzer.Core.Data;
using HandHistoryAnalyzer.Core.Models;
using HandHistoryAnalyzer.Core.Querying.Abstractions;
using HandHistoryAnalyzer.Core.Querying.Requests;

namespace HandHistoryAnalyzer.Core.Querying.RequestHandlers
{
    public class RemoveHandRequestHandler : IRequestHandler<RemoveHandRequest, Hand?>
    {
        private readonly DataContext _dataContext;

        public RemoveHandRequestHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Hand? Handle(RemoveHandRequest request)
        {
            var hand = _dataContext.HandSet.FirstOrDefault(h => h.Number == request.HandNumber);

            if (hand == null)
            {
                return null;
            }

            _dataContext.HandSet.Remove(hand);
            _dataContext.ReverseCommands.Add(new AddHandRequest(hand));

            return hand;
        }
    }
}
