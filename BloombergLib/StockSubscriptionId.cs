using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloombergLib
{
    public class StockSubscriptionId : SubscriptionId, ISubscriptionId
    {
        private static List<string> m_fieldList =  new List<string>(new string[]
                {
                     "BID"
                    ,"ASK"
                    ,"LAST_PRICE"
                    ,"OFFICIAL_CLOSE_TODAY_RT"
                    ,"RT_SIMP_SEC_STATUS"
                    ,"PREV_CLOSE_VALUE_REALTIME"
                    ,"OPEN"
                });

        public StockSubscriptionId(string symbol) : base("Stock", symbol) { }
        public StockSubscriptionId(string symbol, object quoteObject) : base("Stock", symbol, quoteObject) { }

        public string BloombergSymbol
        {
            get
            {
                return Symbol + " US Equity";
            }
        }

        public List<string> FieldList
        {
            get
            {
                return m_fieldList;
            }
        }
    }
}
