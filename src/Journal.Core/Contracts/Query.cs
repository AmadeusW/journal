using System;

namespace Journal.Core
{
    public sealed class Query
    {
        public string UserId {get;}
        public DateTime Timestamp {get;}
        public string RawText {get;}
        public string[] Parts {get;}

        public Query(string rawText, string userId)
            : this(rawText, userId, DateTime.UtcNow)
        {
        }

        public Query(string rawText, string userId, DateTime timestamp)
        {
            this.RawText = rawText ?? throw new ArgumentNullException(nameof(rawText));
            this.UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            this.Timestamp = timestamp;
            this.Parts = rawText.Trim().Split(' ');
        }
    }
}
