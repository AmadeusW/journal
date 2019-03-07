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

            var runs = Journal.Get("run");
            runs.Single(n => n.Property == "run" && n.ValueString == string.Empty);
            runs.Single(n => n.Property == "run" && n.ValueString == "awesome");
            runs.Count(n => n.Property == "climb").ShouldBe(0);

            var climbs = Journal.Get("climb");
            climbs.Single(n => n.Property == "climb" && n.ApplicableDate == yesterday);
            climbs.Count(n => n.Property == "run").ShouldBe(0);
        }

        [Fact]
        public void RecordAndRetrieveDuplicates()
        {
            var yesterday = DateTime.UtcNow - TimeSpan.FromDays(1);
            Journal.Add(Make("log", "run"));
            Journal.Add(Make("log", "run", "awesome"));
            Journal.Add(Make("log", "climb", applicableDate: yesterday));
            Journal.Add(Make("log", "run"));
            Journal.Add(Make("log", "run", "awesome"));
            Journal.Add(Make("log", "climb", applicableDate: yesterday));

            var runs = Journal.Get("run");
            runs.Count(n => n.Property == "run" && n.ValueString == string.Empty).ShouldBe(2);
            runs.Count(n => n.Property == "run" && n.ValueString == "awesome").ShouldBe(2);
            var climbs = Journal.Get("climb");
            climbs.Count(n => n.Property == "climb" && n.ApplicableDate == yesterday).ShouldBe(2);
        }

        [Fact]
        public void RecordAndRetrieveByDate()
        {
            var tomorrow = DateTime.UtcNow + TimeSpan.FromDays(1);
            var today = DateTime.UtcNow;
            var yesterday = DateTime.UtcNow - TimeSpan.FromDays(1);
            var twoDaysAgo = DateTime.UtcNow - TimeSpan.FromDays(2);

            Journal.Add(Make("plan", "run", applicableDate: tomorrow));
            Journal.Add(Make("log", "run", applicableDate: today));
            Journal.Add(Make("log", "run", applicableDate: yesterday));
            Journal.Add(Make("log", "climb", applicableDate: yesterday));

            Journal.Get("run", startDate: today, endDate: today).Count().ShouldBe(1);
            Journal.Get("run", startDate: today, endDate: default).Count().ShouldBe(2);
            Journal.Get("run", startDate: default, endDate: yesterday).Count().ShouldBe(1);
            Journal.Get(startDate: default, endDate: yesterday).Count().ShouldBe(2);
            Journal.Get(startDate: twoDaysAgo, endDate: yesterday).Count().ShouldBe(2);
            Journal.Get(startDate: twoDaysAgo, endDate: tomorrow).Count().ShouldBe(4);
            Journal.Get(startDate: tomorrow, endDate: twoDaysAgo).Count().ShouldBe(0);
            Journal.Get(startDate: tomorrow, endDate: default).Count().ShouldBe(1);
        }

        private Command Make(string kind, string property, string value = default, DateTime applicableDate = default)
        {
            if (applicableDate == default)
                applicableDate = DateTime.UtcNow;
            return new Command("test", kind, applicableDate, property, value);
        }
    }
}
