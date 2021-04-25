using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.DataAccess;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        //public static can not be instantiated, everyone can access content
        public static IDataConnection Connection { get; private set; } 
        //private koyarak sadece classın icinde degeri değiştirilebilir yaptık
        //list olarak oluşturduk çünkü hem sqle hem texte kaydedebiliriz

        /* 
         Connections=new List<IDataConnection>();
         */


        public static void InitializeConnections(DatabaseType db)
        {
            if (db == DatabaseType.Sql)
            {
                SqlConnector sql = new SqlConnector();
                Connection=sql;
                //TODO Create SQL connection
              
            }
            else if (db ==DatabaseType.TextFile)
            {
                TextConnector text = new TextConnector();
                Connection=text;
                //TODO Create Text Connection
            }
        }

        public static string CnnString(string name)
        {
            //references altına assembly kısmına system.configuratiın eklenmeli

            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
