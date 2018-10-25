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
        static private String _baseSearchQuery = "SELECT TOP 1000 Performer, Title, Duration, Resource, Name as \"Category\" FROM Media JOIN Path ON Path.Media = Media.IMedia " +
                "JOIN Belong ON Belong.Media = Media.IMedia JOIN Category ON Category.ICategory = Belong.Category ";

        static public Boolean connect()
        {
            String conn = String.Format("user id={0};" +
                                       "password={1};server={2};" +
                                       "MultipleActiveResultSets = true;" +
                                       //"Trusted_Connection=yes;" +
                                       "database={3}; " +
                                       "connection timeout=10", Config.DBUser, Config.DBPass, Config.DBHost, Config.DB);
            _db = new SqlConnection(conn);

            try
            {
                _db.Open();
                Debug.WriteLine("DB open");
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
            catch (Exception)
            {

            }
        }

        static public String getAudioPath(String folder)
        {
            SqlDataReader myReader = null;
            String query = "SELECT Folder FROM dbo.Directory WHERE IDirectory = " + folder + ";";
            SqlCommand myCommand = new SqlCommand(query, _db);

            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                return (String)myReader["Folder"];
            }

            return "";
        }

        static public int getLastId(String table, String IdName)
        {
            SqlDataReader myReader = null;
            String query = "SELECT TOP 1 " + IdName + " FROM " + table + " ORDER BY " + IdName + " DESC;";
            SqlCommand myCommand = new SqlCommand(query, _db);

            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                return Convert.ToInt32(myReader[IdName]);
            }
            myReader.Close();
            myCommand.Dispose();

            return 0;
        }

        static public Dictionary<String, String> getCategory()
        {
            Dictionary<String, String> cats = new Dictionary<string, string>();

            SqlDataReader myReader = null;
            String query = "SELECT ICategory, Container, Name FROM Category;";
            SqlCommand myCommand = new SqlCommand(query, _db);

            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                cats.Add(myReader["ICategory"].ToString(), myReader["Name"].ToString());
            }

            return cats;
        }

        static public List<Music> GetMusics(int cat)
        {
            List<Music> musics = new List<Music>();

            String query = _baseSearchQuery;
            if (cat >= 1)
            {
                query += "WHERE ICategory = " + cat + " ";
            }
            query += "ORDER BY IMedia DESC;";

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(query, _db);

            myReader = myCommand.ExecuteReader();

            getValues(myReader, musics);

            return musics;


        }

        static public List<Music> GetMusics(String search)
        {
            List<Music> musics = new List<Music>();

            String query = _baseSearchQuery;

            if(search != "")
            {
                query += "WHERE Performer LIKE '%"+search+"%' OR Title LIKE '%"+search+"%' ";
            }

            query += "ORDER BY IMedia DESC;";

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(query, _db);

            myReader = myCommand.ExecuteReader();

            getValues(myReader, musics);

            return musics;
        }

        static private void getValues(SqlDataReader reader, List<Music> list)
        {
            while (reader.Read())
            {
                object[] values = new object[5];
                values[0] = reader["Performer"];
                values[1] = reader["Title"];
                values[2] = reader["Duration"];
                values[3] = reader["Resource"];
                values[4] = reader["Category"];
                Music tmp = new Music(values);
                list.Add(tmp);
            }
        }

        static public List<object[]> getCategories(int group)
        {
            List<object[]> categories = new List<object[]>();
            String query = "SELECT Name, ICategory, Position, Container as Cont FROM Category " +
                "WHERE Container = " + group + " ORDER BY Position";
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(query, _db);

            myReader = myCommand.ExecuteReader();

            while (myReader.Read())
            {
                object[] values = new object[4];
                values[0] = myReader["Name"];
                values[1] = myReader["ICategory"];
                values[2] = myReader["Position"];
                values[3] = myReader["Cont"];

                categories.Add(values);
            }

            return categories;
        }

        static public Dictionary<int, string> getCatGroup()
        {
            Dictionary<int, string> group = new Dictionary<int, string>();
            String query = "SELECT Name, IContainer FROM Container";
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(query, _db);

            myReader = myCommand.ExecuteReader();

            while (myReader.Read())
            {
                group.Add(Convert.ToInt32(myReader["IContainer"]),myReader["Name"].ToString());
            }

            return group;
        }
    }
}
