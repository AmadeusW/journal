using System;
using System.Collections.Generic;
using System.Text;

namespace Rec.Core
{
    public abstract class Command
    {
        public DateTime RecordedDate { get; }
        private string Kind { get; }
        private string RecordedCommand { get; set; }

        public Command(string raw, string kind)
        {
            if (String.IsNullOrWhiteSpace(kind))
                throw new ArgumentNullException(nameof(kind));
            Kind = kind;

            if (String.IsNullOrWhiteSpace(raw))
                throw new ArgumentNullException(nameof(raw));
            RecordedCommand = raw;

            RecordedDate = DateTime.UtcNow;
        }

        internal bool MatchesKind(string query)
        {
            return string.IsNullOrEmpty(query)
                || string.Equals(Kind, query, StringComparison.InvariantCultureIgnoreCase);
        }

        internal bool MatchesRecordedDate(DateTime startDate, DateTime endDate)
        {
            if (startDate != default && startDate > RecordedDate)
                return false;
            if (endDate != default && endDate < RecordedDate)
                return false;
            return true;
        }
    }
}
