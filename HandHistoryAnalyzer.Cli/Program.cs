using System;
using System.IO;
using HandHistoryAnalyzer.Core.Data;

namespace HandHistoryAnalyzer.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!ValidateCommandLineArgs(args)) return;

            var handHistoriesPath = args[0];

            Console.WriteLine("Loading hand histories, it may take some time (under 1 min).");
            var dataContext = new DataContext();
            dataContext.LoadFrom(
                handHistoriesPath,
                onSuccessfulLoadDelegate:
                    elapsedMs => Console.WriteLine($"Read {dataContext.HandSet.Count} file histories in: {elapsedMs} ms.")
            );

            var commandHandler = new CommandHandler(dataContext);
            commandHandler.Listen();
        }

        private static bool ValidateCommandLineArgs(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please specify path to hand histories directory as a first command line argument.");
                return false;
            }

            var handHistoriesPath = args[0];

            if (!Directory.Exists(handHistoriesPath))
            {
                Console.WriteLine("Directory does not exist.");
                return false;
            }

            return true;
        }
    }
}
