using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloombergLib
{
    public class FutureSubscriptionId : SubscriptionId, ISubscriptionId
    {
        private static List<string> m_fieldList = new List<string>(new string[]
                {
                    "LAST_PRICE"
                    ,"OFFICIAL_CLOSE_TODAY_RT"
                    ,"RT_SIMP_SEC_STATUS"
                });

        public FutureSubscriptionId(string symbol) : base("Future", symbol) { }
        public FutureSubscriptionId(string symbol, object quoteObject) : base("Future", symbol, quoteObject) { }

        public string BloombergSymbol
        {
            get
            {
                try
                {
                    string classSymbol = Symbol.Substring(0, 2);
                    string year = Symbol.Substring(4, 1);
                    string month = Symbol.Substring(5, 1);

                    return string.Format("{0}{1}{2} Index", classSymbol, month, year);
                }
                catch (Exception)
                {
                    return null;
                }
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
