using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BloombergLib;
using log4net;
using Gargoyle.Messaging.Common;
using Gargoyle.Common;
using GargoyleMessageLib;

namespace BloombergClient
{
    // sample option SPY   170519C00238000
    public partial class Form1 : Form
    {
        private List<ISubscriptionId> m_subscriptions = new List<ISubscriptionId>();
        private BloombergUtilities m_nativeUtilities = new BloombergUtilities();
        private GargoyleMessageUtilities m_messageUtilities = new GargoyleMessageLib.GargoyleMessageUtilities();
        private ILog m_log = LogManager.GetLogger(typeof(Form1));

        public Form1()
        {
            InitializeComponent();

            log4net.Config.XmlConfigurator.Configure();

            m_nativeUtilities.OnError += m_utilities_OnError;
            m_nativeUtilities.OnInfo += m_utilities_OnInfo;
            m_nativeUtilities.OnMessage += m_nativeUtilities_OnMessage;

            m_messageUtilities.OnInfo += m_utilities_OnInfo;
            m_messageUtilities.OnError += m_utilities_OnError;
            m_messageUtilities.OnDebug += m_Utilities_OnDebug;
            m_messageUtilities.OnQuote += m_messageUtilities_OnQuote;
            m_messageUtilities.OnSubscriberStopped += m_messageUtilities_OnReaderStopped;
      }

 

        void m_Utilities_OnDebug(object sender, LoggingEventArgs e)
        {
            OnDebug(e.Message);
        }

        void m_utilities_OnInfo(object sender, LoggingEventArgs e)
        {
            OnInfo(e.Message);
        }
        void m_utilities_OnError(object sender, LoggingEventArgs e)
        {
            OnError(e.Message, e.Exception);
        }

        void m_messageUtilities_OnQuote(object sender, QuoteEventArgs e)
        {
            try
            {
                DataSet1.StockQuotesRow row = e.ClientObject as DataSet1.StockQuotesRow;
                OnQuote(e.Quote, row);
            }
            catch (Exception ex)
            {
                OnError("Error reading QuoteEventArgs", ex);
            }
        }

        private void OnQuote(QuoteMessage quote, DataSet1.StockQuotesRow row)
        {
            Action a = delegate
            {
                try
                {

                    if (checkBoxLog.Checked)
                    {
                        m_log.Info(quote.ToXml());
                    }

                    bool bUpdated = false;
                    row.Subscribed = (quote.SubscriptionStatus == SubscriptionStatus.Subscribed);

                    if (quote.HasOpen)
                    {
                        row.Open = quote.Open;
                            bUpdated = true;
                    }

                    if (quote.HasPrevClose)
                    {
                        row.PrevClose = quote.PrevClose;
                        bUpdated = true;
                    }

                    if (quote.HasLast)
                    {
                        if (!row.Closed)
                        {
                            row.LastPrice = quote.Last;
                            bUpdated = true;
                        }
                    }

                    if (quote.HasBid)
                    {
                        if (!row.Closed)
                        {
                            row.Bid = quote.Bid;
                            bUpdated = true;
                        }
                    }
                    if (quote.HasAsk)
                    {
                        if (!row.Closed)
                        {
                            row.Ask = quote.Ask;
                            bUpdated = true;
                        }
                    }
                    if (quote.HasDelta)
                    {
                        if (!row.Closed)
                        {
                            row.Delta = quote.Delta;
                            bUpdated = true;
                        }
                    }
                    if (quote.HasGamma)
                    {
                        if (!row.Closed)
                        {
                            row.Gamma = quote.Gamma;
                            bUpdated = true;
                        }
                    }
                    if (quote.HasTheta)
                    {
                        if (!row.Closed)
                        {
                            row.Theta = quote.Theta;
                            bUpdated = true;
                        }
                    }
                    if (quote.HasVega)
                    {
                        if (!row.Closed)
                        {
                            row.Vega = quote.Vega;
                            bUpdated = true;
                        }
                    }
                    if (quote.HasImpliedVol)
                    {
                        if (!row.Closed)
                        {
                            row.ImpliedVol = quote.ImpliedVol;
                            bUpdated = true;
                        }
                    }

                    if (quote.HasClose)
                    {
                        row.ClosingPrice = quote.Close;
                        bUpdated = true;
                    }

                    if (quote.OpenStatus == OpenStatus.Closed)
                    {
                        row.Closed = true;
                        bUpdated = true;
                    }

                    if (quote.OpenStatus == OpenStatus.Open)
                    {
                        row.Closed = false;
                        bUpdated = true;
                    }

                    if (bUpdated)
                    {
                        row.Time = DateTime.Now - DateTime.Today;
                    }
                }
                catch (Exception ex)
                {
                    OnError("Error in quote handler", ex);
                }
            };

            if (InvokeRequired)
                BeginInvoke(a);
            else
                a.Invoke();
        }

