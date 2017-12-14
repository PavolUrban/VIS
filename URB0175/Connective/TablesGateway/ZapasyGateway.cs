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
        public String SQL_BOOKMAKERS_MATCHES = "select domaci_tim, hostujuci_tim,kurz_domaci,kurz_remiza,kurz_hostia,vysledok from zapasy where id_bookmakera =@id_bookmakera";
        public String SQL_UNFINISHEDM = "select count(*) from zapasy where id_bookmakera =@id_bookmakera and vysledok IS NULL";

        public String SQL_BookmakerUnfinishedM = "select id_zapasu, domaci_tim, hostujuci_tim from zapasy where id_bookmakera = @idbookmakera AND vysledok is null";
        public String SQL_setResult = "update Zapasy set vysledok=@vysledok where id_zapasu = @idZapasu";

        public String SQL_SELECT_MOJ1 = "SELECT z2.id_zapasu, z2.domaci_tim, z2.hostujuci_tim, z2.kurz_domaci, tabulka.priemKurzDomaci " +
                                              "FROM(SELECT avg(z1.kurz_domaci) as priemKurzDomaci FROM Zapasy z1) AS tabulka, Zapasy z2 " +
                                              "WHERE z2.kurz_domaci > tabulka.priemKurzDomaci";

        public String SQL_KurzovaPonuka = "Select id_zapasu, domaci_tim,hostujuci_tim, kurz_domaci,kurz_remiza,kurz_hostia FROM Zapasy where vysledok is null";

        public String SQL_SELECT_Unfinished = "Select * from zapasy where vysledok is null";


        public String SQL_SELECT_kurzDomaci = "select kurz_domaci from Zapasy where id_zapasu = @id_zapasu";
        public String SQL_SELECT_kurzRemiza = "select kurz_remiza from Zapasy where id_zapasu = @id_zapasu";
        public String SQL_SELECT_kurzHostia = "select kurz_hostia from Zapasy where id_zapasu = @id_zapasu";


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


        public int UpdateResult(int idZapasu, int vysledok)
        {
            Database db = new Database();

            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_setResult);
            command.Parameters.AddWithValue("@idZapasu", idZapasu);
            command.Parameters.AddWithValue("@vysledok", vysledok);
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



        public Collection<T> SelectAkteri()
        {
            Database db = new Database();

            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT_Unfinished);
            SqlDataReader reader = db.Select(command);



            Collection<T> zapasy = Read(reader);

            db.Close();


            return zapasy;



        }





        public Collection<ZapasyNaPonuku> SelectPonuka()
        {
            Database db = new Database();

            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_KurzovaPonuka);
            SqlDataReader reader = db.Select(command);



            Collection<ZapasyNaPonuku> zapasy = ReaderKurzovaPonuka(reader);

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





        public Collection<PomocnyObjekt4> SelectBookmakersMatches(int idBookmakera)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_BOOKMAKERS_MATCHES);
            command.Parameters.AddWithValue("@id_bookmakera", idBookmakera);

            SqlDataReader reader = db.Select(command);

            Collection<PomocnyObjekt4> clients = ReadExtended(reader);

            db.Close();
            return clients;
        }




        public Collection<PomocnyObjekt5> SelectBookmakersUnfinishedMatches(int idBookmakera)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_BookmakerUnfinishedM);
            command.Parameters.AddWithValue("@idbookmakera", idBookmakera);

            SqlDataReader reader = db.Select(command);

            Collection<PomocnyObjekt5> clients = ReadBookmakersUnfinished(reader);

            db.Close();
            return clients;
        }



        public int UnfinishedBookmakersMatches(int idBookmakera)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_UNFINISHEDM);
            command.Parameters.AddWithValue("@id_bookmakera", idBookmakera);

            SqlDataReader reader = db.Select(command);
            int number = 0;

            number = NumberOfUnfinishedRead(reader);

            db.Close();
            return number;
        }



        
        public double KurzZapasu(int idZapasu, int tip)
        {
            Database db = new Database();
            db.Connect();
            double number = 0.0;

            if (tip == 1)
            {
                SqlCommand command = db.CreateCommand(SQL_SELECT_kurzDomaci);
                command.Parameters.AddWithValue("@id_zapasu", idZapasu);
                SqlDataReader reader = db.Select(command);
                number = KurzZapasuReader(reader);
            }

            else if (tip == 0)
            {
                SqlCommand command = db.CreateCommand(SQL_SELECT_kurzRemiza);
                command.Parameters.AddWithValue("@id_zapasu", idZapasu);
                SqlDataReader reader = db.Select(command);
                number = KurzZapasuReader(reader);
            }

            else
            {
                SqlCommand command = db.CreateCommand(SQL_SELECT_kurzHostia);
                command.Parameters.AddWithValue("@id_zapasu", idZapasu);
                SqlDataReader reader = db.Select(command);
                number = KurzZapasuReader(reader);
            }
           
            db.Close();
            return number;
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
                if (!reader.IsDBNull(++i))
                {
                    zapas.vysledok = reader.GetInt32(i);
                }

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




        private Collection<ZapasyNaPonuku> ReaderKurzovaPonuka(SqlDataReader reader)
        {
            Collection<ZapasyNaPonuku> pos = new Collection<ZapasyNaPonuku>();

            while (reader.Read())
            {
                int i = -1;
                ZapasyNaPonuku po = new ZapasyNaPonuku();
                po.ID_Zapasu = reader.GetInt32(++i);
                po.domaci = reader.GetString(++i);
                po.hostia = reader.GetString(++i);
                po.kurzD = reader.GetDouble(++i);
               
                if (!reader.IsDBNull(++i))
                {
                    po.kurzR = reader.GetDouble(i);
                }

                po.kurzH = reader.GetDouble(++i);



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



        private Collection<PomocnyObjekt4> ReadExtended(SqlDataReader reader)
        {
            Collection<PomocnyObjekt4> clients = new Collection<PomocnyObjekt4>();

            while (reader.Read())
            {
                PomocnyObjekt4 client = new PomocnyObjekt4();
                int i = -1;
                client.domaciTim = reader.GetString(++i);
                client.hostujuciTim = reader.GetString(++i);
                client.kurzDomaci = reader.GetDouble(++i);
                if (!reader.IsDBNull(++i))
                {
                    client.kurzRemiza = reader.GetDouble(i);
                }
                client.kurzHostia = reader.GetDouble(++i);
                if (!reader.IsDBNull(++i))
                {
                    client.vysledok= reader.GetInt32(i);
                }

                clients.Add(client);
            }
            return clients;
        }






        private Collection<PomocnyObjekt5> ReadBookmakersUnfinished(SqlDataReader reader)
        {
            Collection<PomocnyObjekt5> clients = new Collection<PomocnyObjekt5>();

            while (reader.Read())
            {
                PomocnyObjekt5 client = new PomocnyObjekt5();
                int i = -1;
                client.ID_Zapasu = reader.GetInt32(++i);
                client.domaci = reader.GetString(++i);
                client.hostia = reader.GetString(++i);
               


                clients.Add(client);
            }
            return clients;
        }




     





        private int NumberOfUnfinishedRead(SqlDataReader reader)
        {
            int result = 0;

            while (reader.Read())
            {
                
                int i = -1;
                result = reader.GetInt32(++i);
               
            }
            return result;
        }



        private double KurzZapasuReader(SqlDataReader reader)
        {
            double result = 0;

            while (reader.Read())
            {

                int i = -1;
                result = reader.GetDouble(++i);

            }
            return result;
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


public class PomocnyObjekt4
{
    public string domaciTim { get; set; }
    public string hostujuciTim { get; set; }
    public double kurzDomaci { get; set; }
    public double? kurzRemiza { get; set; } //doplnil som otaznik
    public double kurzHostia { get; set; }
    public int? vysledok { get; set; }
}


public class PomocnyObjekt5
{
    
   public int ID_Zapasu { get; set; }
    public string domaci { get; set; }
    public string hostia { get; set; }
   
}


public class ZapasyNaPonuku
{
    public int ID_Zapasu { get; set; }
    public string domaci { get; set; }
    public string hostia { get; set; }
    public double kurzD { get; set;}
    public double? kurzR { get; set; }
    public double kurzH { get; set; }
}


public class KurzZapasu
{
    public double kurzZapasu { get; set; }
}