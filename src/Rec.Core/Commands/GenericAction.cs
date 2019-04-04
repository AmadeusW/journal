using System;
using System.Collections.Generic;
using System.Text;

namespace Rec.Core.Commands
{
    public class GenericAction : Command
    {
        public GenericAction(string commandString, string verb, string param, double paramNumber, DateTime applicableDate, DateTime applicableDate2, string place, string place2)
            : base(commandString, verb, param, paramNumber, applicableDate, applicableDate2, place, place2)
        {
        }
    }
}
