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
    class TiperoveBonusyGateway<T> : TiperoveBonusyGatewayInterface<T>
        where T : TiperoveBonusy
    {


        private static TiperoveBonusyGateway<TiperoveBonusy> instance;
        private TiperoveBonusyGateway() { }

        public static TiperoveBonusyGateway<TiperoveBonusy> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TiperoveBonusyGateway<TiperoveBonusy>();
                }
                return instance;
            }
        }



        public String SQL_SELECT = "SELECT * FROM \"TiperoveBonusy\"";
        public String SQL_SELECT_ID = "SELECT * FROM \"TiperoveBonusy\" WHERE Tiper_id_tipera=@idTipera AND Bonus_id_bonusu=@idBonusu";
        public String SQL_INSERT = "INSERT INTO \"TiperoveBonusy\" VALUES (@idTipera, @idBonusu)";
        public String SQL_DELETE_ID = "DELETE FROM \"TiperoveBonusy\" WHERE Tiper_id_tipera=@idTipera";
        public String SQL_UPDATE = "UPDATE \"TiperoveBonusy\" SET Bonus_id_bonusu=@idBonusu WHERE Tiper_id_tipera=@idTipera AND Bonus_id_bonusu=@idBonusu";


        public int Insert(T t)
        {
            Database db = new Database();
            
                db.Connect();
            
            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, (TiperoveBonusy)t);
            int ret = db.ExecuteNonQuery(command);

                db.Close();
            

            return ret;
        }

        public int Update(T t)
        {
            Database db = new Database();
            
             
                db.Connect();
            
            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, (TiperoveBonusy)t);
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

            Collection<T> tbs = Read(reader);
           
           
                db.Close();
            

            return tbs;
        }







        public T Select(int idTipera, int idBonusu)
        {
            Database db = new Database();
            
               
                db.Connect();
           

            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@idTipera", idTipera);
            command.Parameters.AddWithValue("@idBonusu", idBonusu);
            SqlDataReader reader = db.Select(command);

            Collection<T> tbs = Read(reader);
            TiperoveBonusy tb = null;
            if (tbs.Count == 1)
            {
                tb = tbs[0];
            }
            
                db.Close();
            

            return (T)tb;
        }



        public int Delete(int idTipera)
        {
            Database db = new Database();
        
                db.Connect();
            SqlCommand command = db.CreateCommand(SQL_DELETE_ID);

            command.Parameters.AddWithValue("@idTipera", idTipera);
        
            int ret = db.ExecuteNonQuery(command);

              db.Close();
        

            return ret;
        }

        private void PrepareCommand(SqlCommand command, TiperoveBonusy tb)
        {
            command.Parameters.AddWithValue("@idTipera", tb.Tiper_id_tipera);
            command.Parameters.AddWithValue("@idBonusu", tb.Bonus_id_bonusu);
        }


        private Collection<T> Read(SqlDataReader reader)
        {
            Collection<T> tbs = new Collection<T>();

            while (reader.Read())
            {
                int i = -1;
                TiperoveBonusy tb = new TiperoveBonusy();
                tb.Tiper_id_tipera = reader.GetInt32(++i);
                tb.Bonus_id_bonusu = reader.GetInt32(++i);

                tbs.Add((T)tb);

            }
            return tbs;
        }
    }
}
