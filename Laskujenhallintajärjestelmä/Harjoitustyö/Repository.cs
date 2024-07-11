

using MySqlConnector;
using System.Collections.ObjectModel;

namespace Harjoitustyö
{

    public partial class Repository
    {
        private const string local = @"Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1;";
        private const string localWithDb = @"Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=harjoitustyö;";

        public Laskuttaja GetLaskuttaja()
        {
            var laskuttaja = new Laskuttaja();
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
               
                conn.Open();

                var sqlLause = "SELECT * FROM laskuttaja;";

                MySqlCommand cmd = new MySqlCommand(sqlLause, conn);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    laskuttaja.Nimi = dr.GetString("Nimi");
                    laskuttaja.Osoite = dr.GetString("Osoite");

                }
            }
            return laskuttaja;

        }
        public void CreateDb()
        {

            using (MySqlConnection conn = new MySqlConnection(local))
            {
                conn.Open();


                MySqlCommand cmd = new MySqlCommand("DROP DATABASE IF EXISTS harjoitustyö", conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand("CREATE DATABASE harjoitustyö", conn);
                cmd.ExecuteNonQuery();
            }
        }
        public void CreateTables()
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                string createTables = "USE `harjoitustyö`;\r\n\r\n" +
                    "CREATE TABLE if not exists Tuote\r\n(\r\n" +
                    "  Tuote_ID INT NOT NULL Auto_increment,\r\n" +
                    "  Hinta FLOAT NOT NULL,\r\n" +
                    "  Nimi VARCHAR(30),\r\n" +
                    "  PRIMARY KEY (Tuote_ID)\r\n" +
                    ");\r\n\r\n" +
                    "CREATE TABLE if not exists Asiakas\r\n(\r\n" +
                    "  Osoite VARCHAR(60) NOT NULL,\r\n" +
                    "  Asiakas_ID INT NOT NULL Auto_increment,\r\n" +
                    "  Nimi VARCHAR(30),\r\n" +
                    "  PRIMARY KEY (Asiakas_ID)\r\n" +
                    ");\r\n\r\n\r\n" +
                    "CREATE TABLE if not exists Lasku\r\n(\r\n" +
                    "  Maksettu BOOL," +
                    "  Summa FLOAT," +
                    "  Työtunnit FLOAT NOT NULL,\r\n" +
                    "  Työn_hinta FLOAT NOT NULL,\r\n" +
                    "  LaskuID INT NOT NULL Auto_increment,\r\n" +
                    "  Osoite VARCHAR(60),\r\n" +
                    "  Pvm DATE,\r\n" +
                    "  Eräpäivä DATE,\r\n" +
                    "  Lisätiedot VARCHAR(300),\r\n" +
                    "  Asiakas_ID INT NOT NULL,\r\n" +
                    "  PRIMARY KEY (LaskuID),\r\n" +
                    "  FOREIGN KEY (Asiakas_ID) REFERENCES Asiakas(Asiakas_ID)\r\n" +
                    ");\r\n\r\n" +
                    "CREATE TABLE if not exists Laskurivi\r\n(\r\n" +
                    "  Hinta FLOAT," +
                    "  Rivi INT NOT NULL Auto_increment,\r\n" +
                    "  Määrä FLOAT NOT NULL,\r\n" +
                    "  LaskuID INT NOT NULL,\r\n" +
                    "  Tuote_ID INT NOT NULL,\r\n" +
                    "  Tuote_Nimi VARCHAR(50),\r\n" +
                    "  PRIMARY KEY (Rivi),\r\n" +
                    "  FOREIGN KEY (LaskuID) REFERENCES Lasku(LaskuID),\r\n" +
                    "  FOREIGN KEY (Tuote_ID) REFERENCES Tuote(Tuote_ID)\r\n" +
                    ");\r\n\r\n" +
                    "CREATE table if not exists Laskuttaja\r\n" +
                    "(\r\n\t" +
                    "Nimi VARCHAR(30),\r\n\t" +
                    "Osoite VARCHAR(50)\r\n" +
                    ");";


                MySqlCommand cmd = new MySqlCommand(createTables, conn);
                cmd.ExecuteNonQuery();
            }
        }
        public void CreateInitialData()
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                string createInitialData = "" +
                    "USE `harjoitustyö`;" +
                    "INSERT INTO asiakas" +
                    "(Osoite, Nimi)" +
                    "VALUES ('Osoitekatu 6', 'Matti Meikäläinen'), ('Osoitekuja 8', 'Heikki Heikkinen'), " +
                    "('Satukuja 8', 'Jussi Mattinen'), ('Satutie 10', 'Erkki Erkkinen');" +
                    "INSERT INTO tuote" +
                    "(Hinta, Nimi)" +
                    "VALUES (1.90, 'Tiili'), (2.75, 'Maali/litra'), (4.99, 'Kattopaneeli'), (2.49, 'Keittiölaatta'), " +
                    "(3.80, 'Rauta/kilo'), (2.45, 'Kupari/kilo'), (6.99, 'Kylpyhuonehana'), (6.45, 'Keittiöhana'), " +
                    "(0.1, 'Naula'), (1, 'Liima/litra');" +
                    "INSERT INTO laskuttaja\r\n\t" +
                    "(Nimi, Osoite)\r\n\t" +
                    "VALUES ('Rakennus Oy', 'Osoitekuja 8');\r\n\t" +
                    "CREATE VIEW asiakas_2_Laskun_1_tiedot as\r\n" +
                    "SELECT asiakas.Nimi, asiakas.Osoite, Työtunnit, Työn_hinta, lasku.Osoite AS 'Laskuttajan osoite', " +
                    "Pvm AS 'Lasku luotu', Eräpäivä, Lisätiedot\r\n" +
                    "FROM lasku, asiakas\r\n" +
                    "WHERE asiakas.Asiakas_ID = 2 AND " +
                    "lasku.Asiakas_ID = 2;\r\n";

                MySqlCommand cmd = new MySqlCommand(createInitialData, conn);
                cmd.ExecuteNonQuery();

            }

        }
        public void CreateInitialBills()
        {
            using (MySqlConnection connection = new MySqlConnection(localWithDb))
            {
                connection.Open();

                string InsertBills = "USE `harjoitustyö`;\r\n" +
                    "INSERT INTO lasku(Maksettu, `Asiakas_ID`, `Työtunnit`, `Työn_hinta`, Osoite, Pvm, `Eräpäivä`, `Lisätiedot`)" +
                    "VALUES (0, 2, 3.5, 22.90, 'Osoitekatu 4b', NOW(), '2023-04-15', 'Keittiöremontti');\t\r\n" +
                    "USE `harjoitustyö`;\r\n" +
                    "INSERT INTO laskurivi(`Määrä`, LaskuID, Tuote_ID)" +
                    "VALUES (2, 1, 1), (4, 1, 2), (6, 1, 3), (4, 1, 4), (2, 1, 5);";
                   
                    
                    
                    string UpdateTuoteNimiJaHinta = "UPDATE laskurivi\r\n\t" +
                    "SET Tuote_nimi = (SELECT nimi FROM tuote WHERE Tuote_ID = 1)\r\n\t" +
                    "WHERE Tuote_ID = 1;" +
                    "UPDATE laskurivi\r\n" +
                    "SET Hinta = (SELECT hinta FROM tuote WHERE tuote_id = 1) WHERE Tuote_ID = 1;\r\n\r\n" +
                    "UPDATE laskurivi\r\n\t" +
                    "SET Tuote_nimi = (SELECT nimi FROM tuote WHERE Tuote_ID = 2)\r\n\t" +
                    "WHERE Tuote_ID = 2;\r\n" +
                    "UPDATE laskurivi\r\nSET Hinta = (SELECT hinta FROM tuote WHERE tuote_id = 2) WHERE Tuote_ID = 2;\r\n" +
                    "UPDATE laskurivi\r\n\t" +
                    "SET Tuote_nimi = (SELECT nimi FROM tuote WHERE Tuote_ID = 3)\r\n\t" +
                    "WHERE Tuote_ID = 3;" +
                    "UPDATE laskurivi\r\nSET Hinta = (SELECT hinta FROM tuote WHERE tuote_id = 3) WHERE Tuote_ID = 3;\r\n\r\n" +
                    "UPDATE laskurivi\r\n\t" +
                    "SET Tuote_nimi = (SELECT nimi FROM tuote WHERE Tuote_ID = 4)\r\n\t" +
                    "WHERE Tuote_ID = 4;\r\n" +
                    "UPDATE laskurivi\r\nSET Hinta = (SELECT hinta FROM tuote WHERE tuote_id = 4) WHERE Tuote_ID = 4;\r\n" +
                    "UPDATE laskurivi\r\n\t" +
                    "SET Tuote_nimi = (SELECT nimi FROM tuote WHERE Tuote_ID = 5)\r\n\t" +
                    "WHERE Tuote_ID = 5;\r\n" +
                    "UPDATE laskurivi\r\nSET Hinta = (SELECT hinta FROM tuote WHERE tuote_id = 5) WHERE Tuote_ID = 5;\r\n" +
                    "UPDATE laskurivi\r\n\t" +
                    "SET Tuote_nimi = (SELECT nimi FROM tuote WHERE Tuote_ID = 6)\r\n\t" +
                    "WHERE Tuote_ID = 6;" +
                    "UPDATE laskurivi\r\nSET Hinta = (SELECT hinta FROM tuote WHERE tuote_id = 6) WHERE Tuote_ID = 6;\r\n";
                    

                MySqlCommand cmd = new MySqlCommand(InsertBills + UpdateTuoteNimiJaHinta, connection);
                cmd.ExecuteNonQuery();
            }
        }
        public ObservableCollection<Asiakas> GetAsiakkaat()
        {
            var asiakkaat = new ObservableCollection<Asiakas>();

            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM asiakas", conn);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    asiakkaat.Add(new Asiakas
                    {
                        Asiakas_ID = dr.GetInt32("Asiakas_ID"),
                        Nimi = dr.GetString("Nimi"),
                        Osoite = dr.GetString("Osoite")
                    });
                }
            }

            return asiakkaat;

        }
        public Asiakas GetAsiakasIDnumerolla(string asiakas_id)
        {
            Asiakas asiakas = new Asiakas();
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                var sqlLause = "SELECT * FROM asiakas\r\nWHERE Asiakas_ID = " + asiakas_id + ";\r\n";

                MySqlCommand cmd = new MySqlCommand(sqlLause, conn);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    asiakas.Asiakas_ID = dr.GetInt32("Asiakas_ID");
                    asiakas.Nimi = dr.GetString("Nimi");
                    asiakas.Osoite = dr.GetString("Osoite");

                }
            }
            return asiakas;
        }

        public Lasku GetLasku(string lasku_id)
        {
            var lasku = new Lasku();

            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                var sqlLause = "SELECT * FROM lasku WHERE LaskuID = " + lasku_id + ";";

                MySqlCommand cmd = new MySqlCommand(sqlLause, conn);


                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //lasku.Maksettu = dr.GetBoolean("Maskettu");
                    lasku.Työtunnit = dr.GetFloat("Työtunnit");
                    lasku.Työn_hinta = dr.GetFloat("Työn_hinta");
                    lasku.LaskuID = dr.GetInt32("LaskuID");
                    lasku.Osoite = dr.GetString("Osoite");
                    lasku.Pvm = dr.GetDateTime("Pvm");
                    lasku.Eräpäivä = dr.GetDateTime("Eräpäivä");
                    lasku.Lisätiedot = dr.GetString("Lisätiedot");
                    lasku.Asiakas_ID = dr.GetInt32("Asiakas_ID");

                    lasku.Laskurivit = GetLaskujenRivit(lasku.LaskuID.ToString());


                }




            }
            return lasku;

        }
        public ObservableCollection<Lasku> GetAsiakkaanLaskut(string Asiakas_ID)
        {
            var laskut = new ObservableCollection<Lasku>();

            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                var sqlLause = "SELECT * FROM lasku WHERE Asiakas_ID = " + Asiakas_ID + ";";

                MySqlCommand cmd = new MySqlCommand(sqlLause, conn);

                var dr = cmd.ExecuteReader();



                while (dr.Read())
                {
                    var lasku = new Lasku();

                    lasku.Maksettu = dr.GetBoolean("Maksettu");
                    lasku.Työtunnit = dr.GetFloat("Työtunnit");
                    lasku.Työn_hinta = dr.GetFloat("Työn_hinta");
                    lasku.LaskuID = dr.GetInt32("LaskuID");
                    lasku.Osoite = dr.GetString("Osoite");
                    lasku.Pvm = dr.GetDateTime("Pvm");
                    lasku.Eräpäivä = dr.GetDateTime("Eräpäivä");
                    lasku.Lisätiedot = dr.GetString("Lisätiedot");
                    lasku.Asiakas_ID = dr.GetInt32("Asiakas_ID");

                    lasku.Laskurivit = GetLaskujenRivit(lasku.LaskuID.ToString());

                    laskut.Add(lasku);
                }

            }
            return laskut;
        }

        private ObservableCollection<Laskurivi> GetLaskujenRivit(string lasku_id)
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                var laskurivit = new ObservableCollection<Laskurivi>();

                var sqlLause = "SELECT * FROM laskurivi WHERE LaskuID = " + lasku_id;

                MySqlCommand cmd = new MySqlCommand(sqlLause, conn);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    laskurivit.Add(new Laskurivi
                    {
                        Hinta = dr.GetFloat("Hinta"),
                        Rivi = dr.GetInt32("Rivi"),
                        Määrä = dr.GetFloat("Määrä"),
                        LaskuID = dr.GetInt32("LaskuID"),
                        TuoteID = dr.GetInt32("Tuote_ID"),
                        Tuote_nimi = dr.GetString("Tuote_nimi")
                    }); ;
                }
                return laskurivit;

            }
        }
        public ObservableCollection<Lasku> GetLaskut()
        {
            ObservableCollection<Lasku> laskut = new ObservableCollection<Lasku>();

            var sqlLause = "SELECT * FROM lasku";

            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sqlLause, conn);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var lasku = new Lasku();

                    lasku.Maksettu = dr.GetBoolean("Maksettu");
                    lasku.Työtunnit = dr.GetFloat("Työtunnit");
                    lasku.Työn_hinta = dr.GetFloat("Työn_hinta");
                    lasku.LaskuID = dr.GetInt32("LaskuID");
                    lasku.Osoite = dr.GetString("Osoite");
                    lasku.Pvm = dr.GetDateTime("Pvm");
                    lasku.Eräpäivä = dr.GetDateTime("Eräpäivä");
                    lasku.Lisätiedot = dr.GetString("Lisätiedot");
                    lasku.Asiakas_ID = dr.GetInt32("Asiakas_ID");

                    laskut.Add(lasku);

                }
                return laskut;
            }

        }
       
        private void Update_TuoteNimi_Ja_Hinta(ObservableCollection<Laskurivi> laskurivit)
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();
                foreach (var rivi in laskurivit)
                {
                    var sql = "UPDATE laskurivi SET Tuote_nimi  = (SELECT nimi FROM tuote WHERE Tuote_ID = @Tuote_ID) WHERE Tuote_ID = @Tuote_ID;" +
                        "UPDATE laskurivi SET Hinta  = (SELECT Hinta FROM tuote WHERE Tuote_ID = @Tuote_ID) WHERE Tuote_ID = @Tuote_ID;";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Tuote_ID", rivi.TuoteID.ToString());
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void PoistaKokoLasku(Lasku lasku)
        {
            PoistaLaskunKaikkiRivit(lasku.Laskurivit);
            var sqlLause = "DELETE FROM lasku WHERE LaskuID=@LaskuID;\n" +
                "DELETE FROM laskurivi WHERE LaskuID=@LaskuID;";

            using (MySqlConnection connection = new MySqlConnection(localWithDb))
            {
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(sqlLause, connection);
                cmd.Parameters.AddWithValue("@LaskuID", lasku.LaskuID.ToString());

                cmd.ExecuteNonQuery();

                

            }
        }
        private void PoistaLaskunKaikkiRivit(ObservableCollection<Laskurivi> laskurivit)
        {
            foreach (var rivi in laskurivit)
            {
                var sqlLause = "DELETE FROM laskurivi WHERE LaskuID=@LaskuID";
                using (MySqlConnection connection = new MySqlConnection(localWithDb))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand(sqlLause, connection);
                    cmd.Parameters.AddWithValue("@LaskuID", rivi.LaskuID.ToString());

                    cmd.ExecuteNonQuery();

                }

            }

        }  
    
        public void LuoAsiakas(Asiakas asiakas)
        {
            using (var connn = new MySqlConnection(localWithDb))
            {
                connn.Open();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO asiakas(Osoite, Nimi)VALUES (@osoite, @nimi);", connn);

                cmd.Parameters.AddWithValue("@osoite", asiakas.Osoite);
                cmd.Parameters.AddWithValue("@nimi", asiakas.Nimi);

                cmd.ExecuteNonQuery();

            }

        }
        public void MuokkaaAsiakasta(string nimi, string osoite, string asiakas_id)
        {
            using (var connn = new MySqlConnection(localWithDb))
            {
                connn.Open();

                MySqlCommand cmd = new MySqlCommand("UPDATE asiakas SET nimi=@alkuperäinenNimi, osoite=@alkuperäinenOsoite WHERE Asiakas_ID=@asiakas_id", connn);

                cmd.Parameters.AddWithValue("@alkuperäinenNimi", nimi);
                cmd.Parameters.AddWithValue("@alkuperäinenOsoite", osoite);
                cmd.Parameters.AddWithValue("@asiakas_id", asiakas_id);


                cmd.ExecuteNonQuery();

            }


        }

        public void PoistaAsiakas(string asiakas_id)
        {
            var laskut = new ObservableCollection<Lasku>();
            laskut = GetAsiakkaanLaskut(asiakas_id);
            PoistaAsiakkaanLaskut(laskut);


            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();


                MySqlCommand cmd = new MySqlCommand("DELETE FROM asiakas WHERE Asiakas_ID=@AsiakasID", conn);
                cmd.Parameters.AddWithValue("@AsiakasID", asiakas_id);
                cmd.ExecuteNonQuery();

            }
        }
        public void PoistaAsiakkaanLaskut(ObservableCollection<Lasku> laskut)
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                var poistalaskurivit = "Delete FROM laskurivi WHERE LaskuID = @laskuid;";
                var poistaLaskut = "Delete FROM lasku WHERE LaskuID = @laskuid;";
                conn.Open();
                foreach (var lasku in laskut)
                {
                    MySqlCommand cmd = new MySqlCommand(poistalaskurivit, conn);
                    cmd.Parameters.AddWithValue("@laskuid", lasku.LaskuID.ToString());
                    cmd.ExecuteNonQuery();
                }

                foreach (var lasku in laskut)
                {
                    MySqlCommand cmd = new MySqlCommand(poistaLaskut, conn);
                    cmd.Parameters.AddWithValue("@laskuid", lasku.LaskuID.ToString());
                    cmd.ExecuteNonQuery();


                }





            }

        }
      
        public ObservableCollection<Tuote> GetTuotteet()
        
        {
            var tuotteet = new ObservableCollection<Tuote>();
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                var sqlLause = "SELECT * FROM tuote;";

                MySqlCommand cmd = new MySqlCommand(sqlLause, conn);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    tuotteet.Add(new Tuote
                    {
                        Tuote_ID = dr.GetInt32("Tuote_ID"),
                        Nimi = dr.GetString("Nimi"),
                        Hinta = dr.GetFloat("Hinta")
                    }); ;
                }
            }
            return tuotteet;
        }

        public void LisääTuote(Tuote tuote)
        {
            using (var connn = new MySqlConnection(localWithDb))
            {
                connn.Open();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO tuote(hinta, Nimi)VALUES (@hinta, @nimi);", connn);

                cmd.Parameters.AddWithValue("@hinta", tuote.Hinta);
                cmd.Parameters.AddWithValue("@nimi", tuote.Nimi);

                cmd.ExecuteNonQuery();

            }
        }
        public void MuokkaaTuote(Tuote tuote)
        {
            using (var connn = new MySqlConnection(localWithDb))
            {
                connn.Open();

                MySqlCommand cmd = new MySqlCommand("UPDATE tuote SET nimi=@nimi, hinta=@hinta WHERE Tuote_ID=@tuote_id", connn);

                cmd.Parameters.AddWithValue("@nimi", tuote.Nimi);
                cmd.Parameters.AddWithValue("@hinta", tuote.Hinta);
                cmd.Parameters.AddWithValue("@tuote_id", tuote.Tuote_ID);


                cmd.ExecuteNonQuery();

            }

        }
        public void TallennaLasku(Lasku lasku)
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                if (lasku.LaskuID == 0)
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO lasku(Maksettu, Summa, `Työtunnit`, `Työn_hinta`, Osoite, Pvm, `Eräpäivä`, `Lisätiedot`, Asiakas_ID) VALUES " +
                        "(@maksettu, @summa, @työtunnit, @työn_hinta, @osoite, @pvm, @eräpäivä, @lisätiedot, @asiakas_id);", conn);

                    cmd.Parameters.AddWithValue("@maksettu", lasku.Maksettu);
                    cmd.Parameters.AddWithValue("@summa", lasku.Summa);
                    cmd.Parameters.AddWithValue("@työtunnit", lasku.Työtunnit);
                    cmd.Parameters.AddWithValue("@työn_hinta", lasku.Työn_hinta);
                    cmd.Parameters.AddWithValue("@osoite", lasku.Osoite);
                    cmd.Parameters.AddWithValue("@pvm", lasku.Pvm);
                    cmd.Parameters.AddWithValue("@eräpäivä", lasku.Eräpäivä);
                    cmd.Parameters.AddWithValue("@lisätiedot", lasku.Lisätiedot);
                    cmd.Parameters.AddWithValue("@asiakas_id", lasku.Asiakas_ID);


                    cmd.ExecuteNonQuery();

                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE lasku SET Maksettu=@Maksettu, Summa=@Summa, `Työtunnit`=@työtunnit, `Työn_hinta`=@työn_hinta, Osoite=@osoite, Pvm=@pvm, `Eräpäivä`=@eräpäivä,`Lisätiedot`=@lisätiedot, Asiakas_ID=@asiakas_id WHERE LaskuID = @laskuid", conn);

                    cmd.Parameters.AddWithValue("@maksettu", lasku.Maksettu);
                    cmd.Parameters.AddWithValue("@summa", lasku.Summa);
                    cmd.Parameters.AddWithValue("@työtunnit", lasku.Työtunnit);
                    cmd.Parameters.AddWithValue("@työn_hinta", lasku.Työn_hinta);
                    cmd.Parameters.AddWithValue("@osoite", lasku.Osoite);
                    cmd.Parameters.AddWithValue("@pvm", lasku.Pvm);
                    cmd.Parameters.AddWithValue("@eräpäivä", lasku.Eräpäivä);
                    cmd.Parameters.AddWithValue("@lisätiedot", lasku.Lisätiedot);
                    cmd.Parameters.AddWithValue("@asiakas_id", lasku.Asiakas_ID);
                    cmd.Parameters.AddWithValue("@laskuid", lasku.LaskuID);

                    cmd.ExecuteNonQuery();


                }

               
            }
        }

        public void TallennaLaskunRivit(Lasku lasku)
            
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                foreach (var line in lasku.Laskurivit)
                {

                    if (line.Rivi == 0)
                    {
                        // INSERT
                        MySqlCommand cmdIns = new MySqlCommand("INSERT INTO laskurivi (Hinta, Rivi, `Määrä`, LaskuID, Tuote_ID, Tuote_Nimi) " +
                            "VALUES (@hinta, @rivi, @määrä, @laskuid, @tuote_id, @tuote_nimi)", conn);

                        cmdIns.Parameters.AddWithValue("@hinta", line.Hinta);
                        cmdIns.Parameters.AddWithValue("@rivi", line.Rivi);
                        cmdIns.Parameters.AddWithValue("@määrä", line.Määrä);
                        cmdIns.Parameters.AddWithValue("@laskuid", lasku.LaskuID);
                        cmdIns.Parameters.AddWithValue("@tuote_id", line.TuoteID);
                        cmdIns.Parameters.AddWithValue("@tuote_nimi", line.Tuote_nimi);


                        cmdIns.ExecuteNonQuery();


                    }
                    else
                    {
                        // UPDATE
                        MySqlCommand cmdUpd = new MySqlCommand("UPDATE laskurivi SET Hinta=@hinta, Rivi=@rivi, Määrä=@määrä, LaskuID=@laskuid, " +
                            "Tuote_ID=@tuoteid, Tuote_Nimi=@tuote_nimi WHERE Rivi=@rivi", conn);

                        cmdUpd.Parameters.AddWithValue("@hinta", line.Hinta);
                        cmdUpd.Parameters.AddWithValue("@rivi", line.Rivi);
                        cmdUpd.Parameters.AddWithValue("@määrä", line.Määrä);
                        cmdUpd.Parameters.AddWithValue("@laskuid", line.LaskuID);
                        cmdUpd.Parameters.AddWithValue("@tuoteid", line.TuoteID);
                        cmdUpd.Parameters.AddWithValue("@tuote_nimi", line.Tuote_nimi);

                        cmdUpd.ExecuteNonQuery();

                    }


                }
                
               


            }
            Update_TuoteNimi_Ja_Hinta(lasku.Laskurivit);
        }
        
        

    }
}

               
             
            


      
