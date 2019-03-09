using System;
using System.Collections.Generic;
using System.Text;

namespace Rec.Core
{
    public class ActionCommand : Command
    {
        public string Param { get; }

        public ActionCommand(string raw, string kind, string param)
            : base(raw, kind)
        {
            Param = param;
        }
    }
}
