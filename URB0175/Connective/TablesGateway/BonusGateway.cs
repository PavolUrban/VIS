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
    public class BonusGateway<T> : BonusGatewayInterface<T>
        where T : Bonus
    {
        private static BonusGateway<Bonus> instance;
        private BonusGateway() { }

        public static BonusGateway<Bonus> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BonusGateway<Bonus>();
                }
                return instance;
            }
        }


        public String SQL_SELECT = "SELECT * FROM \"Bonus\"";
        public String SQL_SELECT_ID = "SELECT * FROM \"Bonus\" WHERE id_bonusu=@id";
        public String SQL_INSERT = "INSERT INTO \"Bonus\" VALUES (@nazov, @popis)";
        public String SQL_DELETE_ID = "DELETE FROM \"Bonus\" WHERE id_bonusu=@id";
        public String SQL_UPDATE = "UPDATE \"Bonus\" SET nazov_bonusu=@nazov, popis_bonusu=@popis WHERE id_bonusu=@id";


        public int Insert(T t)
        {
            Database db = new Database(); 
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, (Bonus)t);
            int ret = db.ExecuteNonQuery(command);

            
            db.Close();
            

            return ret;
        }

        public int Update(T t)
        {
            Database db = new Database(); 
            
                db.Connect();
       
             

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommandUpdate(command,(Bonus)t);
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

            Collection<T> Bonusy = Read(reader);
            reader.Close();

          
            db.Close();
            

            return Bonusy;
        }

        public T Select(int id, Database pDb = null)
        {
            Database db = new Database(); ;
           
                db.Connect();
            
          

            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = db.Select(command);

            Collection<T> bonusy = Read(reader);
            Bonus bonus = null;
            if (bonusy.Count == 1)
            {
                bonus = bonusy[0];
            }
            reader.Close();

       
                db.Close();
            

            return  (T)bonus;
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


        private void PrepareCommand(SqlCommand command, Bonus bonus)
        {
            command.Parameters.AddWithValue("@nazov", bonus.nazov_bonusu);
            command.Parameters.AddWithValue("@popis", bonus.popis_bonusu == null ? DBNull.Value : (object)bonus.popis_bonusu);
        }

        private void PrepareCommandUpdate(SqlCommand command, Bonus bonus)
        {

            command.Parameters.AddWithValue("@id", bonus.RecordId);
            command.Parameters.AddWithValue("@nazov", bonus.nazov_bonusu);
            command.Parameters.AddWithValue("@popis", bonus.popis_bonusu == null ? DBNull.Value : (object)bonus.popis_bonusu);
        }


        private Collection<T> Read(SqlDataReader reader)
        {
            Collection<T> bonusy = new Collection<T>();

            while (reader.Read())
            {
                int i = -1;
                Bonus bonus = new Bonus();
                bonus.RecordId = reader.GetInt32(++i);
                bonus.nazov_bonusu = reader.GetString(++i);

                if (!reader.IsDBNull(++i))
                {
                    bonus.popis_bonusu = reader.GetString(i);
                }

                bonusy.Add((T)bonus);

            }
            return bonusy;
        }
    }
}
