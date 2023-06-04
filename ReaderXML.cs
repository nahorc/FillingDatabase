using System.Xml;
using System.Xml.Linq;

namespace FillingDatabase
{
    internal static class ReaderXML
    {
        private static void Repair()
        {
            XDocument xdoc = new XDocument(new XElement("connection",
                   new XElement("host", "localhost"),
                   new XElement("username", "postgres"),
                   new XElement("password", "321323"),
                   new XElement("database", "postgres")
                   )
               );
            xdoc.Save("connection.xml");
        }

        internal static Boolean Write(Dictionary<String, String> connectionData)
        {
            try
            {
                XDocument xdoc = new XDocument(new XElement("connection",
                       new XElement("host", connectionData["host"]),
                       new XElement("username", connectionData["username"]),
                       new XElement("password", connectionData["password"]),
                       new XElement("database", connectionData["database"])
                       )
                   );
                xdoc.Save("connection.xml");
            }
            catch
            {
                return false;
            }
            return true;
        }

        internal static Dictionary<String, String> GetData()
        {
            Dictionary<String, String> connectionData = new Dictionary<String, String>
            {
                { "host", "" },
                { "username", "" },
                { "password", "" },
                { "database", "" }
            };
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("connection.xml");

                XmlElement? xRoot = xDoc.DocumentElement;
                if (xRoot != null )
                {
                    foreach (XmlElement xnode in xRoot)
                    {
                        connectionData[xnode.Name] = xnode.InnerText;
                    }
                } 
                else
                {
                    Repair();
                }
            }
            catch 
            {
                Repair();
            }
            return connectionData;
        }
    }
}
