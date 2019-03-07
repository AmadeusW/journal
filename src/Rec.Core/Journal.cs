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

        public IEnumerable<Command> Get(string v)
        {
            for (int i = 0; i < _commands.Count; i++)
            {
                var command = _commands[i];
                if (command.Matches(v))
                {
                    yield return command;
                }
            }
        }
    }
}
