using System.Collections.Generic;

namespace Peasy
{
    public interface IRulesContainer
    {
        List<IRule[]> Rules { get; }
    }
}
