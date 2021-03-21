using System.Collections.Generic;
using HandHistoryAnalyzer.Core.Models;

namespace HandHistoryAnalyzer.Core.HistoryReading
{
    /// <summary>
    /// An interface for poker history files parsing.
    /// </summary>
    public interface IPokerHistoryFileParser
    {
        IEnumerable<Hand> Parse(string filePath);
    }
}