        private void OnDebug(string message)
        {
            Action a = delegate
            {
                m_log.Debug(message);
                listBoxLog.Items.Add(message);
            };
            if (InvokeRequired)
                BeginInvoke(a);
            else
                a.Invoke();
        }

        private void OnInfo(string message)
        {
            Action a = delegate
            {
                m_log.Info(message);
                listBoxLog.Items.Add(message);
            };
            if (InvokeRequired)
                BeginInvoke(a);
            else
                a.Invoke();
        }

        private void OnError(string message, Exception exception)
        {
            Action a = delegate
            {
                m_log.Error(message, exception);
                if (exception == null)
                {
                    listBoxLog.Items.Add(message);
                }
                else
                {
                    listBoxLog.Items.Add(message + "=>" + exception.Message);
                }
            };
            if (InvokeRequired)
                BeginInvoke(a);
            else
                a.Invoke();
        }

        void m_messageUtilities_OnReaderStopped(object sender, ServiceStoppedEventArgs e)
        {
            OnError(e.Message, e.Exception);

            Action a = delegate
           {
               EnableControls();
           };
            if (InvokeRequired)
                BeginInvoke(a);
            else
                a.Invoke();
        }

        void m_nativeUtilities_OnMessage(object sender, EventArgs e)
        {
            Action a = delegate
            {
                try
                {
                    BloombergMessageEventArgs args = e as BloombergMessageEventArgs;
                    ISubscriptionId subscription = args.SubscriptionId;
                    DataSet1.StockQuotesRow row = subscription.QuoteObject as DataSet1.StockQuotesRow;

                    if (checkBoxLog.Checked)
                    {
                        m_log.Info(args.MessageToXml());
                    }

                    bool bUpdated = false;
                    switch (args.Message.MessageType.ToString())
                    {
                        case "SubscriptionStarted":
                            row.Subscribed = true;
                            break;

                        case "SubscriptionTerminated":
                        case "SubscriptionFailure":
                            row.Subscribed = false;
                            break;

                        case "MarketDataEvents":

                            if (args.Message.HasElement("OPEN"))
                            {
                                row.Open = (double)args.Message.GetElementAsFloat64("OPEN");
                                bUpdated = true;
                            }

                            if (args.Message.HasElement("PREV_CLOSE_VALUE_REALTIME"))
                            {
                                row.PrevClose = (double)args.Message.GetElementAsFloat64("PREV_CLOSE_VALUE_REALTIME");
                                bUpdated = true;
                            }

                            if (args.Message.HasElement("LAST_PRICE"))
                            {
                                if (!row.Closed)
                                {
                                    row.LastPrice = (double)args.Message.GetElementAsFloat64("LAST_PRICE");
                                    bUpdated = true;
                                }
                            }

                            if (args.Message.HasElement("BID"))
                            {
                                if (!row.Closed)
                                {
                                    row.Bid = (double)args.Message.GetElementAsFloat64("BID");
                                    bUpdated = true;
                                }
                            }
                            if (args.Message.HasElement("ASK"))
                            {
                                if (!row.Closed)
                                {
                                    row.Ask = (double)args.Message.GetElementAsFloat64("ASK");
                                    bUpdated = true;
                                }
                            }
                            if (args.Message.HasElement("MID"))
                            {
                                if (!row.Closed)
                                {
                                    row.Mid = (double)args.Message.GetElementAsFloat64("MID");
                                    bUpdated = true;
                                }
                            }
                            if (args.Message.HasElement("DELTA_MID_RT"))
                            {
                                if (!row.Closed)
                                {
                                    row.Delta = (double)args.Message.GetElementAsFloat64("DELTA_MID_RT");
                                    bUpdated = true;
                                }
                            }
                            if (args.Message.HasElement("GAMMA_MID_RT"))
                            {
                                if (!row.Closed)
                                {
                                    row.Gamma = (double)args.Message.GetElementAsFloat64("GAMMA_MID_RT");
                                    bUpdated = true;
                                }
                            }
                            if (args.Message.HasElement("THETA_MID_RT"))
                            {
                                if (!row.Closed)
                                {
                                    row.Theta = (double)args.Message.GetElementAsFloat64("THETA_MID_RT");
                                    bUpdated = true;
                                }
                            }
                            if (args.Message.HasElement("VEGA_MID_RT"))
                            {
                                if (!row.Closed)
                                {
                                    row.Vega = (double)args.Message.GetElementAsFloat64("VEGA_MID_RT");
                                    bUpdated = true;
                                }
                            }
                            if (args.Message.HasElement("IVOL_MID_RT"))
                            {
                                if (!row.Closed)
                                {
                                    row.ImpliedVol = (double)args.Message.GetElementAsFloat64("IVOL_MID_RT");
                                    bUpdated = true;
                                }
                            }

                            if (args.Message.HasElement("OFFICIAL_CLOSE_TODAY_RT"))
                            {
                                double closingPrice = (double)args.Message.GetElementAsFloat64("OFFICIAL_CLOSE_TODAY_RT");
                                row.ClosingPrice = closingPrice;
                                bUpdated = true;
                            }

                            if (args.Message.HasElement("RT_SIMP_SEC_STATUS"))
                                switch (args.Message.GetElementAsString("RT_SIMP_SEC_STATUS"))
                                {
                                    case "CLOS":
                                    case "POST":
                                    case "OUT":
                                        if (!row.Closed)
                                        {
                                            row.Closed = true;
                                            bUpdated = true;
                                        }
                                        break;
                                    default:
                                        if (row.Closed)
                                        {
                                            row.Closed = false;
                                            bUpdated = true;
                                        }
                                        break;
                                }
                            break;
                    }

                    if (bUpdated)
                    {
                        row.Time = DateTime.Now - DateTime.Today;
                    }
                }
                catch (Exception ex)
                {
                    OnError("Error in message handler", ex);
                }
            };

            if (InvokeRequired)
                BeginInvoke(a);
            else
                a.Invoke();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            DataSet1.StockQuotesRow row = dataSet1.StockQuotes.NewStockQuotesRow();
            row.Symbol = textBoxStock.Text;
            dataSet1.StockQuotes.AddStockQuotesRow(row);

            StockSubscriptionId subscription = new StockSubscriptionId(row.Symbol, (object)row);
            m_subscriptions.Add(subscription);

            if (m_nativeUtilities.IsConnected)
            {
                m_nativeUtilities.Subscribe(new StockSubscriptionId[] { subscription });
            }
            else if (m_messageUtilities.IsSubscriberStarted)
            {
                m_messageUtilities.Subscribe(subscription.Symbol, QuoteType.Stock, subscription.QuoteObject);
            }
      }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                radioButtonTCPBloomberg.Enabled = radioButtonNativeBloomberg.Enabled = false;

