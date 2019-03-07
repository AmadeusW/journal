using System;

namespace Rec.Core
{
    public class Command
    {
        public string Property { get; }
        public string ValueString { get; }
        public int ValueInt { get; }

        public DateTime ApplicableDate { get; }

        public DateTime RecordedDate { get; }
        private string Kind { get; }
        private string RecordedCommand { get; set; }

        public Command(string raw, string kind, DateTime applicableDate, string property, string value)
        {
            if (String.IsNullOrWhiteSpace(property))
                throw new ArgumentNullException(nameof(property));
            Property = property;

            if (String.IsNullOrWhiteSpace(kind))
                throw new ArgumentNullException(nameof(kind));
            Kind = kind;

            if (string.IsNullOrEmpty(value))
            {
                ValueString = string.Empty;
                ValueInt = 1;
            }
            else if (Int32.TryParse(value, out int parsed))
            {
                ValueString = string.Empty;
                ValueInt = parsed;
            }
            else
            {
                ValueString = value;
                ValueInt = 0;
            }

            ApplicableDate = applicableDate;
            RecordedDate = DateTime.UtcNow;
            RecordedCommand = raw;
        }

        internal bool MatchesTopic(string query)
        {
            return string.IsNullOrEmpty(query) 
                || string.Equals(Property, query, StringComparison.InvariantCultureIgnoreCase);
        }

        internal bool MatchesApplicableDate(DateTime startDate, DateTime endDate)
        {
            if (startDate != default && startDate > ApplicableDate)
                return false;
            if (endDate != default && endDate < ApplicableDate)
                return false;
            return true;
        }
    }
}
