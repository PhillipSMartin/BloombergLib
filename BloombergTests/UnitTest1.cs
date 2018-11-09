using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BloombergLib;
using System.Collections.Generic;
using log4net;
using Gargoyle.Common;

namespace BloombergTests
{
    [TestClass]
    public class UnitTest1
    {
        BloombergUtilities m_utilities = new BloombergUtilities();
        private ILog m_logger = LogManager.GetLogger(typeof(UnitTest1));

        [TestInitialize]
        public void SetupTests()
        {
            log4net.Config.XmlConfigurator.Configure();

            m_utilities.OnError += utilities_OnError;
            m_utilities.OnInfo += utilities_OnInfo;
            m_utilities.OnMessage += utilities_OnMessage;
         }

        [TestMethod]
        public void TestLog4Net()
        {
            m_logger.Info("Test message");
        }

        [TestMethod]
        public void TestBloombergOptionSymbol()
        {
            OptionSubscriptionId option = new OptionSubscriptionId("SPY   170519C00276500");
            Assert.AreEqual("SPY US 05/19/17 276.5 EQUITY", option.BloombergSymbol, "Wrong Bloomberg symbol");
        }

        [TestMethod]
        public void TestBloombergFuturesSymbol()
        {
            FutureSubscriptionId future = new FutureSubscriptionId("ES/17M");
            Assert.AreEqual("ESM7 Index", future.BloombergSymbol, "Wrong Bloomberg symbol");
        }

        [TestMethod]
        public void TestBloombergIndexSymbol()
        {
            IndexSubscriptionId index = new IndexSubscriptionId("SPY");
            Assert.AreEqual("SPY INDEX", index.BloombergSymbol, "Wrong Bloomberg symbol");
        }

        [TestMethod]
        public void TestStockSubscription()
        {
  
            bool success = true;
            bool init = m_utilities.Init();
            Assert.AreEqual(success, init, "Initialization failed");
            if (!init) return;

            bool connected = m_utilities.Connect();
            Assert.AreEqual(success, connected, "Connection failed");
            if (!connected) return;

            List<KeyValuePair<string, object>> stockList = new List<KeyValuePair<string, object>>();
            stockList.Add(new KeyValuePair<string, object>("IBM", null));
            //stockList.Add("MSFT");
            stockList.Add(new KeyValuePair<string, object>("AAPL", null));

            bool started = m_utilities.SubscribeToStocks(stockList.ToArray());
            Assert.AreEqual(success, started, "Subscription failed");
            if (!started) return;

            DateTime stopDateTime = DateTime.Today + new TimeSpan(16, 20, 0);
            int tickTime = (int)(stopDateTime - DateTime.Now).TotalMilliseconds;
            if (tickTime <= 120000)
                tickTime = 120000;  // make sure stop time is at least 2 minutes from now
            System.Threading.Thread.Sleep(tickTime);

            bool stopped = m_utilities.Disconnect();
            Assert.AreEqual(success, stopped, "Stop failed");
        }

        [TestMethod]
        public void TestIndexSubscription()
        {
  
            bool success = true;
            bool init = m_utilities.Init();
            Assert.AreEqual(success, init, "Initialization failed");
            if (!init) return;

            bool connected = m_utilities.Connect();
            Assert.AreEqual(success, connected, "Connection failed");
            if (!connected) return;

            List<KeyValuePair<string, object>> stockList = new List<KeyValuePair<string, object>>();
            stockList.Add(new KeyValuePair<string, object>("SPX", null));
            stockList.Add(new KeyValuePair<string, object>("RUT", null));

            bool started = m_utilities.SubscribeToIndices(stockList.ToArray());
            Assert.AreEqual(success, started, "Subscription failed");
            if (!started) return;

            DateTime stopDateTime = DateTime.Today + new TimeSpan(16, 20, 0);
            int tickTime = (int)(stopDateTime - DateTime.Now).TotalMilliseconds;
            if (tickTime <= 120000)
                tickTime = 120000;  // make sure stop time is at least 2 minutes from now
            System.Threading.Thread.Sleep(tickTime);

            bool stopped = m_utilities.Disconnect();
            Assert.AreEqual(success, stopped, "Stop failed");
        }


        [TestMethod]
        public void TestFutureSubscription()
        {

            bool success = true;
            bool init = m_utilities.Init();
            Assert.AreEqual(success, init, "Initialization failed");
            if (!init) return;

            bool connected = m_utilities.Connect();
            Assert.AreEqual(success, connected, "Connection failed");
            if (!connected) return;

            List<KeyValuePair<string, object>> futuresList = new List<KeyValuePair<string, object>>();
            futuresList.Add(new KeyValuePair<string, object>("ES/17M", null));
 
            bool started = m_utilities.SubscribeToFutures(futuresList.ToArray());
            Assert.AreEqual(success, started, "Subscription failed");
            if (!started) return;

            DateTime stopDateTime = DateTime.Today + new TimeSpan(16, 20, 0);
            int tickTime = (int)(stopDateTime - DateTime.Now).TotalMilliseconds;
            if (tickTime <= 120000)
                tickTime = 120000;  // make sure stop time is at least 2 minutes from now
            System.Threading.Thread.Sleep(tickTime);

            bool stopped = m_utilities.Disconnect();
            Assert.AreEqual(success, stopped, "Stop failed");
        }

        void utilities_OnMessage(object sender, EventArgs e)
        {
            BloombergMessageEventArgs args = e as BloombergMessageEventArgs;

            Bloomberglp.Blpapi.Message message = args.Message;
            string eventType = message.GetElementAsString("EVENT_TYPE");
            if ((eventType == "QUOTE") || (eventType == "SUMMARY"))
            {
                m_logger.Info(args.Message);
            }
        }

        void utilities_OnInfo(object sender, LoggingEventArgs e)
        {
            m_logger.Info(e.Message);
         }

        void utilities_OnError(object sender, LoggingEventArgs e)
        {
            m_logger.Error(e.Message, e.Exception);
        }
    }
}
