using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using Bloomberglp.Blpapi;

namespace BloombergLib
{
    public delegate void BloombergMessageEventHandler(object sender, BloombergMessageEventArgs e);

    public class BloombergMessageEventArgs : EventArgs
    {
        public BloombergMessageEventArgs(Message message)
        {
            Message = message;
        }

        public Message Message { get; private set; }
        public ISubscriptionId SubscriptionId { get { return Message.CorrelationID.Object as ISubscriptionId; } }

        public string MessageToXml()
        {
            string currentElementName = null;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", null, null));
                XmlElement rootNode = xmlDoc.CreateElement("MESSAGE");
                xmlDoc.AppendChild(rootNode);

                XmlElement fieldNode = xmlDoc.CreateElement("Time");
                fieldNode.AppendChild(xmlDoc.CreateTextNode(DateTime.Now.ToString("T")));
                rootNode.AppendChild(fieldNode);

                string correlationId = Message.CorrelationID.ToString();
                fieldNode = xmlDoc.CreateElement("SubscriptionId");
                fieldNode.AppendChild(xmlDoc.CreateTextNode(SubscriptionId.ToString()));
                rootNode.AppendChild(fieldNode);

                fieldNode = xmlDoc.CreateElement("MessageType");
                fieldNode.AppendChild(xmlDoc.CreateTextNode(Message.MessageType.ToString()));
                rootNode.AppendChild(fieldNode);

                foreach (Element element in Message.Elements)
                {
                      if (!element.IsNull)
                    {
                        if ((element.NumValues > 0) && !element.IsComplexType)
                        {
                            currentElementName = element.Name.ToString().Replace(" ", "").Replace("/", "").Replace("'", "").Replace("&", "n").Replace("-", "").Replace("%", "Pct").Replace("#", "");

                            fieldNode = xmlDoc.CreateElement(currentElementName);
                            fieldNode.AppendChild(xmlDoc.CreateTextNode(element.GetValueAsString()));
                            rootNode.AppendChild(fieldNode);
                        }
                    }
                }

                using (StringWriter stringWriter = new StringWriter())
                {
                    XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
                    xmlDoc.WriteTo(xmlTextWriter);
                    return stringWriter.ToString();
                }
            }
            catch(Exception ex)
            {
                if (currentElementName == null)
                    throw ex;
                else
                {
                    throw new ApplicationException("Error parsing field " + currentElementName);
                }
            }
        }
    }

}
