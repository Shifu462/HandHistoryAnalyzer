using System;
using System.Collections.Generic;
using System.Linq;
using HandHistoryAnalyzer.Core.Data;
using HandHistoryAnalyzer.Core.Models;
using HandHistoryAnalyzer.Core.Querying.Models;
using HandHistoryAnalyzer.Core.Querying.Requests;

namespace HandHistoryAnalyzer.Cli
{
    /// <summary>
    /// Class that handles all cli commands.
    /// </summary>
    public class CommandHandler
    {
        private delegate void CommandHandlerMethod(string[] commandArgs);

        private readonly DataContext _dataContext;
        private readonly IDictionary<string, CommandHandlerMethod> _commandHandlers;

        public CommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
            _commandHandlers = new Dictionary<string, CommandHandlerMethod>
            {
                { "top10", GetTop10Players },
                { "total", GetTotalHands },
                { "remove", RemoveHand },

                { "undo", Revert },
                { "z", Revert },

                { "quit", Quit },
                { "q", Quit },
                { "help", Help },
            };
        }

        public void Listen()
        {
            Console.WriteLine("Type 'help' to show a message about all available commands.");

            while (true)
            {
                var line = Console.ReadLine();
                if (line == null)
                {
                    return;
                }

                var commandArgs = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                Console.Clear();

                this.HandleCommand(commandArgs);
                Console.WriteLine();
            }
        }

        public void HandleCommand(string[] commandArgs)
        {
            if (!commandArgs.Any()
                || !_commandHandlers.TryGetValue(commandArgs[0], out var commandHandlerMethod))
            {
                Help(null);
                return;
            }

            commandHandlerMethod(commandArgs);
        }

        private void GetTop10Players(string[] commandArgs)
        {
            var users = _dataContext.Query<GetTopUsersRequest, IEnumerable<UserWithHandsCount>>(new(10));
            Console.WriteLine("Username - Hands count");
            foreach (var user in users)
            {
                Console.WriteLine(user.Username + " - " + user.HandsCount.ToString());
            }
        }

        private void GetTotalHands(string[] commandArgs)
        {
            if (commandArgs.Length < 2)
            {
                Console.WriteLine("Specify player nickname.");
                return;
            }

            var username = string.Join(' ', commandArgs.Skip(1));
            var total = _dataContext.Query<GetTotalHandsRequest, int>(new(username));

            if (total == 0)
            {
                Console.WriteLine($"User `{username}` does not exist in the database.");
                return;
            }

            Console.WriteLine($"{username} - {total}");
        }

        private void RemoveHand(string[] commandArgs)
        {
            if (!long.TryParse(commandArgs[1], out var handNumber))
            {
                Console.WriteLine("Invalid hand number.");
                return;
            }

            var foundHand = _dataContext.Query<RemoveHandRequest, Hand>(new(handNumber));

            if (foundHand == null)
            {
                Console.WriteLine($"Hand #{handNumber} was not found, skipping.");
                return;
            }

            Console.WriteLine($"Removed hand #{handNumber}.");
        }

        private void Revert(string[] commandArgs)
        {
            var lastCommand = _dataContext.RevertLastCommand();

            if (lastCommand == null)
            {
                Console.WriteLine("Nothing to undo!");
                return;
            }

            Console.WriteLine($"Reverted last command.");
        }

        private void Quit(string[] commandArgs)
        {
            Environment.Exit(0);
        }

        private void Help(string[] commandArgs)
        {
            Console.WriteLine(
@"top10 — Get top 10 players with most hands. The result in a form of table “Nickname - Hands”.
total — Get total amount of hands played by a given player.
remove N — Removes hand with number N.
undo, z — Undo last command.
quit, q — quits the application.
help — shows this message."
);
        }
    }
}