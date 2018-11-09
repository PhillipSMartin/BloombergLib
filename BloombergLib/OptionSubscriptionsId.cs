using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloombergLib
{
    public class OptionSubscriptionId : SubscriptionId, ISubscriptionId
    {
        private static List<string> m_fieldList = new List<string>(new string[]
                {
                    "BID"
                    ,"ASK"
                    ,"LAST_PRICE"
                    ,"RT_SIMP_SEC_STATUS"
                    ,"DELTA_MID_RT"
                    ,"GAMMA_MID_RT"
                    ,"THETA_MID_RT"
                    ,"VEGA_MID_RT"
                    ,"IVOL_MID_RT"
             });

        public OptionSubscriptionId(string symbol) : base("Option", symbol) { }
        public OptionSubscriptionId(string symbol, object quoteObject) : base("Option", symbol, quoteObject) { }

        public string BloombergSymbol
        {
            get
            {
                try
                {
                    string classSymbol = Symbol.Substring(0, 6).Trim();
                    string year = Symbol.Substring(6, 2);
                    string month = Symbol.Substring(8, 2);
                    string day = Symbol.Substring(10, 2);
                    string callPut = Symbol.Substring(12, 1);
                    double strike = Double.Parse(Symbol.Substring(13, 8)) / 1000;

                    return string.Format("{0} US {1}/{2}/{3} {4}{5} EQUITY", classSymbol, month, day, year, callPut, strike);
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
