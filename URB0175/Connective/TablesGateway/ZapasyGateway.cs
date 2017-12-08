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
    public class ZapasyGateway<T> : ZapasyGatewayInterface<T>
        where T : Zapasy
    {
        private static ZapasyGateway<Zapasy> instance;
        private ZapasyGateway() { }

        public static ZapasyGateway<Zapasy> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ZapasyGateway<Zapasy>();
                }
                return instance;
            }
        }

        public String SQL_SELECT = "SELECT * FROM \"Zapasy\"";
        public String SQL_SELECT_ID = "SELECT * FROM \"Zapasy\" WHERE id_zapasu=@id";
        public String SQL_INSERT = "INSERT INTO \"Zapasy\" VALUES (@domaci, @hostujuci,@kurzD, @kurzR, @kurzH, @sport, @bookmaker,@vysledok)";
        public String SQL_DELETE_ID = "DELETE FROM \"Zapasy\" WHERE id_zapasu=@id";
        public String SQL_UPDATE = "UPDATE \"Zapasy\" SET domaci_tim=@domaci, hostujuci_tim=@hostujuci, kurz_domaci = @kurzD, kurz_remiza=@kurzR, kurz_hostia=@kurzH, id_sportu=@sport, id_bookmakera=@bookmaker, vysledok=@vysledok WHERE id_zapasu=@id";



        public String SQL_SELECT_MOJ1 = "SELECT z2.id_zapasu, z2.domaci_tim, z2.hostujuci_tim, z2.kurz_domaci, tabulka.priemKurzDomaci " +
                                              "FROM(SELECT avg(z1.kurz_domaci) as priemKurzDomaci FROM Zapasy z1) AS tabulka, Zapasy z2 " +
                                              "WHERE z2.kurz_domaci > tabulka.priemKurzDomaci";


        public String SQL_SELECT_MOJ2 = "SELECT z2.id_zapasu, z2.domaci_tim, z2.hostujuci_tim, z2.kurz_domaci, tabulka.priemKurzDomaci, z2.id_sportu " +
