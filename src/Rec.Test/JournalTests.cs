using Rec.Core;
using Shouldly;
using System;
using Xunit;
using System.Linq;

namespace Rec.Test
{
    public class JournalTests
    {
        public Journal Journal{ get; }

        public JournalTests()
        {
            Journal = new Journal();
        }

        [Fact]
        public void RecordAndRetrieve()
        {
            var yesterday = DateTime.UtcNow - TimeSpan.FromDays(1);
            Journal.Add(Make("log", "run"));
            Journal.Add(Make("log", "run", "awesome"));
            Journal.Add(Make("log", "climb", applicableDate: yesterday));

            Journal.Commands.ShouldContain(n => n.Property == "run" && n.ValueString == string.Empty);
            Journal.Commands.ShouldContain(n => n.Property == "run" && n.ValueString == "awesome");
            Journal.Commands.ShouldContain(n => n.Property == "climb" && n.ApplicableDate == yesterday);
        }

        private Command Make(string kind, string property, string value = default, DateTime applicableDate = default)
        {
            if (applicableDate == default)
                applicableDate = DateTime.UtcNow;
            return new Command("test", kind, applicableDate, property, value);
        }
    }
}
