using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Journal.Core
{
    // Adapted from https://github.com/KirillOsenkov/QuickInfo - thank you Kirill!
    public class Composition
    {
        private List<IParser> parsers = new List<IParser>();
        public IEnumerable<IParser> Parsers => parsers;

        public Composition() : this(new[] { typeof(Composition).Assembly })
        {
        }

        public Composition(params Assembly[] assemblies)
        {
            if (assemblies.Length == 0)
            {
                assemblies = new[] { typeof(Composition).Assembly };
            }

            foreach (var assembly in assemblies)
            {
                var parserTypes = assembly
                    .GetTypes()
                    .Where(t => t.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IParser)));
                parsers.AddRange(parserTypes.Select(t => (IParser)Activator.CreateInstance(t)));
            }

            SortParsers();
        }

        public Composition(params Type[] types)
        {
            var instances = types.Select(t => (IParser)Activator.CreateInstance(t));
            parsers.AddRange(instances);
            SortParsers();
        }

        private void SortParsers()
        {
            // TODO: sort by a defined property of each parser
            parsers.Sort((l, r) => string.CompareOrdinal(l.GetType().Name, r.GetType().Name));
        }
    }
}