"FROM(SELECT avg(z1.kurz_domaci) as priemKurzDomaci, z1.id_sportu FROM Zapasy z1 GROUP BY z1.id_sportu) " +
"AS tabulka, Zapasy z2 JOIN Sport s on s.id_sportu = z2.id_sportu WHERE z2.kurz_domaci>tabulka.priemKurzDomaci AND z2.id_sportu = tabulka.id_sportu AND s.nazov_sportu = @nazovSportu";

        public String SQL_SELECT_MOJ3 = "SELECT id_tiketu, znt.Zapasy_id_zapasu, tip, celkovy_kurz, vklad, celkova_vyhra, uspesnost_tiketu FROM ZapasyNaTikete znt Join Tikety t ON t.id_tiketu = znt.Tikety_id_tiketu Join Tiper tp ON tp.id_tipera = t.id_tipera AND t.id_tipera = @idTipera order by id_tiketu";

        public int Insert(T t)
        {
            Database db = new Database();
           
                db.Connect();
           

            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, (Zapasy)t);
            int ret = db.ExecuteNonQuery(command);
            
                db.Close();
            
            return ret;
        }


        public int Update(T t)
        {
            Database db = new Database();
            
                db.Connect();
            
            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommandUpdate(command, (Zapasy) t);
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



            Collection<T> zapasy = Read(reader);
            
             db.Close();
            

            return zapasy;



        }



        public Collection<PomocnyObjekt> MojVypisZapasov1(Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_MOJ1);
            SqlDataReader reader = db.Select(command);

            Collection<PomocnyObjekt> zapasy = Reader1(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return zapasy;
        }



        public Collection<PomocnyObjekt2> MojVypisZapasov2(string nazovSportu, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_MOJ2);
            command.Parameters.AddWithValue("@nazovSportu", nazovSportu);

            SqlDataReader reader = db.Select(command);

            Collection<PomocnyObjekt2> zapasy = Reader2(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return zapasy;
        }


        public Collection<PomocnyObjekt3> MojVypisZapasov3(int idTipera, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_MOJ3);
            command.Parameters.AddWithValue("@idTipera", idTipera);

            SqlDataReader reader = db.Select(command);

            Collection<PomocnyObjekt3> zapasy = Reader3(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return zapasy;
        }



        public T Select(int id)
        {
            Database db = new Database();
            
                db.Connect();
            
            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = db.Select(command);

            Collection<T> zapasy = Read(reader);
            Zapasy zapas = null;
            if (zapasy.Count == 1)
            {
                zapas = zapasy[0];
            }
            
                db.Close();
            

            return (T)zapas;
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


        private void PrepareCommand(SqlCommand command, Zapasy zapas)
        {
            command.Parameters.AddWithValue("@domaci", zapas.domaci_tim);
            command.Parameters.AddWithValue("@hostujuci", zapas.hostujuci_tim);
            command.Parameters.AddWithValue("@kurzD", zapas.kurz_domaci);
            command.Parameters.AddWithValue("@kurzR", zapas.kurz_remiza.HasValue ? (object)zapas.kurz_remiza.Value : DBNull.Value);
            command.Parameters.AddWithValue("@kurzH", zapas.kurz_hostia);
            command.Parameters.AddWithValue("@sport", zapas.id_sportu);
            command.Parameters.AddWithValue("@bookmaker", zapas.id_bookmakera);
            command.Parameters.AddWithValue("@vysledok", zapas.vysledok.HasValue ? (object)zapas.vysledok.Value : DBNull.Value);
        }


        private void PrepareCommandUpdate(SqlCommand command, Zapasy zapas)
        {
            command.Parameters.AddWithValue("@id", zapas.RecordId);
            command.Parameters.AddWithValue("@domaci", zapas.domaci_tim);
            command.Parameters.AddWithValue("@hostujuci", zapas.hostujuci_tim);
            command.Parameters.AddWithValue("@kurzD", zapas.kurz_domaci);
            command.Parameters.AddWithValue("@kurzR", zapas.kurz_remiza.HasValue ? (object)zapas.kurz_remiza.Value : DBNull.Value);
            command.Parameters.AddWithValue("@kurzH", zapas.kurz_hostia);
            command.Parameters.AddWithValue("@sport", zapas.id_sportu);
            command.Parameters.AddWithValue("@bookmaker", zapas.id_bookmakera);
            command.Parameters.AddWithValue("@vysledok", zapas.vysledok.HasValue ? (object)zapas.vysledok.Value : DBNull.Value);
        }


        private Collection<T> Read(SqlDataReader reader)
        {
            Collection<T> zapasy = new Collection<T>();

            while (reader.Read())
            {
                int i = -1;
                Zapasy zapas = new Zapasy();
                zapas.RecordId = reader.GetInt32(++i);
                zapas.domaci_tim = reader.GetString(++i);
                zapas.hostujuci_tim = reader.GetString(++i);
                zapas.kurz_domaci = reader.GetDouble(++i);

                if (!reader.IsDBNull(++i))
                {
                    zapas.kurz_remiza = reader.GetDouble(i);
                }

                zapas.kurz_hostia = reader.GetDouble(++i);
                zapas.id_sportu = reader.GetInt32(++i);
                zapas.id_bookmakera = reader.GetInt32(++i);


                zapasy.Add((T)zapas);

            }
            return zapasy;
        }




        private Collection<PomocnyObjekt> Reader1(SqlDataReader reader)
        {
            Collection<PomocnyObjekt> pos = new Collection<PomocnyObjekt>();

            while (reader.Read())
            {
                int i = -1;
                PomocnyObjekt po = new PomocnyObjekt();
                po.idZapasu = reader.GetInt32(++i);
                po.domaciTim = reader.GetString(++i);
                po.hostujuciTim = reader.GetString(++i);
                po.kurzDomaci = reader.GetDouble(++i);
                po.priemernyKurz = reader.GetDouble(++i);



                pos.Add(po);

            }
            return pos;
        }


        private Collection<PomocnyObjekt2> Reader2(SqlDataReader reader)
        {
            Collection<PomocnyObjekt2> pos = new Collection<PomocnyObjekt2>();

            while (reader.Read())
            {
                int i = -1;
                PomocnyObjekt2 po = new PomocnyObjekt2();
                po.idZapasu = reader.GetInt32(++i);
                po.domaciTim = reader.GetString(++i);
                po.hostujuciTim = reader.GetString(++i);
                po.kurzDomaci = reader.GetDouble(++i);
                po.priemernyKurz = reader.GetDouble(++i);
                po.idSportu = reader.GetInt32(++i);

                pos.Add(po);

            }
            return pos;
        }


        private Collection<PomocnyObjekt3> Reader3(SqlDataReader reader)
        {
            Collection<PomocnyObjekt3> pos = new Collection<PomocnyObjekt3>();

            while (reader.Read())
            {
                int i = -1;

                PomocnyObjekt3 po = new PomocnyObjekt3();
                po.idTiketu = reader.GetInt32(++i);
                po.idZapasu = reader.GetInt32(++i);
                po.tip = reader.GetInt32(++i);

                if (!reader.IsDBNull(++i))
                {
                    po.celkovyKurz = reader.GetDouble(i);
                }

                po.vklad = reader.GetDouble(++i);

                if (!reader.IsDBNull(++i))
                {
                    po.celkovaVyhra = reader.GetDouble(i);
                }

                if (!reader.IsDBNull(++i))
                {
                    po.uspesnostTiketu = reader.GetBoolean(i);
                }


                pos.Add(po);

            }
            return pos;
        }
    }
}




public class PomocnyObjekt
{
    public int idZapasu { get; set; }
    public string domaciTim { get; set; }
    public string hostujuciTim { get; set; }
    public double kurzDomaci { get; set; }
    public double priemernyKurz { get; set; }
}

public class PomocnyObjekt2
{
    public int idZapasu { get; set; }
    public string domaciTim { get; set; }
    public string hostujuciTim { get; set; }
    public double kurzDomaci { get; set; }
    public double priemernyKurz { get; set; }
    public int idSportu { get; set; }
}

public class PomocnyObjekt3
{
    public int idTiketu { get; set; }
    public int idZapasu { get; set; }
    public int tip { get; set; }
    public double? celkovyKurz { get; set; }
    public double vklad { get; set; }
    public double? celkovaVyhra { get; set; }
    public bool? uspesnostTiketu { get; set; }
}