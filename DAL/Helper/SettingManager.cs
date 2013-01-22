using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using DAL.Properties;

namespace DAL.Helper
{
    public class SettingManager
    {
        //public static string INSTANCE = "\\sqlexpress";
        public static string INSTANCE = "";

        public void UpdateSetting(string keyName, string keyValue)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            foreach (XmlElement xmlElement in xmlDoc.DocumentElement)
            {
                if (xmlElement.Name == "appSettings")
                {
                    foreach (XmlNode xmlNode in xmlElement.ChildNodes)
                    {
                        if (xmlNode.Name == keyName)
                            xmlNode.Value = keyValue;
                    }
                }
            }
        }

        public static string BuildStringConnection()
        {            
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.NetworkLibrary = ConnectionString.NetworkLibrary;
            builder.DataSource = ConnectionString.DBServerName + "," + ConnectionString.Port;
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = ConnectionString.DatabaseName;
            if (!string.IsNullOrEmpty(ConnectionString.DatabaseUserID) && !string.IsNullOrEmpty(ConnectionString.DatabasePwd))
            {
                builder.UserID = ConnectionString.DatabaseUserID;
                builder.Password = ConnectionString.DatabasePwd;
            }
            builder.Pooling = false;
            
            //builder.
            Console.WriteLine(builder.ConnectionString);
            return builder.ConnectionString;
        }

        public static string BuildStringConnectionForTest(string DBServerName, string Port, string NetworkLibrary,
            string DatabaseName, string DatabaseUserID, string DatabasePwd)
        {

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.NetworkLibrary = NetworkLibrary;
            builder.DataSource = DBServerName + "," + Port;
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = DatabaseName;
            if (DatabaseUserID != "" && DatabasePwd != "")
            {
                builder.UserID = DatabaseUserID;
                builder.Password = DatabasePwd;
            }
            builder.Pooling = false;

            //builder.
            Console.WriteLine(builder.ConnectionString);
            return builder.ConnectionString;
        }

        public static void UpdateSetting(string DBServerName, string Port, string NetworkLibrary,
            string DatabaseName, string DatabaseUserID, string DatabasePwd)
        {
            if (DBServerName != null)
            {
                ConnectionString.DBServerName = DBServerName;
            }
            if (Port != null)
            {
                ConnectionString.Port = Port;
            }
            if (NetworkLibrary != null)
            {
                ConnectionString.NetworkLibrary = NetworkLibrary;
            }
            if (DatabaseName != null)
            {
                ConnectionString.DatabaseName = DatabaseName;
            }
            if (DatabaseUserID != null)
            {
                ConnectionString.DatabaseUserID = DatabaseUserID;
            }
            if (DatabasePwd != null)
            {
                ConnectionString.DatabasePwd = DatabasePwd;
            }
        }
    }
}
