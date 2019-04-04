using Rec.Core.Commands;
using Rec.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rec.Core.Serialization
{
    public class StringSerializer : ISerializer
    {
        private readonly CommandParser parser;

        public StringSerializer(CommandParser parser)
        {
            this.parser = parser;
        }

        Command ISerializer.Deserialize(string serialized)
        {
            var split = serialized.Split(';');
            var recordedDate = DateTime.Parse(split[0]);
            var command = this.parser.Parse(split[1], recordedDate);
            return command;
        }

        string ISerializer.Serialize(Command command)
        {
            //Console.WriteLine($"Write - {command}");
            return command.RecordedDate.ToLongDateString()
                + ";"
                + command.RecordedCommand;
        }
    }
}
