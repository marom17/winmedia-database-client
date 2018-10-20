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

        static public Boolean connect()
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
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return false;
            }
        }

        static public SqlConnection db()
        {
            return _db;
        }

        static public void disconnect()
        {
            try
            {
                _db.Close();
            }
            catch(Exception)
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
            String conn = String.Format("user id={0};" +
                                       "password={1};server={2};" +
                                       //"Trusted_Connection=yes;" +
                                       "database={3}; " +
                                       "connection timeout=30", Config.DBUser, Config.DBPass, Config.DBHost, Config.DB);
            SqlConnection sql = new SqlConnection(conn);
            sql.Open();

            SqlDataReader myReader = null;
            String query = "SELECT TOP 1 " + IdName + " FROM " + table + " ORDER BY " + IdName + " DESC;";
            SqlCommand myCommand = new SqlCommand(query,sql);

            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                return Convert.ToInt32(myReader[IdName]);
            }
            myReader.Close();
            myCommand.Dispose();
            sql.Close();

            return 0;
        }
    }
}
