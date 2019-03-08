using System;
using System.Collections.Generic;
using System.Text;

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

        public IEnumerable<DataCommand> GetData(
            string topic = default, 
            DateTime startDate = default, 
            DateTime endDate = default)
        {
            for (int i = 0; i < _commands.Count; i++)
            {
                if (!(_commands[i] is DataCommand command))
                    continue;

                if (!command.MatchesTopic(topic))
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

                if (!command.MatchesKind(kind))
                    continue;

                if (!command.MatchesRecordedDate(startDate, endDate))
                    continue;

                yield return command;
            }
        }
    }
}
