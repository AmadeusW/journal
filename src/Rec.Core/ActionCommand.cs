using System;
using System.Collections.Generic;
using System.Text;

namespace Rec.Core
{
    public class ActionCommand : Command
    {
        public ActionCommand(string raw, string kind)
            : base(raw, kind)
        {

        }
    }
}
