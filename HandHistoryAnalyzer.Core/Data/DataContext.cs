using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HandHistoryAnalyzer.Core.Extensions;
using HandHistoryAnalyzer.Core.HistoryReading;
using HandHistoryAnalyzer.Core.Models;
using HandHistoryAnalyzer.Core.Querying.Abstractions;

namespace HandHistoryAnalyzer.Core.Data
{
    /// <summary>
    /// Stores hands data and provides an interface for querying the data.
    /// </summary>
    public class DataContext
    {
        private readonly QueryHandler _queryHandler;

        public ICollection<Hand> HandSet { get; } = new List<Hand>();
        public ICollection<IRevertable> ReverseCommands { get; } = new List<IRevertable>();

        public DataContext()
        {
            _queryHandler = new QueryHandler(this);
        }

        public TResult Query<TRequest, TResult>(TRequest request)
            where TRequest : IRequest<TResult>
        {
            return _queryHandler.Query<TRequest, TResult>(request);
        }

        public IRequest? RevertLastCommand()
        {
            var lastCommand = ReverseCommands.LastOrDefault();

            if (lastCommand == null) return null;

            var lastCommandType = lastCommand.GetType();
            var resultType = lastCommandType.GetGenericInterfaces(typeof(IRequest<>)).First().GenericTypeArguments[0];
            var queryMethodForLastCommand = typeof(QueryHandler).GetMethod(nameof(QueryHandler.Query))!.MakeGenericMethod(lastCommandType, resultType);

            queryMethodForLastCommand.Invoke(_queryHandler, new[] { lastCommand });

            this.ReverseCommands.Remove(lastCommand);
            return lastCommand;
        }

        public delegate void OnSuccessfulLoad(long elapsedMs);

        public void LoadFrom(string handHistoriesPath, OnSuccessfulLoad? onSuccessfulLoadDelegate = null)
        {
            var dataContext = this;

            var historyFileReader = new PokerHistoryFileParser();

            var allFilesSw = Stopwatch.StartNew();
            Parallel.ForEach(
                Directory.GetFiles(handHistoriesPath, "*.txt", SearchOption.AllDirectories),
                txtFilePath =>
                {
                    var hands = historyFileReader.Parse(txtFilePath);
                    lock (dataContext)
                    {
                        dataContext.HandSet.AddRange(hands);
                    }
                });

            allFilesSw.Stop();
            onSuccessfulLoadDelegate?.Invoke(allFilesSw.ElapsedMilliseconds);
        }
    }
}
