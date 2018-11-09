using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloombergLib
{
    public class IndexSubscriptionId : SubscriptionId, ISubscriptionId
    {
        private static List<string> m_fieldList = new List<string>(new string[]
                {
                    "LAST_PRICE"
                    ,"OFFICIAL_CLOSE_TODAY_RT"
                    ,"RT_SIMP_SEC_STATUS"
                    ,"PREV_CLOSE_VALUE_REALTIME"
                    ,"OPEN"
             });

        public IndexSubscriptionId(string symbol) : base("Index", symbol) { }
        public IndexSubscriptionId(string symbol, object quoteObject) : base("Index", symbol, quoteObject) { }

        public string BloombergSymbol
        {
            get
            {
                return Symbol + " INDEX";
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