                if (radioButtonNativeBloomberg.Checked)
                {
                    if (m_nativeUtilities.Init())
                    {
                        if (m_nativeUtilities.Connect())
                        {
                            if (m_subscriptions.Count > 0)
                            {
                                m_nativeUtilities.Subscribe(m_subscriptions.ToArray());
                            }
                        }
                    }
                }
                else if (radioButtonTCPBloomberg.Checked)
                {
                    if (m_messageUtilities.StartSubscriber(Properties.Settings.Default.BloombergHost, Properties.Settings.Default.Port))
                    {
                        foreach (SubscriptionId id in m_subscriptions)
                        {
                            m_messageUtilities.Subscribe(id.Symbol, (QuoteType)Enum.Parse(typeof(QuoteType), id.CorrelationType, true), id.QuoteObject);
                        }
                    }
                }
                else
                {
                    if (m_messageUtilities.StartSubscriber(Properties.Settings.Default.TWSHost, Properties.Settings.Default.Port))
                    {
                        foreach (SubscriptionId id in m_subscriptions)
                        {
                            m_messageUtilities.Subscribe(id.Symbol, (QuoteType)Enum.Parse(typeof(QuoteType), id.CorrelationType, true), id.QuoteObject);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                OnError("Error in start processing", ex);
            }
            finally
            {
                EnableControls();
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonNativeBloomberg.Checked)
                {
                    m_nativeUtilities.Disconnect();
                }
                else
                {
                    m_messageUtilities.StopSubscriber();
                }
            }
            catch (Exception ex)
            {
                OnError("Error in stop processing", ex);
            }
            finally
            {
                EnableControls();
            }
         }

        private void EnableControls()
        {
            bool isStarted = false;
            if (radioButtonNativeBloomberg.Checked)
                isStarted = m_nativeUtilities.IsConnected;
            else
                isStarted = m_messageUtilities.IsSubscriberStarted;
            buttonStart.Enabled = !isStarted;
            buttonStop.Enabled = isStarted;
           radioButtonTCPBloomberg.Enabled = radioButtonNativeBloomberg.Enabled = !isStarted;
         }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            foreach (ISubscriptionId subscription in m_subscriptions)
            {
                if (subscription.Symbol == textBoxStock.Text)
                {
                    m_subscriptions.Remove(subscription);
                    dataSet1.StockQuotes.Rows.Remove((DataSet1.StockQuotesRow)subscription.QuoteObject);

                    if (m_nativeUtilities.IsConnected)
                    {
                        m_nativeUtilities.Unsubscribe(new string[] { subscription.Symbol });
                    }
                    else if (m_messageUtilities.IsSubscriberStarted)
                    {
                        m_messageUtilities.Unsubscribe(subscription.Symbol);
                    }
                    break;
                }
            }
        }

