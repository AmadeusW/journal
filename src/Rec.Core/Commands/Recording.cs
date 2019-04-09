using System;

namespace Rec.Core.Commands
{
    public class Recording : Command
    {
        public Recording(string commandString, DateTime recordedDate, string verb, string reasonRecorded, string param, double paramNumber, DateTime applicableDate, DateTime applicableDate2, string place, string place2)
            : base(commandString, recordedDate, verb, reasonRecorded, param, paramNumber, applicableDate, applicableDate2, place, place2)
        {
        }
    }
}
