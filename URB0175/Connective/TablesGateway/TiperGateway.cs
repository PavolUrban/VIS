using Connective.Abstract.Interface;
using Connective.Conn;
using Connective.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connective.TablesGateway
{
    public class TiperGateway<T> : TiperGatewayInterface<T>
        where T : Tiper
    {
        private static TiperGateway<Tiper> instance;
        private TiperGateway() { }

        public static TiperGateway<Tiper> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TiperGateway<Tiper>();
                }
                return instance;
            }
        }




        public String SQL_SELECT = "SELECT * FROM \"Tiper\"";
        public String SQL_SELECT_ID = "SELECT * FROM \"Tiper\" WHERE id_tipera=@id";
        public String SQL_INSERT = "INSERT INTO \"Tiper\" VALUES (@meno, @priezvisko, @datumNar, @email, @datumReg, @pohlavie, @stavUctu, @heslo)";
        public String SQL_DELETE_ID = "DELETE FROM \"Tiper\" WHERE id_tipera=@id";
        public String SQL_UPDATE = "UPDATE \"Tiper\" SET meno=@meno, priezvisko=@priezvisko, datum_narodenia=@datumNar, email=@email, datum_registracie = @datumReg, pohlavie=@pohlavie, stav_uctu=@stavUctu, heslo=@heslo WHERE id_tipera=@id";

        public String SQL_SELECTZNT = "SELECT * FROM ZapasyNaTikete znt join tikety t ON t.id_tiketu = znt.Tikety_id_Tiketu join tiper tp On tp.id_tipera = t.id_tipera AND t.id_tipera = @idTipera";
        public String SQL_CHECK_PASSWORD = "SELECT DISTINCT* FROM Tiper WHERE email=@email AND heslo=@heslo";



        public int Insert(T t)
        {
            Database db = new Database();
           
                db.Connect();
           
            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, (Tiper)t);
            int ret = db.ExecuteNonQuery(command);

            
                db.Close();
            
            return ret;
        }

        public int Update(T t)
        {
            Database db = new Database();
            
                db.Connect();
            
            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommandUpdate(command, (Tiper)t);
            int ret = db.ExecuteNonQuery(command);
            
                db.Close();
            
            return ret;
        }

        public Collection<T> Select()
        {
            Database db = new Database();
            
                db.Connect();
            
            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);

            Collection<T> tiperi = Read(reader);
            
                db.Close();
            

            return tiperi;
        }

        public T Select(int id)
        {
            Database db = new Database();
            
                db.Connect();
            
            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = db.Select(command);

            Collection<T> tiperi = Read(reader);
            Tiper tiper = null;
            if (tiperi.Count == 1)
            {
                tiper = tiperi[0];
            }
            

                db.Close();
            

            return (T)tiper;
        }



        public int Delete(int id)
        {
            Database db = new Database();
            
                db.Connect();
            


            SqlCommand command = db.CreateCommand(SQL_DELETE_ID);

            command.Parameters.AddWithValue("@id", id);
            int ret = db.ExecuteNonQuery(command);

           
                db.Close();
            

            return ret;
        }


        private void PrepareCommand(SqlCommand command, Tiper tiper)
        {
            command.Parameters.AddWithValue("@meno", tiper.meno);
            command.Parameters.AddWithValue("@priezvisko", tiper.priezvisko);
            command.Parameters.AddWithValue("@datumNar", tiper.datum_narodenia);
            command.Parameters.AddWithValue("@email", tiper.email);
            command.Parameters.AddWithValue("@datumReg", tiper.datum_registracie);
            command.Parameters.AddWithValue("@pohlavie", tiper.pohlavie);
            command.Parameters.AddWithValue("@stavUctu", tiper.stav_uctu.HasValue ? (object)tiper.stav_uctu.Value : DBNull.Value);
            command.Parameters.AddWithValue("@heslo", tiper.heslo!=null ? (object)tiper.heslo : DBNull.Value);
        }

        private void PrepareCommandUpdate(SqlCommand command, Tiper tiper)
        {

            command.Parameters.AddWithValue("@id", tiper.RecordId);
            command.Parameters.AddWithValue("@meno", tiper.meno);
            command.Parameters.AddWithValue("@priezvisko", tiper.priezvisko);
            command.Parameters.AddWithValue("@datumNar", tiper.datum_narodenia);
            command.Parameters.AddWithValue("@email", tiper.email);
            command.Parameters.AddWithValue("@datumReg", tiper.datum_registracie);
            command.Parameters.AddWithValue("@pohlavie", tiper.pohlavie);
            command.Parameters.AddWithValue("@stavUctu", tiper.stav_uctu.HasValue ? (object)tiper.stav_uctu.Value : DBNull.Value);
            command.Parameters.AddWithValue("@heslo", tiper.heslo != null ? (object)tiper.heslo : DBNull.Value);
        }


        private Collection<T> Read(SqlDataReader reader)
        {
            Collection<T> tiperi = new Collection<T>();

            while (reader.Read())
            {
                int i = -1;
                Tiper tiper = new Tiper();
                tiper.RecordId = reader.GetInt32(++i);
                tiper.meno = reader.GetString(++i);
                tiper.priezvisko = reader.GetString(++i);
                tiper.datum_narodenia = reader.GetDateTime(++i);
                tiper.email = reader.GetString(++i);
                tiper.datum_registracie = reader.GetDateTime(++i);
                tiper.pohlavie = reader.GetString(++i);

                if (!reader.IsDBNull(++i))
                {
                    tiper.stav_uctu = reader.GetDouble(i);
                }

                if (!reader.IsDBNull(++i))
                {
                    tiper.heslo= reader.GetString(i);
                }

                tiperi.Add((T)tiper);

            }
            return tiperi;
        }


        public void narodeninovyBonus()
        {
            Database db = new Database();
         
                db.Connect();
          

            // 1.  create a command object identifying the stored procedure
            SqlCommand command = db.CreateCommand("narodeninovyBonus");

            // 2. set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            // 4. execute procedure
            int ret = db.ExecuteNonQuery(command);

            Console.WriteLine(ret);


                db.Close();
          

        }


        public void upozornenieNaNeaktivitu()
        {
            Database db = new Database();
            
                db.Connect();
            
            // 1.  create a command object identifying the stored procedure
            SqlCommand command = db.CreateCommand("upozornenieNaNeaktivitu");

            // 2. set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            // 4. execute procedure
            int ret = db.ExecuteNonQuery(command);

            Console.WriteLine(ret);

            
                db.Close();
            

        }


        public bool CheckPassword(T tiper)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_CHECK_PASSWORD);
            PrepareCommand(command, tiper);

            SqlDataReader reader = db.Select(command);

            Collection<T> tiperi = Read(reader);
            db.Close();

            if (tiperi.Count > 0)
            {
                return true;
            }
            return false;
        }


    }
}
