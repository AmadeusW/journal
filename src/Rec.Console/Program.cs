using Rec.Core;
using System;

namespace Rec.Cli
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var parser = new CommandParser();
            var serializer = new FileReparsingSerializer("journal.txt", parser);
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

                var command = parser.Parse(query);
                if (command == null)
                {
                    Console.WriteLine("Error parsing query");
                }
                else
                {
                    journal.Add(command);
                    engine.Invoke(command);
                }
            }

            Console.WriteLine("Saving");
            serializer.Save(journal);
            Console.WriteLine("Goodbye");
        }
    }
}
