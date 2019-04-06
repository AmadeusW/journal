using Rec.Core;
using Rec.Core.Serialization;
using System;

namespace Rec.Cli
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var parser = new CommandParser();
            var commandSerializer = new StringSerializer(parser);
            var serializer = new FileReparsingSerializer("journal.txt", commandSerializer);
            Console.WriteLine("Loading");
            var commands = serializer.Load();
            var journal = new Journal(commands);
            var engine = new Engine(journal);
            Console.WriteLine("Ready");

            while (true)
            {
                var query = Console.ReadLine().Trim();
                if (string.Equals(query, "exit", StringComparison.InvariantCultureIgnoreCase))
                    break;

                var command = parser.Parse(query, DateTime.Now);
                if (command == null)
                {
                    Console.WriteLine("Error parsing query");
                }
                else
                {
                    journal.Add(command);
                    engine.Invoke(command);
                    serializer.Save(command);
                }
            }
            Console.WriteLine("Goodbye");
        }
    }
}
