using Rec.Core;
using Shouldly;
using System;
using Xunit;
using System.Linq;
using Rec.Core.Commands;

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
            Journal.Add(MakeData("run"));
            Journal.Add(MakeData("run", "awesome"));
            Journal.Add(MakeData("climb", applicableDate: yesterday));
            Journal.Add(MakeAction("show", "run"));

            var runs = Journal.GetData("run");
            runs.Single(n => n.Verb == "run" && n.Param == string.Empty);
            runs.Single(n => n.Verb == "run" && n.Param == "awesome");
            runs.Count(n => n.Verb == "climb").ShouldBe(0);

            var climbs = Journal.GetData("climb");
            climbs.Single(n => n.Verb == "climb" && n.ApplicableDate == yesterday);
            climbs.Count(n => n.Verb == "run").ShouldBe(0);

            var showActions = Journal.GetActions("show");
            showActions.Count().ShouldBe(1);
            var logActions = Journal.GetActions("run");
            logActions.Count().ShouldBe(2);
            var allActions = Journal.GetActions("");
            allActions.Count().ShouldBe(4);
        }

        [Fact]
        public void RecordAndRetrieveDuplicates()
        {
            var yesterday = DateTime.UtcNow - TimeSpan.FromDays(1);
            Journal.Add(MakeData("run"));
            Journal.Add(MakeData("run", "awesome"));
            Journal.Add(MakeData("climb", applicableDate: yesterday));
            Journal.Add(MakeAction("show", "run"));
            Journal.Add(MakeData("run"));
            Journal.Add(MakeData("run", "awesome"));
            Journal.Add(MakeData("climb", applicableDate: yesterday));
            Journal.Add(MakeAction("show", "run"));

            var runs = Journal.GetData("run");
            runs.Count(n => n.Verb == "run" && n.Param == string.Empty).ShouldBe(2);
            runs.Count(n => n.Verb == "run" && n.Param == "awesome").ShouldBe(2);
            var climbs = Journal.GetData("climb");
            climbs.Count(n => n.Verb == "climb" && n.ApplicableDate == yesterday).ShouldBe(2);

            var showActions = Journal.GetActions("show");
            showActions.Count().ShouldBe(2);
            var logActions = Journal.GetActions("run");
            logActions.Count().ShouldBe(4);
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
            Journal.Add(MakeData("run", applicableDate: today));
            Journal.Add(MakeData("run", applicableDate: yesterday));
            Journal.Add(MakeData("climb", applicableDate: yesterday));

            Journal.GetData("run", startDate: today, endDate: today).Count().ShouldBe(1);
            Journal.GetData("run", startDate: default, endDate: yesterday).Count().ShouldBe(1);
            Journal.GetData("run", startDate: yesterday, endDate: today).Count().ShouldBe(2);
            // todo: retrieve planned runs
            Journal.GetData(startDate: default, endDate: yesterday).Count().ShouldBe(2);
            Journal.GetData(startDate: twoDaysAgo, endDate: yesterday).Count().ShouldBe(2);
            Journal.GetData(startDate: twoDaysAgo, endDate: tomorrow).Count().ShouldBe(4);
            Journal.GetData(startDate: tomorrow, endDate: twoDaysAgo).Count().ShouldBe(0);
            Journal.GetData(startDate: tomorrow, endDate: default).Count().ShouldBe(1);

            var planActions = Journal.GetActions("plan");
            planActions.Count().ShouldBe(1);
            var runActions = Journal.GetActions("run");
            runActions.Count().ShouldBe(2);
            var allActions = Journal.GetActions("");
            allActions.Count().ShouldBe(4);
        }

        private Recording MakeData(string verb, string param = "", DateTime applicableDate = default, string place = "")
        {
            if (applicableDate == default)
                applicableDate = DateTime.UtcNow;
            return new Recording("n/a", DateTime.Now, verb, "", param, 0, applicableDate, default, place, string.Empty);
        }

        private GenericAction MakeAction(string verb, string param = "", DateTime applicableDate = default, string place = "")
        {
            return new GenericAction("n/a", DateTime.Now, verb, param, 0, applicableDate, default, place, string.Empty);
        }
    }
}
