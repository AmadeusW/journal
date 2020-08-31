using System;

namespace Journal.Core
{
    public abstract class Command
    {
        protected DateTime Timestamp {get;}

        public Command(Query query)
        {
            this.Timestamp = query.Timestamp;
        }
    }

    public abstract class WriteCommand : Command
    {
        public WriteCommand(Query query) : base(query) { }
        public abstract void Save(Query query);
    }

    public abstract class ActionCommand : Command
    {
        public ActionCommand(Query query) : base(query) { }
        public abstract void Execute();
    }
}
