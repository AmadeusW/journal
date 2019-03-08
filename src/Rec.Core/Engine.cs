using System;
using System.Collections.Generic;
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
            Console.WriteLine($"Pretending we are invoking {action.Kind}");
        }
    }
}
