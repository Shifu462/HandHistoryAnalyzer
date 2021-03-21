using System;
using System.Collections.Generic;
using System.Linq;
using HandHistoryAnalyzer.Core.Extensions;
using HandHistoryAnalyzer.Core.Querying.Abstractions;

namespace HandHistoryAnalyzer.Core.Data
{
    /// <summary>
    /// Handles queries on the data.
    /// </summary>
    public class QueryHandler
    {
        private readonly DataContext _dataContext;
        private readonly IDictionary<Type, Type> _requestHandlers;

        public QueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
            _requestHandlers = typeof(QueryHandler).Assembly
                .GetExportedTypes().Where(t => t.GetGenericInterfaces(typeof(IRequestHandler<,>)).Any())
                .ToDictionary(
                    t => t.GetGenericInterfaces(typeof(IRequestHandler<,>)).First().GenericTypeArguments[0],
                    t => t);
        }

        /// <summary>
        /// Performs a query on the data from `_dataContext`.
        /// </summary>
        public TResult Query<TRequest, TResult>(TRequest request)
            where TRequest : IRequest<TResult>
        {
            var requestHandler = Activator.CreateInstance(
                _requestHandlers[request.GetType()],
                new[] { _dataContext }) as IRequestHandler<TRequest, TResult>;

            return requestHandler!.Handle(request);
        }
    }
}
