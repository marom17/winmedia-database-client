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
    class AudioDB
    {
        private static String _importMusic = "INSERT INTO dbo.Media "+
            "VALUES (@id,@performer,@title,'',0,@duration,@start,@stop,@trimin,@trimout,0,0,0,0,@cutin,@cutout,@loopin,@loopout,@introin,@introout,@hookin,@hookout,0,0,1,1,1,0,1,0,-3,"
            +"'','','','','','','','','','','','',3,GETDATE(),'no','Aucune','','');";

        private static String _importPath = "INSERT INTO dbo.Path VALUES (@id,@media,0,2,@path,@length,0,0,1,2,2,384000,2,2,48000,1,1,0,0,0,0,0,@size,0,GETDATE());";
        public static void ImportAudio(Music file)
        {
            DBHelper.connect();
            var mediaId = DBHelper.getLastId("dbo.Media", "IMedia") + 1;
            DBHelper.disconnect();
            
            SqlCommand command = new SqlCommand(_importMusic, DBHelper.connect());
            command.Parameters.Add("@id", SqlDbType.Int);
            command.Parameters["@id"].Value = mediaId;
            command.Parameters.Add("@performer", SqlDbType.NVarChar);
            command.Parameters["@performer"].Value = file.Artist;
            command.Parameters.Add("@title", SqlDbType.NVarChar);
            command.Parameters["@title"].Value = file.Title;
            command.Parameters.Add("@duration", SqlDbType.Int);
            command.Parameters["@duration"].Value = file.Duration;
            command.Parameters.Add("@start", SqlDbType.Int);
            command.Parameters["@start"].Value = file.Start;
            command.Parameters.Add("@stop", SqlDbType.Int);
            command.Parameters["@stop"].Value = file.Stop;
            command.Parameters.Add("@trimin", SqlDbType.Int);
            command.Parameters["@trimin"].Value = file.Trimin;
            command.Parameters.Add("@trimout", SqlDbType.Int);
            command.Parameters["@trimout"].Value = file.Trimout;
            command.Parameters.Add("@cutin", SqlDbType.Int);
            command.Parameters["@cutin"].Value = file.Start;
            command.Parameters.Add("@cutout", SqlDbType.Int);
            command.Parameters["@cutout"].Value = file.Cutout;
            command.Parameters.Add("@loopin", SqlDbType.Int);
            command.Parameters["@loopin"].Value = file.Start;
            command.Parameters.Add("@loopout", SqlDbType.Int);
            command.Parameters["@loopout"].Value = file.Start;
            command.Parameters.Add("@introin", SqlDbType.Int);
            command.Parameters["@introin"].Value = file.Start;
            command.Parameters.Add("@introout", SqlDbType.Int);
            command.Parameters["@introout"].Value = file.Intro;
            command.Parameters.Add("@hookin", SqlDbType.Int);
            command.Parameters["@hookin"].Value = file.Start;
            command.Parameters.Add("@hookout", SqlDbType.Int);
            command.Parameters["@hookout"].Value = file.Start;

            try
            {
                Int32 rowAffected = command.ExecuteNonQuery();
                DBHelper.disconnect();
                DBHelper.connect();
                var pathId = DBHelper.getLastId("dbo.Path", "IPath") + 1;
                DBHelper.disconnect();

                command = new SqlCommand(_importPath, DBHelper.connect());
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = pathId;
                command.Parameters.Add("@media", SqlDbType.Int);
                command.Parameters["@media"].Value = mediaId;
                command.Parameters.Add("@path", SqlDbType.NVarChar);
                command.Parameters["@path"].Value = file.FilePath;
                //command.Parameters["@path"].Value = file.FileName;
                command.Parameters.Add("@length", SqlDbType.Int);
                command.Parameters["@length"].Value = file.Duration;
                command.Parameters.Add("@size", SqlDbType.Int);
                command.Parameters["@size"].Value = file.FileLength;
                rowAffected = command.ExecuteNonQuery();
                DBHelper.disconnect();



            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
