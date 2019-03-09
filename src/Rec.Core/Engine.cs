using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rec.Core
{
    public class Engine
    {
        private readonly Journal journal;

        public Engine(Journal journal)
        {
            this.journal = journal;
        }

        public void Invoke(Command command)
        {
            if (!(command is ActionCommand action))
            {
                return;
            }

            if (action.MatchesKind("show"))
            {
                var topic = action.Param;
                Console.WriteLine($"Showing {topic}");

                var matchingCommands = journal.Commands
                    .Select(n => n as DataCommand)
                    .Where(n => n != null)
                    .Where(n => n.MatchesTopic(topic));
                foreach (var match in matchingCommands)
                {
                    Console.WriteLine(match.ToString());
                }
            }
        }
    }
}
