using System;
using System.Collections.Generic;
using System.Text;

namespace Rec.Core.Contracts
{
    public interface ISerializer
    {
        Command Deserialize(string serialized);
        string Serialize(Command command);
    }
}
