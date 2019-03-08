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
            Journal.Add(MakeData("log", "run"));
            Journal.Add(MakeData("log", "run", "awesome"));
            Journal.Add(MakeData("log", "climb", applicableDate: yesterday));
            Journal.Add(MakeAction("show"));

            var runs = Journal.GetData("run");
            runs.Single(n => n.Property == "run" && n.ValueString == string.Empty);
            runs.Single(n => n.Property == "run" && n.ValueString == "awesome");
            runs.Count(n => n.Property == "climb").ShouldBe(0);

            var climbs = Journal.GetData("climb");
            climbs.Single(n => n.Property == "climb" && n.ApplicableDate == yesterday);
            climbs.Count(n => n.Property == "run").ShouldBe(0);

            var showActions = Journal.GetActions("show");
            showActions.Count().ShouldBe(1);
            var logActions = Journal.GetActions("log");
            logActions.Count().ShouldBe(3);
            var allActions = Journal.GetActions("");
            allActions.Count().ShouldBe(4);
        }

        [Fact]
        public void RecordAndRetrieveDuplicates()
        {
            var yesterday = DateTime.UtcNow - TimeSpan.FromDays(1);
            Journal.Add(MakeData("log", "run"));
            Journal.Add(MakeData("log", "run", "awesome"));
            Journal.Add(MakeData("log", "climb", applicableDate: yesterday));
            Journal.Add(MakeAction("show"));
            Journal.Add(MakeData("log", "run"));
            Journal.Add(MakeData("log", "run", "awesome"));
            Journal.Add(MakeData("log", "climb", applicableDate: yesterday));
            Journal.Add(MakeAction("show"));

            var runs = Journal.GetData("run");
            runs.Count(n => n.Property == "run" && n.ValueString == string.Empty).ShouldBe(2);
            runs.Count(n => n.Property == "run" && n.ValueString == "awesome").ShouldBe(2);
            var climbs = Journal.GetData("climb");
            climbs.Count(n => n.Property == "climb" && n.ApplicableDate == yesterday).ShouldBe(2);

            var showActions = Journal.GetActions("show");
            showActions.Count().ShouldBe(2);
            var logActions = Journal.GetActions("log");
            logActions.Count().ShouldBe(6);
            var allActions = Journal.GetActions("");
            allActions.Count().ShouldBe(8);
        }

        [Fact]
        public void RecordAndRetrieveByDate()
        {
            var tomorrow = DateTime.UtcNow + TimeSpan.FromDays(1);
            var today = DateTime.UtcNow;
            var yesterday = DateTime.UtcNow - TimeSpan.FromDays(1);
            var twoDaysAgo = DateTime.UtcNow - TimeSpan.FromDays(2);

            Journal.Add(MakeData("plan", "run", applicableDate: tomorrow));
            Journal.Add(MakeData("log", "run", applicableDate: today));
            Journal.Add(MakeData("log", "run", applicableDate: yesterday));
            Journal.Add(MakeData("log", "climb", applicableDate: yesterday));

            Journal.GetData("run", startDate: today, endDate: today).Count().ShouldBe(1);
            Journal.GetData("run", startDate: today, endDate: default).Count().ShouldBe(2);
            Journal.GetData("run", startDate: default, endDate: yesterday).Count().ShouldBe(1);
            Journal.GetData(startDate: default, endDate: yesterday).Count().ShouldBe(2);
            Journal.GetData(startDate: twoDaysAgo, endDate: yesterday).Count().ShouldBe(2);
            Journal.GetData(startDate: twoDaysAgo, endDate: tomorrow).Count().ShouldBe(4);
            Journal.GetData(startDate: tomorrow, endDate: twoDaysAgo).Count().ShouldBe(0);
            Journal.GetData(startDate: tomorrow, endDate: default).Count().ShouldBe(1);

            var planActions = Journal.GetActions("plan");
            planActions.Count().ShouldBe(1);
            var logActions = Journal.GetActions("log");
            logActions.Count().ShouldBe(3);
            var allActions = Journal.GetActions("");
            allActions.Count().ShouldBe(4);
        }

        private DataCommand MakeData(string kind, string property, string value = default, DateTime applicableDate = default)
        {
            if (applicableDate == default)
                applicableDate = DateTime.UtcNow;
            return new DataCommand("n/a", kind, applicableDate, property, value);
        }

        private ActionCommand MakeAction(string kind)
        {
            return new ActionCommand("n/a", kind);
        }
    }
}
