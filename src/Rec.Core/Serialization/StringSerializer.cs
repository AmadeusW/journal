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
            serialized = serialized.Trim();
            if (string.IsNullOrWhiteSpace(serialized))
                return null;

            try
            {
                var split = serialized.Split(';');
                var recordedDate = DateTime.Parse(split[0]);
                var command = this.parser.Parse(split[1], recordedDate);
                return command;
            }
            catch
            {
                // Probably do some logging
                return null;
            }
        }

        string ISerializer.Serialize(Command command)
        {
            //Console.WriteLine($"Write - {command}");
            return command.RecordedDate.ToString("o")
                + ";"
                + command.RecordedCommand;
        }
    }
}
