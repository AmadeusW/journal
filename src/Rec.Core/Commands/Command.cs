using System;
using System.Collections.Generic;
using System.Text;

namespace Rec.Core.Commands
{
    public abstract class Command
    {
        public string Verb { get; }
        public string Param { get; }
        public double ParamNumber { get; }
        public DateTime ApplicableDate { get; }
        public DateTime ApplicableDate2 { get; }
        public string Place { get; }
        public string Place2 { get; }

        public string RecordedCommand { get; }
        public DateTime RecordedDate { get; }

        public Command(string raw, string verb, string param, double paramNumber, DateTime applicableDate, DateTime applicableDate2, string place, string place2)
        {
            if (String.IsNullOrWhiteSpace(verb))
                throw new ArgumentNullException(nameof(verb));
            Verb = verb;

            if (String.IsNullOrWhiteSpace(raw))
                throw new ArgumentNullException(nameof(raw));
            RecordedCommand = raw;

            this.Param = param;
            this.ParamNumber = paramNumber;
            this.ApplicableDate = applicableDate;
            this.ApplicableDate2 = applicableDate2;
            this.Place = place;
            this.Place2 = place2;
        }

        internal bool MatchesVerb(string query)
        {
            return string.IsNullOrEmpty(query)
                || string.Equals(Verb, query, StringComparison.InvariantCultureIgnoreCase);
        }

        internal bool MatchesApplicableDate(DateTime startDate, DateTime endDate)
        {
            bool firstDateMatchesStart = startDate == default || ApplicableDate >= startDate;
            bool firstDateMatchesEnd = endDate == default || ApplicableDate <= endDate;
            bool secondDateMatchesStart = ApplicableDate2 == default || startDate == default || ApplicableDate2 >= startDate;
            bool secondDateMatchesEnd = ApplicableDate2 == default || endDate == default || ApplicableDate2 <= endDate;

            return firstDateMatchesStart && firstDateMatchesEnd && secondDateMatchesStart && secondDateMatchesEnd;
        }

        internal bool MatchesRecordedDate(DateTime startDate, DateTime endDate)
        {
            bool recordedDateMatchesStart = startDate == default || ApplicableDate >= startDate;
            bool recordedDateMatchesEnd = endDate == default || ApplicableDate <= endDate;

            return recordedDateMatchesStart && recordedDateMatchesEnd;
        }

        internal bool MatchesParam(string query)
        {
            return string.IsNullOrEmpty(query)
                || Param.IndexOf(query, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }

        public override string ToString()
        {
            return $"{RecordedDate} - {RecordedCommand}";
        }
    }
}
