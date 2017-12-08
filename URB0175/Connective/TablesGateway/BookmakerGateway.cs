using Connective.Conn;
using Connective.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connective.TablesGateway
{
    public class BookmakerGateway
    {
        public static String SQL_SELECT = "SELECT * FROM \"Bookmaker\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"Bookmaker\" WHERE id_bookmakera=@id";
        public static String SQL_INSERT = "INSERT INTO \"Bookmaker\" VALUES (@meno, @priezvisko, @datumNar, @email, @nastup, @pohlavie)";
        public static String SQL_DELETE_ID = "DELETE FROM \"Bookmaker\" WHERE id_bookmakera=@id";
        public static String SQL_UPDATE = "UPDATE \"Bookmaker\" SET meno=@meno, priezvisko=@priezvisko, datum_narodenia=@datumNar, email=@email, datum_nastupu_do_prace = @nastup, pohlavie=@pohlavie, heslo=@heslo WHERE id_bookmakera=@id";
        public static String SQL_CHECK_LOGIN = "SELECT DISTINCT * FROM Bookmaker WHERE email=@email AND heslo=@heslo";
        public static String SQL_SELECT_PROFILE = "SELECT meno, priezvisko, email FROM Bookmaker where id_bookmakera=@id";






        public static int Insert(Bookmaker bookmaker, Database pDb = null)
        {
            Database db = new Database();
           
                db.Connect();
           

            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, bookmaker);
            int ret = db.ExecuteNonQuery(command);

        
                db.Close();
            

            return ret;
        }

        public static int Update(Bookmaker bookmaker)
        {
            Database db = new Database();
            
               
                db.Connect();
           

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommandUpdate(command, bookmaker);
            int ret = db.ExecuteNonQuery(command);

          
                db.Close();
          

            return ret;
        }

        public static Collection<Bookmaker> Select()
        {
            Database db = new Database();
          
                db.Connect();
         

            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);

            Collection<Bookmaker> bookmakeri = Read(reader);
            reader.Close();

          
                db.Close();
            

            return bookmakeri;
        }

        public static Bookmaker Select(int id)
        {
            Database db = new Database();
         
                db.Connect();


            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = db.Select(command);

            Collection<Bookmaker> bookmakeri = Read(reader);
            Bookmaker bookmaker = null;
            if (bookmakeri.Count == 1)
            {
                bookmaker = bookmakeri[0];
            }
            reader.Close();

           
                db.Close();
            

            return bookmaker;
        }







        public static Bookmaker Select2(int id)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT_PROFILE);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = db.Select(command);

            Collection<Bookmaker> bookmakeri = Read1(reader);
            Bookmaker bookmaker = null;
            if (bookmakeri.Count == 1)
            {
                bookmaker = bookmakeri[0];
            }
           
                db.Close();
            

            return bookmaker;
        }





        public static int Delete(int id)
        {
            Database db = new Database();
          
                db.Connect();
            
           
            SqlCommand command = db.CreateCommand(SQL_DELETE_ID);

            command.Parameters.AddWithValue("@id", id);
            int ret = db.ExecuteNonQuery(command);

                db.Close();
            
            return ret;
        }


        private static void PrepareCommand(SqlCommand command, Bookmaker bookmaker)
        {
            command.Parameters.AddWithValue("@meno", bookmaker.meno);
            command.Parameters.AddWithValue("@priezvisko", bookmaker.priezvisko);
            command.Parameters.AddWithValue("@datumNar", bookmaker.datum_narodenia);
            command.Parameters.AddWithValue("@email", bookmaker.email);
            command.Parameters.AddWithValue("@nastup", bookmaker.datum_nastupu_do_prace);
            command.Parameters.AddWithValue("@pohlavie", bookmaker.pohlavie);
            command.Parameters.AddWithValue("@heslo", bookmaker.heslo);

        }

      


        private static void PrepareCommandUpdate(SqlCommand command, Bookmaker bookmaker)
        {

            command.Parameters.AddWithValue("@id", bookmaker.RecordId);
            command.Parameters.AddWithValue("@meno", bookmaker.meno);
            command.Parameters.AddWithValue("@priezvisko", bookmaker.priezvisko);
            command.Parameters.AddWithValue("@datumNar", bookmaker.datum_narodenia);
            command.Parameters.AddWithValue("@email", bookmaker.email);
            command.Parameters.AddWithValue("@nastup", bookmaker.datum_nastupu_do_prace);
            command.Parameters.AddWithValue("@pohlavie", bookmaker.pohlavie);
        }


        private static Collection<Bookmaker> Read(SqlDataReader reader)
        {
            Collection<Bookmaker> bookmakeri = new Collection<Bookmaker>();

            while (reader.Read())
            {
                int i = -1;
                Bookmaker bookmaker = new Bookmaker();
                bookmaker.RecordId= reader.GetInt32(++i);
                bookmaker.meno = reader.GetString(++i);
                bookmaker.priezvisko = reader.GetString(++i);
                bookmaker.datum_narodenia = reader.GetDateTime(++i);
                bookmaker.email = reader.GetString(++i);
                bookmaker.datum_nastupu_do_prace = reader.GetDateTime(++i);
                bookmaker.pohlavie = reader.GetString(++i);
                bookmaker.heslo = reader.GetString(++i);


                bookmakeri.Add(bookmaker);

            }
            return bookmakeri;
        }

        private static Collection<Bookmaker> Read1(SqlDataReader reader)
        {
            Collection<Bookmaker> bookmakers = new Collection<Bookmaker>();

            while (reader.Read())
            {
                Bookmaker b = new Bookmaker();
                int i = -1;
                b.meno = reader.GetString(++i);
                b.priezvisko = reader.GetString(++i);
                b.email = reader.GetString(++i);

                bookmakers.Add(b);
            }
            return bookmakers;
        }



        public static bool CheckPassword(Bookmaker bookmaker)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_CHECK_LOGIN);
            PrepareCommand(command, bookmaker);

            SqlDataReader reader = db.Select(command);

            Collection<Bookmaker> bookmakers = Read(reader);
            db.Close();

            if (bookmakers.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
