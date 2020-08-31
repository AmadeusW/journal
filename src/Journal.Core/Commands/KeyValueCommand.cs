using System;

namespace Journal.Core.Commands
{
    public class KeyValueCommandParser : IParser
    {
        public Command? Parse(Query query)
        {
            if (query.Parts.Length != 2)
                return null;
            return new KeyValueCommand(query.parts[0], query.parts[1], query);
        }

        public string Describe => "<key> <value>";
    }

    // TODO: this requires an interface so that it can be composed
    public class KeyValueCommand : WriteCommand
    {
        public string Key {get;}
        public string Value {get;}

        public KeyValueCommand(Query query, string key, string value) : base(query)
        {
            this.Key = key ?? throw new ArgumentNullException(nameof(key));
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public void Save()
        {
            // TODO: a way to persist this data
        }
    }
}
