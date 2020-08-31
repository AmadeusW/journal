using System;

namespace Journal.Core
{
    public interface IParser
    {
        Command? Parse(Query query);
        string Description {get;}
    }
}
