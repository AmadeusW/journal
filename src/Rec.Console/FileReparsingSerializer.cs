using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Rec.Core;
using Rec.Core.Contracts;
using System.Linq;

namespace Rec.Cli
{
    /// <summary>
    /// Serializer which reparses commands upon load
    /// </summary>
    class FileReparsingSerializer : ISerializer
    {
        private readonly string path;
        private readonly CommandParser parser;

        public FileReparsingSerializer(string path, CommandParser parser)
        {
            this.path = path;
            this.parser = parser;
        }

        internal List<Command> Load()
        {
            if (!File.Exists(this.path))
            {
                return new List<Command>();
            }
            var lines = File.ReadAllLines(this.path);
            var commands = lines.Select(n => parser.Parse(n));
            return commands.ToList();
        }

        internal void Save(Journal journal)
        {
            File.WriteAllLines(this.path, journal.Commands.Select(n => n.RecordedCommand));
        }
    }
}
