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
        public static string INSTANCE = "\\sqlexpress";
        //public static string INSTANCE = "";

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

            builder["Data Source"] = Settings.Default.DBServerName;
            builder["integrated Security"] = true;
            builder["Initial Catalog"] = DAL.Properties.Settings.Default.DatabaseName;
            if (DAL.Properties.Settings.Default.DatabaseUserID != "" && DAL.Properties.Settings.Default.DatabasePwd != "")
            {
                builder.UserID = DAL.Properties.Settings.Default.DatabaseUserID;
                builder.Password = DAL.Properties.Settings.Default.DatabasePwd;
            }
            builder.Pooling = false;
            
            //builder.
            Console.WriteLine(builder.ConnectionString);
            return builder.ConnectionString;
        }
        public static string BuildStringConnectionForTest(string DBServerName, string DatabaseName, string DatabaseUserID, string DatabasePwd)
        {

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder["Data Source"] = DBServerName + INSTANCE;
            builder["integrated Security"] = true;
            builder["Initial Catalog"] = DatabaseName;
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
        public static void UpdateSetting(string DBServerName,string DatabaseName,string DatabaseUserID,string DatabasePwd)
        {
            if (DBServerName != null)
            {
                DAL.Properties.Settings.Default.DBServerName = DBServerName + INSTANCE;
            }
            if(DatabaseName != null)
            {
                DAL.Properties.Settings.Default.DatabaseName = DatabaseName;
            }
            if (DatabaseUserID != null)
            {
                DAL.Properties.Settings.Default.DatabaseUserID = DatabaseUserID;
            }
            if (DatabasePwd != null)
            {
                DAL.Properties.Settings.Default.DatabasePwd = DatabasePwd;
            }
            DAL.Properties.Settings.Default.Save();
        }
    }
}
