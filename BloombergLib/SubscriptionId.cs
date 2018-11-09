using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloomberglp.Blpapi;

namespace BloombergLib
{
    public abstract class SubscriptionId
    {
        protected SubscriptionId(string correlationType, string symbol)
        {
            CorrelationType = correlationType;
            Symbol = symbol;
        }
        protected SubscriptionId(string correlationType, string symbol, object quoteObject)
        {
            CorrelationType = correlationType;
            Symbol = symbol;
            QuoteObject = quoteObject;
        }

        public string CorrelationType { get; private set; }
        public string Symbol { get; private set; }
        public object QuoteObject { get; private set; }

        public override string ToString()
        {
            return String.Format("{0}: {1}", CorrelationType, Symbol);
        }       
     }
}
