using System;
using System.Linq;
using HandHistoryAnalyzer.Core.Data;
using HandHistoryAnalyzer.Core.Models;
using HandHistoryAnalyzer.Core.Querying.Abstractions;
using HandHistoryAnalyzer.Core.Querying.Requests;

namespace HandHistoryAnalyzer.Core.Querying.RequestHandlers
{
    public class AddHandRequestHandler : IRequestHandler<AddHandRequest, Hand>
    {
        private readonly DataContext _dataContext;

        public AddHandRequestHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Hand Handle(AddHandRequest request)
        {
            if (_dataContext.HandSet.Any(h => h.Number == request.Hand.Number))
            {
                throw new InvalidOperationException("Hand with such number already exists.");
            }

            _dataContext.HandSet.Add(request.Hand);
            return request.Hand;
        }
    }
}
