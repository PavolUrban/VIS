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
    public class SportGateway<T> : SportGatewayInterface<T>
        where T : Sport
    {
        private static SportGateway<Sport> instance;
        private SportGateway() { }

        public static SportGateway<Sport> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SportGateway<Sport>();
                }
                return instance;
            }
        }





        public String SQL_SELECT = "SELECT * FROM \"Sport\"";
        public String SQL_SELECT_ID = "SELECT * FROM \"Sport\" WHERE id_sportu=@id";
        public String SQL_INSERT = "INSERT INTO \"Sport\" VALUES (@nazov)";
        public String SQL_DELETE_ID = "DELETE FROM \"Sport\" WHERE id_sportu=@id";
        public String SQL_UPDATE = "UPDATE \"Sport\" SET nazov_sportu=@nazov WHERE id_sportu=@id";


        public int Insert(T t)
        {
            Database db = new Database();
           
              
                db.Connect();
           
            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, (Sport)t);
            int ret = db.ExecuteNonQuery(command);
            
                db.Close();
            

            return ret;
        }

        public int Update(T t)
        {
            Database db = new Database();
            
                
                db.Connect();
            
            
            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommandUpdate(command, (Sport)t);
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

            Collection<T> sporty = Read(reader);
            reader.Close();
            
                db.Close();
            

            return sporty;
        }

        public T Select(int id)
        {
            Database db = new Database();
            
                db.Connect();
            
            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = db.Select(command);

            Collection<T> sporty = Read(reader);
            Sport sport = null;
            if (sporty.Count == 1)
            {
                sport = sporty[0];
            }
            

           
                db.Close();
            

            return (T)sport;
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

        private void PrepareCommand(SqlCommand command, Sport sport)
        {
            command.Parameters.AddWithValue("@nazov", sport.nazov_sportu);
        }


        private void PrepareCommandUpdate(SqlCommand command, Sport sport)
        {

            command.Parameters.AddWithValue("@id", sport.RecordId);
            command.Parameters.AddWithValue("@nazov", sport.nazov_sportu);
        }


        private static Collection<T> Read(SqlDataReader reader)
        {
            Collection<T> sporty = new Collection<T>();

            while (reader.Read())
            {
                int i = -1;
                Sport sport = new Sport();
                sport.RecordId = reader.GetInt32(++i);
                sport.nazov_sportu = reader.GetString(++i);

                sporty.Add((T)sport);

            }
            return sporty;
        }




    }
}
