using System;
using System.Collections.Generic;
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
            _db.Close();
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
    }
}
