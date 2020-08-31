using System;

namespace Journal.Core
{
    public abstract class Command
    {
        DateTime Timestamp {get;}

        public Command(Query query)
        {
            this.Timestamp = query.Timestamp;
        }
    }

    public abstract class WriteCommand : Command
    {
        void Save(Query query);
    }

    public abstract class ActionCommand : Command
    {
        void Execute();
    }
}
