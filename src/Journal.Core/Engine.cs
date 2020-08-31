using System;

namespace Journal.Core
{
    public class Engine
    {
        private Composition Composition {get;} 

        public Engine(Composition composition)
        {
            this.Composition = composition;
        }

        public void Process(Query query)
        {
            foreach (var parser in this.Composition.Parsers)
            {
                var command = parser.Deserialize(query.Text);
                if (command != null)
                {
                    command.Process(query);
                    if (command is WriteCommand write)
                    {
                        // Persist the data
                        // Also journal the command itself
                        CommandJournal.Add(command);
                    }
                    if (command is ActionCommand action)
                    {
                        // Invoke the action
                        // Don't record this command
                    }
                }
            }
        }
    }
}
