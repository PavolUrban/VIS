using Connective.Abstract.Interface;
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
    public class TiketyGateway<T> : TiketyGatewayInterface<T>
        where T : Tikety
    {

        private static TiketyGateway<Tikety> instance;
        private TiketyGateway() { }

        public static TiketyGateway<Tikety> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TiketyGateway<Tikety>();
                }
                return instance;
            }
        }





        public String SQL_SELECT = "SELECT * FROM \"Tikety\"";
        public String SQL_SELECT_ID = "SELECT * FROM \"Tikety\" WHERE id_tiketu=@id";
        public String SQL_SELECT_TOP1 = "SELECT top 1 * FROM Tikety order by id_tiketu desc";
        public String SQL_INSERT = "INSERT INTO \"Tikety\" VALUES (@kod, @idTipera, @celkovyKurz, @vklad, @celkovaVyhra, @uspesnostTiketu)";
        public String SQL_DELETE_ID = "DELETE FROM \"Tikety\" WHERE id_tiketu=@id";
        public String SQL_UPDATE = "UPDATE \"Tikety\" SET kod_tiketu=@kod, id_tipera=@idTipera, celkovy_kurz=@celkovyKurz, vklad=@vklad, celkova_vyhra=@celkovaVyhra, uspesnost_tiketu=@uspesnostTiketu WHERE id_tiketu=@id";
        public String SQL_SELECT_TIPEROVEZAPASY = "select * from tikety where id_tipera = @id_tipera order by id_tiketu";

        public  int Insert(T t)
        {
            Database db = new Database();
          
                db.Connect();
          

            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, (Tikety)t);
            int ret = db.ExecuteNonQuery(command);

           
                db.Close();
            

            return ret;
        }

        public int Update(T t)
        {
            Database db = new Database();
          
                db.Connect();
           

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommandUpdate(command, (Tikety)t);
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

            Collection<T> tikety = Read(reader);
            

                db.Close();
            

            return tikety;
        }


        public Collection<T> SelectTiperove(int idTipera)
        {
            Database db = new Database();

            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT_TIPEROVEZAPASY);
            command.Parameters.AddWithValue("@id_tipera", idTipera);
            SqlDataReader reader = db.Select(command);

            Collection<T> tikety = Read(reader);


            db.Close();


            return tikety;
        }


        public Collection<T> SelectTOP1()
        {
            Database db = new Database();
           
                db.Connect();
           
            SqlCommand command = db.CreateCommand(SQL_SELECT_TOP1);
            SqlDataReader reader = db.Select(command);

            Collection<T> tikety = Read(reader);
            reader.Close();
            
                db.Close();
            
            return tikety;
        }

        public T Select(int id)
        {
            Database db = new Database();
               db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = db.Select(command);

            Collection<T> tikety = Read(reader);
            Tikety tiket = null;
            if (tikety.Count == 1)
            {
                tiket = tikety[0];
            }
            

                db.Close();
            

            return (T)tiket;
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


        private void PrepareCommand(SqlCommand command, Tikety tiket)
        {
            command.Parameters.AddWithValue("@kod", tiket.kod_tiketu);
            command.Parameters.AddWithValue("@idTipera", tiket.id_tipera);
            command.Parameters.AddWithValue("@celkovyKurz", tiket.celkovy_kurz.HasValue ? (object)tiket.celkovy_kurz.Value : DBNull.Value);
            command.Parameters.AddWithValue("@vklad", tiket.vklad);
            command.Parameters.AddWithValue("@celkovaVyhra", tiket.celkova_vyhra.HasValue ? (object)tiket.celkova_vyhra.Value : DBNull.Value);
            command.Parameters.AddWithValue("@uspesnostTiketu", tiket.uspesnost_tiketu.HasValue ? (object)tiket.uspesnost_tiketu.Value : DBNull.Value);
        }


        private void PrepareCommandUpdate(SqlCommand command, Tikety tiket)
        {

            command.Parameters.AddWithValue("@id", tiket.RecordId);
            command.Parameters.AddWithValue("@kod", tiket.kod_tiketu);
            command.Parameters.AddWithValue("@idTipera", tiket.id_tipera);
            command.Parameters.AddWithValue("@celkovyKurz", tiket.celkovy_kurz.HasValue ? (object)tiket.celkovy_kurz.Value : DBNull.Value);
            command.Parameters.AddWithValue("@vklad", tiket.vklad);
            command.Parameters.AddWithValue("@celkovaVyhra", tiket.celkova_vyhra.HasValue ? (object)tiket.celkova_vyhra.Value : DBNull.Value);
            command.Parameters.AddWithValue("@uspesnostTiketu", tiket.uspesnost_tiketu.HasValue ? (object)tiket.uspesnost_tiketu.Value : DBNull.Value);
        }


        private Collection<T> Read(SqlDataReader reader)
        {
            Collection<T> tikety = new Collection<T>();

            while (reader.Read())
            {
                int i = -1;
                Tikety tiket = new Tikety();
                tiket.RecordId = reader.GetInt32(++i);
                tiket.kod_tiketu = reader.GetString(++i);
                tiket.id_tipera = reader.GetInt32(++i);

                if (!reader.IsDBNull(++i))
                {
                    tiket.celkovy_kurz = reader.GetDouble(i);
                }

                tiket.vklad = reader.GetDouble(++i);

                if (!reader.IsDBNull(++i))
                {
                    tiket.celkova_vyhra = reader.GetDouble(i);
                }

                if (!reader.IsDBNull(++i))
                {
                    tiket.uspesnost_tiketu = reader.GetBoolean(i);
                }

                tikety.Add((T)tiket);

            }
            return tikety;
        }


    }
}
