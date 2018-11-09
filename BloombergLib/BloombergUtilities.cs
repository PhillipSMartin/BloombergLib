using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloomberglp.Blpapi;
using System.ComponentModel;
using System.Threading;
using Gargoyle.Common;

namespace BloombergLib
{
    public class BloombergUtilities : IDisposable
    {
        private Session m_session;
        private IDictionary<string, Subscription> m_subscriptionMap = new Dictionary<string, Subscription>();
 
        public BloombergUtilities() { { WaitMs = 10000; } }

        #region Event handlers
        private event LoggingEventHandler m_infoEventHandler;
        private event LoggingEventHandler m_errorEventHandler;
        private event BloombergMessageEventHandler m_messageEventHandler;
        private event ServiceStoppedEventHandler m_readerStoppedEventHandler;

        // event fired when an exception occurs
        public event LoggingEventHandler OnError
        {
            add { m_errorEventHandler += value; }
            remove { m_errorEventHandler -= value; }
        }
        // event fired for logging
        public event LoggingEventHandler OnInfo
        {
            add { m_infoEventHandler += value; }
            remove { m_infoEventHandler -= value; }
        }
        // event fired when a message is received
        public event BloombergMessageEventHandler OnMessage
        {
            add { m_messageEventHandler += value; }
            remove { m_messageEventHandler -= value; }
        }
        // event fired when reader stops
        public event ServiceStoppedEventHandler OnReaderStopped
        {
            add { m_readerStoppedEventHandler += value; }
            remove { m_readerStoppedEventHandler -= value; }
        }
        #endregion

        #region Public Properties
        public bool IsInitialized { get; private set; }
        public bool IsConnected { get; private set; }
        public int WaitMs { get; private set; }
        public bool HadError { get; private set; }
        public string LastErrorMessage { get; private set; }
        #endregion

        #region Public Methods
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool Init()
        {
            if (!IsInitialized)
            {
                try
                {
                    SessionOptions sessionOptions = new SessionOptions()
                    {
                        ServerHost = "localhost",
                        ServerPort = 8194
                    };
                    m_session = new Session(sessionOptions);
                    IsInitialized = m_session.Start();
                    if (IsInitialized)
                        Info("BloombergUtilities initialized");
                    else
                        Info("Unable to start Bloomberg session");
                }

                catch (Exception ex)
                {
                    Error("Unable to start Bloomberg session", ex);
                }
            }
            return IsInitialized;
        }

        public bool Connect()
        {
            if (!IsConnected)
            {
                try
                {
            if (!IsInitialized)
            {
                Info("BloombergUtilities has not been initialized");
                return false;
            }

                    IsConnected = m_session.OpenService("//blp/mktdata");
            if (!IsConnected)
            {
                        Info("Unable to connect to Bloomberg");
                        return false;
                    }
                    Info("Connected to Bloomberg");

                    BackgroundWorker listenerProcess = new BackgroundWorker();
                    listenerProcess.DoWork += new DoWorkEventHandler(EventListener);
                    listenerProcess.RunWorkerAsync();
                }
                catch (Exception ex)
                {
                    Error("Unable to connect to Bloomberg", ex);
                    if (IsConnected)
                    {
                        IsConnected = false;
                        m_session.Stop();
                }
            }
            }
            return IsConnected;
        }

        public bool SubscribeToStocks(KeyValuePair<string, object>[] stocks)
        {
            try
            {
                if (stocks == null)
                    throw new ArgumentNullException("stocks");

                List<StockSubscriptionId> subscriptions = new List<StockSubscriptionId>();
                foreach (KeyValuePair<string, object> stock in stocks)
                {
                    subscriptions.Add(new StockSubscriptionId(stock.Key, stock.Value));
                }

                return Subscribe(subscriptions.ToArray());
            }
            catch (Exception ex)
            {
                Error("Unable to subscribe to stocks", ex);
                return false;
            }
        }

        public bool SubscribeToIndices(KeyValuePair<string, object>[] indices)
        {
            try
            {
                if (indices == null)
                    throw new ArgumentNullException("indices");

                List<IndexSubscriptionId> subscriptions = new List<IndexSubscriptionId>();
                foreach (KeyValuePair<string, object> index in indices)
                {
                    subscriptions.Add(new IndexSubscriptionId(index.Key, index.Value));
                }

                return Subscribe(subscriptions.ToArray());
            }
            catch (Exception ex)
            {
                Error("Unable to subscribe to indices", ex);
                return false;
            }
        }

