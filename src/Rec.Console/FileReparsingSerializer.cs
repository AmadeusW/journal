using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Rec.Core;
using Rec.Core.Contracts;
using System.Linq;
using Rec.Core.Commands;

namespace Rec.Cli
{
    /// <summary>
    /// Serializer which reparses commands upon load
    /// </summary>
    class FileReparsingSerializer
    {
        private readonly string path;
        private readonly ISerializer serializer;

        public FileReparsingSerializer(string path, ISerializer serializer)
        {
            this.path = path;
            this.serializer = serializer;
        }

        internal List<Command> Load()
        {
            if (!File.Exists(this.path))
            {
                return new List<Command>();
            }
            var lines = File.ReadAllLines(this.path);
            var commands = lines.Select(n => serializer.Deserialize(n));
            return commands.Where(n => n != null).ToList();
        }

        internal void Save(Journal journal)
        {
            File.WriteAllLines(this.path, journal.Commands.Select(n => n.RecordedCommand));
        }

        internal void Save(Command command)
        {
            File.AppendAllLines(this.path, new string[] { serializer.Serialize(command) });
        }
    }
}
