using System;
using System.Collections.Generic;
using Rec.Core.Commands;

namespace Rec.Core
{
    public class Journal
    {
        private readonly List<Command> _commands;
        public IEnumerable<Command> Commands => _commands;

        public Journal() : this(new List<Command>())
        {
        }

        public Journal(List<Command> commands)
        {
            _commands = commands;
        }

        public void Add(Command command)
        {
            _commands.Add(command);
        }

        public IEnumerable<Recording> GetData(
            string kind = default, 
            DateTime startDate = default, 
            DateTime endDate = default)
        {
            for (int i = 0; i < _commands.Count; i++)
            {
                if (!(_commands[i] is Recording command))
                    continue;

                if (!command.MatchesVerb(kind))
                    continue;

                if (!command.MatchesApplicableDate(startDate, endDate))
                    continue;

                yield return command;
            }
        }

        public IEnumerable<Command> GetActions(
            string kind = default,
            DateTime startDate = default,
            DateTime endDate = default)
        {
            for (int i = 0; i < _commands.Count; i++)
            {
                var command = _commands[i];

                if (!command.MatchesVerb(kind))
                    continue;

                if (!command.MatchesApplicableDate(startDate, endDate))
                    continue;

                yield return command;
            }
        }
    }
}