        public bool SubscribeToOptions(KeyValuePair<string, object>[] options)
        {
            try
            {
                if (options == null)
                    throw new ArgumentNullException("options");

                List<OptionSubscriptionId> subscriptions = new List<OptionSubscriptionId>();
                foreach (KeyValuePair<string, object> option in options)
                {
                    subscriptions.Add(new OptionSubscriptionId(option.Key, option.Value));
                }

                return Subscribe(subscriptions.ToArray());
            }
            catch (Exception ex)
            {
                Error("Unable to subscribe to options", ex);
                return false;
            }
        }

        public bool SubscribeToFutures(KeyValuePair<string, object>[] futures)
        {
            try
            {
                if (futures == null)
                    throw new ArgumentNullException("futures");

                List<FutureSubscriptionId> subscriptions = new List<FutureSubscriptionId>();
                foreach (KeyValuePair<string, object> future in futures)
                {
                    subscriptions.Add(new FutureSubscriptionId(future.Key, future.Value));
                }

                return Subscribe(subscriptions.ToArray());
            }
            catch (Exception ex)
            {
                Error("Unable to subscribe to future", ex);
                return false;
            }
        }

        public bool Subscribe<T>(T[] instruments) where T : ISubscriptionId
        {
            if (!IsConnected)
            {
                Info("Must connect to Bloomberg before subscribing");
                return false;
            }
            try
            {
                if (instruments == null)
                    throw new ArgumentNullException("instruments");

                if (instruments.Length > 0)
                {
                    List<Subscription> subscriptions = new List<Subscription>();
                    foreach (T instrument in instruments)
                    {
                        lock (m_subscriptionMap)
                        {
                            if (!m_subscriptionMap.ContainsKey(instrument.Symbol))
                            {
                                CorrelationID correlationId = new CorrelationID(instrument);
                                if (instrument.BloombergSymbol == null)
                                    Info("Unable to parse symbol " + instrument.Symbol);
                                else
                                {
                                    Subscription subscription = new Subscription(instrument.BloombergSymbol, instrument.FieldList, correlationId);
                                    subscriptions.Add(subscription);
                                    m_subscriptionMap.Add(instrument.Symbol, subscription);
                                }
                            }
                        }
                    }

                    if (subscriptions.Count > 0)
                    {
                        m_session.Subscribe(subscriptions);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Error("Unable to subscribe", ex);
                return false;
            }
        }

        public bool Unsubscribe(string[] symbols)
        {
            if (!IsConnected)
            {
                Info("Must connect to Bloomberg before unsubscribing");
                return false;
            }
            try
            {
                if (symbols == null)
                    throw new ArgumentNullException("symbols");

                if (symbols.Length > 0)
                {
                    List<Subscription> subscriptions = new List<Subscription>();
                    foreach (string symbol in symbols)
                    {
                        lock (m_subscriptionMap)
                        {
                            Subscription subscription;
                            if (m_subscriptionMap.TryGetValue(symbol, out subscription))
                            {
                                subscriptions.Add(subscription);
                                m_subscriptionMap.Remove(symbol);
                            }
                        }
                    }

                    if (subscriptions.Count > 0)
                    {
                        m_session.Unsubscribe(subscriptions);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Error("Unable to subscribe", ex);
                return false;
            }
        }

        public bool UnsubscribeAll()
        {
            if (!IsConnected)
            {
                Info("Must connect to Bloomberg before unsubscribing");
                return false;
            }
            try
            {
                List<Subscription> subscriptions = new List<Subscription>();
                lock (m_subscriptionMap)
                {
                    foreach (Subscription subscription in m_subscriptionMap.Values)
                    {
                        subscriptions.Add(subscription);
                    }
                    m_subscriptionMap.Clear();
                }

                if (subscriptions.Count > 0)
                {
                    m_session.Unsubscribe(subscriptions);
                }
                return true;
            }
            catch (Exception ex)
            {
                Error("Unable to subscribe", ex);
                return false;
            }
        }

        public bool Disconnect()
        {
            if (IsConnected)
            {
                UnsubscribeAll();
                m_session.Stop();
                IsConnected = false;
                IsInitialized = false;
                Thread.Sleep(WaitMs);
            }
            return true;
        }
        #endregion

        #region Private Methods
        #region Logging
        protected void Info(string msg)
        {
            try
            {
                if (m_infoEventHandler != null)
                {
                    m_infoEventHandler(null, new LoggingEventArgs("BloombergLib", msg, null));
                }
            }
            catch
            {
            }
        }

        protected void Error(string msg, Exception e)
        {
            try
            {
                HadError = true;
                LastErrorMessage = e.Message;
                if (m_errorEventHandler != null)
                    m_errorEventHandler(null, new LoggingEventArgs("BloombergLib", msg, e));
            }
            catch
            {
            }
        }
        protected void RelayMessage(Message message)  
        {
            BloombergMessageEventArgs args = null;
            try
            {
                args = new BloombergMessageEventArgs(message);
            }
            catch (Exception ex)
            {
                Error("Error parsing Bloomberg message", ex);
            }
            try
            {
                if (m_messageEventHandler != null)
                    m_messageEventHandler(null, args);
                else
                    Info(args.MessageToXml());
            }
            catch (Exception ex)
            {
                Error("Unhandled exception in OnMessage handler", ex);
            }
        }

        private void ClearErrorState()
        {
            HadError = false;
            LastErrorMessage = null;
        }
        #endregion

         private void EventListener(object o, DoWorkEventArgs args)
        {
            try
            {
                    bool stop = false;
                    while (!stop)
                    {
                        Event eventObj = m_session.NextEvent();
                        switch (eventObj.Type)
                        {
                            case Event.EventType.SUBSCRIPTION_DATA:
                                ProcessSubscriptionData(eventObj);
                                break;
                            case Event.EventType.SUBSCRIPTION_STATUS:
                                ProcessSubscriptionStatus(eventObj);
                                break;
                            default:
                                stop = LogEvent(eventObj); // returns true if we encounter a SessionTerminated message
                                break;
                        }
                    }

                IsConnected = false;
            }
            catch (Exception ex)
            {
                Error("Error starting EventListener", ex);
                if (IsConnected)
                {
                    IsConnected = false;
                    m_session.Stop();
                }
            }

            if (m_readerStoppedEventHandler != null)
                m_readerStoppedEventHandler(null, new ServiceStoppedEventArgs("BloombergLib", "Disconnected from Bloomberg"));
        }

         private void ProcessSubscriptionData(Event eventObj)
         {
             foreach (Message message in eventObj.GetMessages())
             {
                 switch (message.MessageType.ToString())
                 {
                     case "MarketDataEvents":
                         RelayMessage(message);
                         break;
                     default:
                         Info(String.Format("Received {0} event, id={1}, msgType={2}", eventObj.Type, message.CorrelationID, message.MessageType));
                         break;
                 }
             }
         }
         private void ProcessSubscriptionStatus(Event eventObj)
         {
             foreach (Message message in eventObj.GetMessages())
             {
                 RelayMessage(message);
                 Info(String.Format("Received {0} event, id={1}, msgType={2}", eventObj.Type, message.CorrelationID, message.MessageType));
             }
         }

        // returns true if we encounter a SessionTerminated message
        private bool LogEvent(Event eventObj)
        {
             foreach (Message message in eventObj.GetMessages())
            {
                Info(String.Format("Received {0} event, id={1}, msgType={2}", eventObj.Type, message.CorrelationID, message.MessageType));
                if (eventObj.Type == Event.EventType.SESSION_STATUS && message.MessageType.Equals("SessionTerminated"))
                {
                    return true;
                }
            }
            return false;
        }

        private bool WaitAny(int millisecondsTimeout, params System.Threading.WaitHandle[] successConditionHandles)
        {
            int n;
            if (millisecondsTimeout == 0)
                n = System.Threading.WaitHandle.WaitAny(successConditionHandles);
            else
                n = System.Threading.WaitHandle.WaitAny(successConditionHandles, millisecondsTimeout);
            if (n == System.Threading.WaitHandle.WaitTimeout)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                Disconnect();
                Info("BloombergUtilities disposed");
            }
        }
        #endregion
    }
}
