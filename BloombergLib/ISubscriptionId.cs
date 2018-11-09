using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloombergLib
{
    public interface ISubscriptionId
    {
        string CorrelationType { get; }
        string Symbol { get; }
        object QuoteObject { get;  }
        string BloombergSymbol { get; }
        List<string> FieldList { get; }
    }
}
