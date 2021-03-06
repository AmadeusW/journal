﻿using Rec.Core.Commands;
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
            if (!(command is GenericAction action))
            {
                return;
            }

            if (command.MatchesVerb("show") || command.MatchesVerb("list"))
            {
                // TODO: This should go to implementation of ListAction
                // TODO: the action should search through both verbs and params using not-exact match,
                // because "plan gardening supplies" converts to verb:gardening, param:supplies, and query "garden" does not exactly match the verb
                Console.WriteLine($"Showing {action.Param}");

                var matchingCommands = journal.Commands
                    .Select(n => n as Recording)
                    .Where(n => n != null)
                    .Where(n => n.MatchesParam(action.Param) || n.MatchesVerb(action.Param));
                foreach (var match in matchingCommands)
                {
                    Console.WriteLine(match.ToString());
                }
            }
        }
    }
}
