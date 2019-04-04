using Rec.Core;
using Rec.Core.Commands;
using Shouldly;
using System;
using Xunit;

namespace Rec.Test
{
    public class ParserTests
    {
        public CommandParser Parser { get; }

        public ParserTests()
        {
            Parser = new CommandParser();
        }

        [Theory]
        [InlineData("run", "run", 0, "")]
        [InlineData("run 30", "run", 30, "30")]
        [InlineData("run awesome", "run", 0, "awesome")]
        [InlineData("run 30 awesome", "run", 0, "30 awesome")]
        public void TestLog(string raw, string property, int intValue, string stringValue)
        {
            var command = Parser.Parse(raw, default) as Recording;
            command.Verb.ShouldBe(property);
            command.ParamNumber.ShouldBe(intValue);
            command.Param.ShouldBe(stringValue);
        }

        [Theory]
        [InlineData("run @4/20", "run", 0, "")]
        [InlineData("run 30 @4/20", "run", 30, "30")]
        [InlineData("run awesome @4/20", "run", 0, "awesome")]
        [InlineData("run 30 awesome @4/20", "run", 0, "30 awesome")]
        public void TestLogWithShortDate(string raw, string property, int intValue, string stringValue)
        {
            var command = Parser.Parse(raw, default) as Recording;
            command.Verb.ShouldBe(property);
            command.ParamNumber.ShouldBe(intValue);
            command.Param.ShouldBe(stringValue);
            command.ApplicableDate.Month.ShouldBe(4);
            command.ApplicableDate.Day.ShouldBe(20);
        }

        [Theory]
        [InlineData("run @2017.04.20", "run", 0, "")]
        [InlineData("run 30 @2017.04.20", "run", 30, "30")]
        [InlineData("run awesome @2017.04.20", "run", 0, "awesome")]
        [InlineData("run 30 awesome @2017.04.20", "run", 0, "30 awesome")]
        public void TestLogWithDate(string raw, string property, int intValue, string stringValue)
        {
            var command = Parser.Parse(raw, default) as Recording;
            command.Verb.ShouldBe(property);
            command.ParamNumber.ShouldBe(intValue);
            command.Param.ShouldBe(stringValue);
            command.ApplicableDate.Month.ShouldBe(4);
            command.ApplicableDate.Day.ShouldBe(20);
            command.ApplicableDate.Year.ShouldBe(2017);
        }
    }
}
