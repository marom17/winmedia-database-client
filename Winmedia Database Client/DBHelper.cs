using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winmedia_Database_Client
{
    class DBHelper
    {
        static private SqlConnection _db;

        static public SqlConnection connect()
        {
            String conn = String.Format("user id={0};" +
                                       "password={1};server={2};" +
                                       //"Trusted_Connection=yes;" +
                                       "database={3}; " +
                                       "connection timeout=30", Config.DBUser, Config.DBPass, Config.DBHost, Config.DB);
            _db = new SqlConnection(conn);

            try
            {
                _db.Open();
                return _db;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return null;
            }
        }

        static public void disconnect()
        {
            try
            {
                _db.Close();
            }
            catch(Exception e)
            {

            }
        }

        static public void getData()
        {
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select * from dbo.Account",
                                                         _db);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    Debug.WriteLine(myReader["Name"] + "/" + myReader["Password"]);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        static public int getLastId(String table, String IdName)
        {
            SqlDataReader myReader = null;
            String query = "SELECT TOP 1 " + IdName + " FROM " + table + " ORDER BY " + IdName + " DESC;";
            SqlCommand myCommand = new SqlCommand(query,_db);

            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                return Convert.ToInt32(myReader[IdName]);
            }

            return 0;
        }
    }
}
