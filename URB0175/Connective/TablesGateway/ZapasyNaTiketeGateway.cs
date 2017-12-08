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
    public class ZapasyNaTiketeGateway<T> : ZapasyNaTiketeGatewayInterface<T>
        where T : ZapasyNaTikete
    {
        private static ZapasyNaTiketeGateway<ZapasyNaTikete> instance;
        private ZapasyNaTiketeGateway() { }

        public static ZapasyNaTiketeGateway<ZapasyNaTikete> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ZapasyNaTiketeGateway<ZapasyNaTikete>();
                }
                return instance;
            }
        }

        public String SQL_SELECT = "SELECT * FROM \"ZapasyNaTikete\"";
        public String SQL_SELECT_ID = "SELECT * FROM \"ZapasyNaTikete\" WHERE Tikety_id_tiketu=@idTiketu AND Zapasy_id_zapasu=@idZapasu";
        public String SQL_SELECT_TOP1 = "SELECT top 1 * FROM Zapasynatikete order by Tikety_id_tiketu desc";
        public String SQL_INSERT = "INSERT INTO \"ZapasyNaTikete\" VALUES (@idTiketu, @idZapasu, @tip)";
        public String SQL_DELETE_ID = "DELETE FROM \"ZapasyNaTikete\" WHERE Tikety_id_tiketu=@idTiketu";
        public String SQL_UPDATE = "UPDATE \"ZapasyNaTikete\" SET tip=@tip WHERE Tikety_id_tiketu=@idTiketu AND Zapasy_id_zapasu=@idZapasu";


        public int Insert(T t)
        {
            Database db = new Database();
            
                db.Connect();
            
            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, (ZapasyNaTikete)t);
            int ret = db.ExecuteNonQuery(command);
            
                db.Close();
            

            return ret;
        }

        public int Update(T t)
        {
            Database db = new Database();
            
                db.Connect();
            
            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, (ZapasyNaTikete)t);
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

            Collection<T> znts = Read(reader);



            db.Close();
            

            return znts;
        }

        public Collection<T> SelectTOP1()
        {
            Database db = new Database();
            
                db.Connect();
            
            SqlCommand command = db.CreateCommand(SQL_SELECT_TOP1);
            SqlDataReader reader = db.Select(command);

            Collection<T> znts = Read(reader);
            
                db.Close();
            
            return znts;
        }

        public T Select(int idTiketu, int idZapasu)
        {
            Database db = new Database();
           
                db.Connect();
           
            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@idTiketu", idTiketu);
            command.Parameters.AddWithValue("@idZapasu", idZapasu);
            SqlDataReader reader = db.Select(command);

            Collection<T> znts = Read(reader);
            ZapasyNaTikete znt = null;
            if (znts.Count == 1)
            {
                znt = znts[0];
            }
                db.Close();
            
            return (T)znt;
        }



        public int Delete(int idTiketu)
        {
            Database db = new Database();
            
                db.Connect();
            
            SqlCommand command = db.CreateCommand(SQL_DELETE_ID);

            command.Parameters.AddWithValue("@idTiketu", idTiketu);
            
            int ret = db.ExecuteNonQuery(command);

                db.Close();
            
            return ret;
        }

        private void PrepareCommand(SqlCommand command, ZapasyNaTikete znt)
        {
            command.Parameters.AddWithValue("@idTiketu", znt.Tikety_id_tiketu);
            command.Parameters.AddWithValue("@idZapasu", znt.Zapasy_id_zapasu);
            command.Parameters.AddWithValue("@tip", znt.tip);
        }


        private Collection<T> Read(SqlDataReader reader)
        {
            Collection<T> znts = new Collection<T>();

            while (reader.Read())
            {
                int i = -1;
                ZapasyNaTikete znt = new ZapasyNaTikete();
                znt.Tikety_id_tiketu = reader.GetInt32(++i);
                znt.Zapasy_id_zapasu = reader.GetInt32(++i);
                znt.tip = reader.GetInt32(++i);

                znts.Add((T)znt);

            }
            return znts;
        }


        public void celkovaVyhra(int idTiketu, int idTipera)
        {
            Database db = new Database();
            
                db.Connect();
            // 1.  create a command object identifying the stored procedure
            SqlCommand command = db.CreateCommand("celkovaVyhra");

            // 2. set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            // 3. create input parameters
            SqlParameter input = new SqlParameter();

            command.Parameters.Add("@p_idTiketu", SqlDbType.Int).Value = idTiketu;
            command.Parameters.Add("@p_idTipera", SqlDbType.Int).Value = idTipera;


            // 4. execute procedure
            int ret = db.ExecuteNonQuery(command);

            Console.WriteLine(ret);


           
                db.Close();
           
        }

    }
}
