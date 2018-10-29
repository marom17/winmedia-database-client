using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Winmedia_Database_Client
{
    class DBHelper
    {
        static private SqlConnection _db;
        static private String _baseSearchQuery = "SELECT TOP 1000 Performer, Title, Duration, Resource, Name as \"Category\", Category as CatId, Start, Stop,"+
            " Introin, Introout, Fadein,  Fadeout, Jingle, JinglePosition, JingleVolume, Stretch, IMedia FROM Media JOIN Path ON Path.Media = Media.IMedia " +
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

            myReader.Close();
            myCommand.Dispose();

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

            myReader.Close();
            myCommand.Dispose();

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

            myReader.Close();
            myCommand.Dispose();

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

            myReader.Close();
            myCommand.Dispose();

            return musics;
        }

        static private void getValues(SqlDataReader reader, List<Music> list)
        {
            while (reader.Read())
            {
                list.Add(musicInfo(reader));
            }
        }

        static private Music musicInfo(SqlDataReader reader)
        {
            object[] values = new object[17];
            values[0] = reader["Performer"];
            values[1] = reader["Title"];
            values[2] = reader["Duration"];
            values[3] = reader["Resource"];
            values[4] = reader["Category"];
            values[5] = reader["CatId"];
            values[6] = reader["Start"];
            values[7] = reader["Stop"];
            values[8] = reader["Introin"];
            values[9] = reader["Introout"];
            values[10] = reader["Fadein"];
            values[11] = reader["Fadeout"];
            values[12] = reader["Jingle"];
            values[13] = reader["JinglePosition"];
            values[14] = reader["JingleVolume"];
            values[15] = reader["Stretch"];
            values[16] = reader["IMedia"];
            Music tmp = new Music(values);

            return tmp;

        }

        static private Music singleMusic(int id)
        {
            String query = _baseSearchQuery;
            query += "WHERE IMedia = " + id + ";";
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(query, _db);

            myReader = myCommand.ExecuteReader();
            myReader.Read();

            Music tmp = musicInfo(myReader);

            myReader.Close();
            myCommand.Dispose();

            return tmp;
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

            myReader.Close();
            myCommand.Dispose();

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

            myReader.Close();
            myCommand.Dispose();
            return group;
        }

        static public int getPlaylistId(DateTime date, String hour)
        {
            String query = "SELECT IPlaylist, Day FROM Playlist JOIN Slot ON ISlot = Slot WHERE Day = CONVERT(datetime,'" + date.ToString("yyyy-MM-dd HH:mm:ss") + "') AND Name = '" + hour + "' AND Computer = 6;";

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(query, _db);

            int idPlaylist = 0;
            try
            {
                myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    idPlaylist = Convert.ToInt32(myReader["IPlaylist"]);
                }

                myReader.Close();
                myCommand.Dispose();
            }
            catch (Exception) { }
            return idPlaylist;
        }

        static public List<PlaylistElement> getPlaylist(int idPlaylist)
        {
            List<PlaylistElement> pElements = new List<PlaylistElement>();

            try
            {
                if (idPlaylist > 0)
                {
                    String query = "SELECT * FROM Content WHERE Playlist = " + idPlaylist + ";";
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(query, _db);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        int id = Convert.ToInt32(myReader["Media"]);
                        Music mus = singleMusic(id);
                        mus.Stretch = Convert.ToDouble(myReader["Stretch"]);
                        PlaylistElement tmp = new PlaylistElement(mus);


                        pElements.Add(tmp);
                    }

                    myReader.Close();
                    myCommand.Dispose();
                    ShareVar.IdPlaylist = idPlaylist;
                }
                else
                {
                    ShareVar.IdPlaylist = 0;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return pElements;
        }

        public static void savePlaylist(ItemCollection playlist)
        {
            String query = "DELETE FROM Content WHERE Playlist = " + ShareVar.IdPlaylist + ";";

            SqlTransaction transaction = _db.BeginTransaction();


            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand(query, _db,transaction);
                myReader = myCommand.ExecuteReader();
                myCommand.Dispose();
                myReader.Close();

                foreach(ListViewItem element in playlist)
                {
                    Music tmp = ((PlaylistElement)element.Content).Music;
                    query = "INSERT INTO Content (Playlist,Media,Category,Duration,Start,Stop,Fadein,Fadeout,Alphain,Alphaout,Stretch,Jingle,JinglePosition,JingleVolume) " +
                    "VALUES(@playlist,@media,@category,@duration,@start,@stop,@fadein,@fadeout,@fadein,@fadeout,@stretch,@jingle,@jinglePosition,@jingleVolume);";
                    SqlCommand cmd = new SqlCommand(query, _db, transaction);

                    cmd.Parameters.Add("@playlist", SqlDbType.Int);
                    cmd.Parameters["@playlist"].Value = ShareVar.IdPlaylist;
                    cmd.Parameters.Add("@media", SqlDbType.Int);
                    cmd.Parameters["@media"].Value = tmp.MediaId;
                    cmd.Parameters.Add("@category", SqlDbType.Int);
                    cmd.Parameters["@category"].Value = tmp.Catid;
                    cmd.Parameters.Add("@duration", SqlDbType.Int);
                    cmd.Parameters["@duration"].Value = tmp.TimeLength;
                    cmd.Parameters.Add("@start", SqlDbType.Int);
                    cmd.Parameters["@start"].Value = tmp.Start;
                    cmd.Parameters.Add("@stop", SqlDbType.Int);
                    cmd.Parameters["@stop"].Value = tmp.Stop;
                    cmd.Parameters.Add("@fadein", SqlDbType.Int);
                    cmd.Parameters["@fadein"].Value = tmp.Fadein;
                    cmd.Parameters.Add("@fadeout", SqlDbType.Int);
                    cmd.Parameters["@fadeout"].Value = tmp.Fadeout;
                    cmd.Parameters.Add("@stretch", SqlDbType.Real);
                    cmd.Parameters["@stretch"].Value = tmp.Stretch;
                    cmd.Parameters.Add("@jingle", SqlDbType.Int);
                    cmd.Parameters["@jingle"].Value = tmp.Jingle;
                    cmd.Parameters.Add("@jinglePosition", SqlDbType.Int);
                    cmd.Parameters["@jinglePosition"].Value = tmp.Jingleposition;
                    cmd.Parameters.Add("@jingleVolume", SqlDbType.Real);
                    cmd.Parameters["@jingleVolume"].Value = tmp.JingleVolume;          

                    SqlDataReader reader = cmd.ExecuteReader();
                    cmd.Dispose();
                    reader.Close();
                }

                transaction.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                transaction.Rollback();
            }

        }
    }
}