        private void buttonDeleteOption_Click(object sender, EventArgs e)
        {
            foreach (ISubscriptionId subscription in m_subscriptions)
            {
                if (subscription.Symbol == textBoxOption.Text)
                {
                    m_subscriptions.Remove(subscription);
                    dataSet1.StockQuotes.Rows.Remove((DataSet1.StockQuotesRow)subscription.QuoteObject);

                    if (m_nativeUtilities.IsConnected)
                    {
                        m_nativeUtilities.Unsubscribe(new string[] { subscription.Symbol });
                    }
                    else if (m_messageUtilities.IsSubscriberStarted)
                    {
                        m_messageUtilities.Unsubscribe(subscription.Symbol);
                    }
                    break;
                }
            }
        }

        private void buttonAddOption_Click(object sender, EventArgs e)
        {
            DataSet1.StockQuotesRow row = dataSet1.StockQuotes.NewStockQuotesRow();
            row.Symbol = textBoxOption.Text;
            dataSet1.StockQuotes.AddStockQuotesRow(row);

            OptionSubscriptionId subscription = new OptionSubscriptionId(row.Symbol, (object)row);
            m_subscriptions.Add(subscription);

            if (m_nativeUtilities.IsConnected)
            {
                m_nativeUtilities.Subscribe(new OptionSubscriptionId[] { subscription });
            }
           else if (m_messageUtilities.IsSubscriberStarted)
           {
                m_messageUtilities.Subscribe(subscription.Symbol, QuoteType.Option, subscription.QuoteObject);
            }

        }

        private void buttonAddFuture_Click(object sender, EventArgs e)
        {
            DataSet1.StockQuotesRow row = dataSet1.StockQuotes.NewStockQuotesRow();
            row.Symbol = textBoxFuture.Text;
            dataSet1.StockQuotes.AddStockQuotesRow(row);

            FutureSubscriptionId subscription = new FutureSubscriptionId(row.Symbol, (object)row);
            m_subscriptions.Add(subscription);

            if (m_nativeUtilities.IsConnected)
            {
                m_nativeUtilities.Subscribe(new FutureSubscriptionId[] { subscription });
            }
            else if (m_messageUtilities.IsSubscriberStarted)
            {
                m_messageUtilities.Subscribe(subscription.Symbol, QuoteType.Future, subscription.QuoteObject);
            }


        }

        private void buttonDeleteFuture_Click(object sender, EventArgs e)
        {
            foreach (ISubscriptionId subscription in m_subscriptions)
            {
                if (subscription.Symbol == textBoxFuture.Text)
                {
                    m_subscriptions.Remove(subscription);
                    dataSet1.StockQuotes.Rows.Remove((DataSet1.StockQuotesRow)subscription.QuoteObject);

                    if (m_nativeUtilities.IsConnected)
                    {
                        m_nativeUtilities.Unsubscribe(new string[] { subscription.Symbol });
                    }
                    else if (m_messageUtilities.IsSubscriberStarted)
                    {
                        m_messageUtilities.Unsubscribe(subscription.Symbol);
                    }
                    break;
                }
            }

        }

        private void buttonAddIndex_Click(object sender, EventArgs e)
        {
            DataSet1.StockQuotesRow row = dataSet1.StockQuotes.NewStockQuotesRow();
            row.Symbol = textBoxIndex.Text;
            dataSet1.StockQuotes.AddStockQuotesRow(row);

            IndexSubscriptionId subscription = new IndexSubscriptionId(row.Symbol, (object)row);
            m_subscriptions.Add(subscription);

            if (m_nativeUtilities.IsConnected)
            {
                m_nativeUtilities.Subscribe(new IndexSubscriptionId[] { subscription });
            }
            else if (m_messageUtilities.IsSubscriberStarted)
            {
                m_messageUtilities.Subscribe(subscription.Symbol, QuoteType.Index, subscription.QuoteObject);
            }

        }

        private void buttonDeleteIndex_Click(object sender, EventArgs e)
        {
            foreach (ISubscriptionId subscription in m_subscriptions)
            {
                if (subscription.Symbol == textBoxIndex.Text)
                {
                    m_subscriptions.Remove(subscription);
                    dataSet1.StockQuotes.Rows.Remove((DataSet1.StockQuotesRow)subscription.QuoteObject);

                    if (m_nativeUtilities.IsConnected)
                    {
                        m_nativeUtilities.Unsubscribe(new string[] { subscription.Symbol });
                    }
                    else if (m_messageUtilities.IsSubscriberStarted)
                    {
                        m_messageUtilities.Unsubscribe(subscription.Symbol);
                    }
                    break;
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = String.Format("{0} {1}",
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
               System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
        }
    }
}
