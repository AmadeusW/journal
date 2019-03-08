using Rec.Core;
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
        [InlineData("log run", "run", 1, "")]
        [InlineData("log run 30", "run", 30, "")]
        [InlineData("log run awesome", "run", 0, "awesome")]
        [InlineData("log run 30 awesome", "run", 0, "30 awesome")]
        public void TestLog(string raw, string property, int intValue, string stringValue)
        {
            var command = Parser.Parse(raw) as DataCommand;
            command.Property.ShouldBe(property);
            command.ValueInt.ShouldBe(intValue);
            command.ValueString.ShouldBe(stringValue);
        }

        [Theory]
        [InlineData("log run @4/20", "run", 1, "")]
        [InlineData("log run 30 @4/20", "run", 30, "")]
        [InlineData("log run awesome @4/20", "run", 0, "awesome")]
        [InlineData("log run 30 awesome @4/20", "run", 0, "30 awesome")]
        public void TestLogWithShortDate(string raw, string property, int intValue, string stringValue)
        {
            var command = Parser.Parse(raw) as DataCommand;
            command.Property.ShouldBe(property);
            command.ValueInt.ShouldBe(intValue);
            command.ValueString.ShouldBe(stringValue);
            command.ApplicableDate.Month.ShouldBe(4);
            command.ApplicableDate.Day.ShouldBe(20);
        }

        [Theory]
        [InlineData("log run @2017.04.20", "run", 1, "")]
        [InlineData("log run 30 @2017.04.20", "run", 30, "")]
        [InlineData("log run awesome @2017.04.20", "run", 0, "awesome")]
        [InlineData("log run 30 awesome @2017.04.20", "run", 0, "30 awesome")]
        public void TestLogWithDate(string raw, string property, int intValue, string stringValue)
        {
            var command = Parser.Parse(raw) as DataCommand;
            command.Property.ShouldBe(property);
            command.ValueInt.ShouldBe(intValue);
            command.ValueString.ShouldBe(stringValue);
            command.ApplicableDate.Month.ShouldBe(4);
            command.ApplicableDate.Day.ShouldBe(20);
            command.ApplicableDate.Year.ShouldBe(2017);
        }
    }
}
