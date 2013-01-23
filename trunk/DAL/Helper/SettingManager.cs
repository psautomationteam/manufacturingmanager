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
        public static void CheckRegistry()
        {
            //RemoveRegistry();
            ModifyRegistry reg = new ModifyRegistry();
            if(string.IsNullOrEmpty(reg.Read("DBServerName")))
                UpdateRegistry(Constant.INIT_IP, Constant.INIT_PORT, Constant.INIT_NETWORK_LIBRARY,
                    Constant.INIT_DATABASE_NAME, Constant.INIT_USER_ID, Constant.INIT_PW);
        }

        public static string BuildStringConnection()
        {
            ModifyRegistry reg = new ModifyRegistry();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.NetworkLibrary = reg.Read("NetworkLibrary");
            builder.DataSource = reg.Read("DBServerName") + "," + reg.Read("Port");
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = reg.Read("DatabaseName");
            if (!string.IsNullOrEmpty(reg.Read("DatabaseUserID")) && 
                !string.IsNullOrEmpty(reg.Read("DatabasePwd")))
            {
                builder.UserID = reg.Read("DatabaseUserID");
                builder.Password = reg.Read("DatabasePwd");
            }
            builder.Pooling = false;
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
            Console.WriteLine(builder.ConnectionString);
            return builder.ConnectionString;
        }

        public static void UpdateRegistry(string DBServerName, string Port, string NetworkLibrary,
            string DatabaseName, string DatabaseUserID, string DatabasePwd)
        {
            ModifyRegistry reg = new ModifyRegistry();
            if (DBServerName != null)
            {
                reg.Write("DBServerName", DBServerName);
            }
            if (DBServerName != null)
            {
                reg.Write("Port", Port);
            }
            if (DBServerName != null)
            {
                reg.Write("NetworkLibrary", NetworkLibrary);
            }
            if (DBServerName != null)
            {
                reg.Write("DatabaseName", DatabaseName);
            }
            if (DBServerName != null)
            {
                reg.Write("DatabaseUserID", DatabaseUserID);
            }
            if (DBServerName != null)
            {
                reg.Write("DatabasePwd", DatabasePwd);
            }
        }

        private static void RemoveRegistry()
        {
            ModifyRegistry reg = new ModifyRegistry();
            reg.DeleteSubKeyTree();
        }
    }
}
