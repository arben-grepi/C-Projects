using MySqlConnector;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace Harjoitustyö
{
    /// <summary>
    /// Interaction logic for Asiakastiedot.xaml
    /// </summary>
    public partial class Asiakastiedot : Window
    {
       

        private Repository Repo;

        private ObservableCollection<Asiakas> Asiakkaat;

        private Asiakas Asiakas;

        private ObservableCollection<Lasku> Laskut;

        private ObservableCollection<Tuote> Tuotteet;
        private Lasku Lasku;


        public Asiakastiedot()
        {
            InitializeComponent();

            Repo = new Repository();

            Asiakkaat = new ObservableCollection<Asiakas>();

            Asiakas = new Asiakas();

            Laskut = new ObservableCollection<Lasku>();

            Tuotteet = new ObservableCollection<Tuote>();

            Lasku = new Lasku();

            Repo.CreateDb();
            Repo.CreateTables();
            Repo.CreateInitialData();
            Repo.CreateInitialBills();

            Laskut = Repo.GetLaskut();

            Alusta_ComBoxes_In_Asiakastiedot();

            Alusta_GroupAsiakas_Käyttöliittymä();

           
            AlustacomSelaaKaikkiaLaskuja_combobox();

        }
        /// <summary>
        /// Hakee perustiedot kaikista laskuista DB:stä ja syöttää ne string muuttujana Lasku-boxissa sijaitsevaan comboboxiin (comSelaaKaikkiaLaskuja).
        /// </summary>
        private void AlustacomSelaaKaikkiaLaskuja_combobox()
        {
            comSelaaKaikkiaLaskuja.Items.Clear();
            Laskut = Repo.GetLaskut();
            foreach (var lasku in Laskut)
            {
                comSelaaKaikkiaLaskuja.Items.Add($"LaskuID:{lasku.LaskuID} AsiakkaanID:{lasku.Asiakas_ID} Eräpäivä:{lasku.Eräpäivä}");
                

            }
           

        }

        /// <summary>
        /// Alusta molemmat comboboxit jotka sijaitsevat Asiakastiedot - laatikossa asiakkaiden tiedoilla. 
        /// Toiseen comboboxiin tuodaan asiakkaan nimi ja toiseen ID numero string muuttujassa. 
        /// </summary>
        private void Alusta_ComBoxes_In_Asiakastiedot()
        {

            comIDnum.Items.Clear();
            comNimi.Items.Clear();

            Asiakkaat = Repo.GetAsiakkaat();

            for (int i = 0; i < Asiakkaat.Count; i++)
            {
                comIDnum.Items.Add(Asiakkaat[i].Asiakas_ID.ToString());
                comNimi.Items.Add(Asiakkaat[i].Nimi.ToString());
            }




        }

        /// <summary>
        /// Alustaa Asiakas-boxin painikkeet ja tekstilaatikoiden näkyvyydet (Hidden/Visible) siihen tilaan missä ne kuuluu olla kun ohjelma on käynnistetään.
        /// </summary>
        private void Alusta_GroupAsiakas_Käyttöliittymä()
        {
            Asiakas = new Asiakas();
            groupAsiakastiedot.DataContext = Asiakas;

            comIDnum.SelectedIndex = -1;
            comNimi.SelectedIndex = -1;

            btnMuokkaaAsiakasta.Visibility = Visibility.Hidden;
            btnLuo_uusi_asiakas.Visibility = Visibility.Hidden;
            btnPeruuta.Visibility = Visibility.Hidden;
            btnTallenna.Visibility = Visibility.Hidden;
            btnPoista_Asiakas.Visibility = Visibility.Hidden;



            lblAsennetaan_automaattisesti.Visibility = Visibility.Collapsed;
            lblAsiakas_ID.Visibility = Visibility.Collapsed;
            lblNimi.Visibility = Visibility.Collapsed;
            lblOsoite.Visibility = Visibility.Collapsed;


            lblAsiakkaanLaskut.Visibility = Visibility.Collapsed;
            lblAsiakkaanLaskut.Visibility = Visibility.Collapsed;
            lblLaskut.Visibility = Visibility.Collapsed;

            txtNimi.Visibility = Visibility.Collapsed;
            txtOsoite.Visibility = Visibility.Collapsed;


            btnLuo_uusi_asiakas.Visibility = Visibility.Visible;



        }


        /// <summary>
        /// Kun kyseinen combobox suljetaan. 
        /// </summary>
        private void comIDnum_dropdownclosed(object sender, EventArgs e)
        {

            //varmistetaan että käyttäjä valitsi jonkun tiedon
            if (comIDnum.Text != string.Empty)
            {
                //valitaan asiakkaan nimi jolla on kyseinen ID
                comNimi.SelectedIndex = comIDnum.SelectedIndex;

                Com_dropdownclosed_käyttöliittymä_toimenpiteet();

                var asiakas_id = (comIDnum.SelectedItem.ToString());

                Asiakas = Repo.GetAsiakasIDnumerolla(asiakas_id);

                Laskut = Repo.GetAsiakkaanLaskut(asiakas_id);

                comNimi.SelectedIndex = comIDnum.SelectedIndex;

                comSelaaKaikkiaLaskuja.SelectedIndex = -1;


                TäytäLabeleihinAsiakkaanTiedot();



            }

        }

        /// <summary>
        /// Kun kyseinen combobox suljetaan. 
        /// </summary>
        private void comNImi_dropdownclosed(object sender, EventArgs e)
        {

            //varmistetaan että käyttäjä valitsi jonkun tiedon
            if (comNimi.Text != string.Empty)
            {
                comIDnum.SelectedIndex = comNimi.SelectedIndex;

                Com_dropdownclosed_käyttöliittymä_toimenpiteet();

                var asiakas_id = (comIDnum.SelectedItem.ToString());

                Asiakas = Repo.GetAsiakasIDnumerolla(asiakas_id);
                Laskut = Repo.GetAsiakkaanLaskut(asiakas_id);

                //valitaan sama asiakkaan id toiseen comboboxiin kyseisen comboxin valinnan perusteella
                comIDnum.SelectedIndex = comNimi.SelectedIndex;

                //varmistetaan että Lasku-boxin comboboxissa ei lue mitään laskutietoja. 
                comSelaaKaikkiaLaskuja.SelectedIndex = -1;

                TäytäLabeleihinAsiakkaanTiedot();

            }

        }


        /// <summary>
        ///Alustaa Asiakas-boxin painikkeet ja tekstilaatikoiden näkyvyydet (Hidden/Visible) siihen tilaan missä ne kuuluu olla
        ///kun comboxeihin valitaan joku asiakastieto. 
        /// </summary>
        private void Com_dropdownclosed_käyttöliittymä_toimenpiteet()
        {
            if (comIDnum.Text != string.Empty || comSelaaKaikkiaLaskuja.Text != string.Empty)
            {
                var tyhjälasku = new Lasku();


                btnLuo_uusi_asiakas.Visibility = Visibility.Visible;
                btnMuokkaaAsiakasta.Visibility = Visibility.Visible;
                btnPoista_Asiakas.Visibility = Visibility.Visible;

                btnPeruuta.Visibility = Visibility.Hidden;
                btnTallenna.Visibility = Visibility.Hidden;


                btnLuo_uusi_asiakas.Visibility = Visibility.Visible;

                lblAsennetaan_automaattisesti.Visibility = Visibility.Hidden;
                txtNimi.Visibility = Visibility.Hidden;
                txtOsoite.Visibility = Visibility.Hidden;

                lblAsiakas_ID.Visibility = Visibility.Visible; lblAsiakas_ID.Content = null;
                lblNimi.Visibility = Visibility.Visible; lblNimi.Content = string.Empty;
                lblOsoite.Visibility = Visibility.Visible; lblOsoite.Content = string.Empty;
                lblLaskut.Visibility = Visibility.Visible;
                lblAsiakkaanLaskut.Visibility = Visibility.Visible; lblAsiakkaanLaskut.Content = string.Empty;

                


            }

        }

       /// <summary>
       /// Täyteään Asiakas-boxin labeleihin comboxiin valitun asiakkaan nimi, osoite ja id tieto
       /// </summary>
        private void TäytäLabeleihinAsiakkaanTiedot()
        {
            if (Asiakas != null)
            {
                Laskut = Repo.GetAsiakkaanLaskut(Asiakas.Asiakas_ID.ToString());

                lblAsiakas_ID.Content = Asiakas.Asiakas_ID.ToString();

                lblNimi.Content = Asiakas.Nimi.ToString();

                lblOsoite.Content = Asiakas.Osoite.ToString();

                if (Laskut.Count != 0)
                {
                    lblAsiakkaanLaskut.Content = "ID: ";
                    for (int i = 0; i < Laskut.Count; i++)
                    {
                        lblAsiakkaanLaskut.Content += Laskut[i].LaskuID.ToString() + " ";

                    }

                }
                else
                {
                    lblAsiakkaanLaskut.Content = "Ei laskuja";
                }

            }

        }



        /// <summary>
        /// Metodi aktivoituu  kun painetaan "Luo uusi asiakas" asiakas nappia käyttöliittymässä
        /// (Sijaitsee Asiakas - boxissa)
        /// </summary>

        private void Luo_uusi_Asiakas_click(object sender, RoutedEventArgs e)
        {
            Luo_uusi_Asiakas_click_Käyttöliittymä_Toimenpiteet();
        }

        /// <summary>
        /// Alustetaan käyttöliittymän nappeja ja tekstilaatikoita näkyviksi tai piiloon, 
        /// jotta voidaan luoda uusi asiakas. 
        /// </summary>
        private void Luo_uusi_Asiakas_click_Käyttöliittymä_Toimenpiteet()
        {
            btnMuokkaaAsiakasta.Visibility = Visibility.Hidden;
            btnPoista_Asiakas.Visibility = Visibility.Hidden;
            btnLuo_uusi_asiakas.Visibility = Visibility.Hidden;
            btnPeruuta.Visibility = Visibility.Visible;
            btnTallenna.Visibility = Visibility.Visible;

            lblAsennetaan_automaattisesti.Visibility = Visibility.Visible;

            txtNimi.Visibility = Visibility.Visible; txtNimi.Text = string.Empty;
            txtOsoite.Visibility = Visibility.Visible; txtOsoite.Text = string.Empty;


            lblAsiakas_ID.Visibility = Visibility.Hidden;
            lblNimi.Visibility = Visibility.Hidden;
            lblOsoite.Visibility = Visibility.Hidden;
            lblLaskut.Visibility = Visibility.Hidden;
            lblAsiakkaanLaskut.Visibility = Visibility.Hidden;


            comIDnum.SelectedIndex = -1;
            comNimi.SelectedIndex = -1;

        }

        /// <summary>
        /// Metodi aktivoituu kun painetaan Peruuta nappia Asiakas-boxissa.
        /// Palautetaan käyttöliittymä takaisin siihen muotoon kun missä se on alussa.
        /// </summary>
        private void Peruuta_click(object sender, RoutedEventArgs e)
        {

            Alusta_GroupAsiakas_Käyttöliittymä();

        }

        /// <summary>
        /// Aktivoituu kun painetaan MuokkaaAsiakasta-napppia joka sijaitsee Asiakas-boxissa. 
        /// Metodi muuttuu käyttöliittymän nappian , labeleiden ja tekstilaatikoidn Visibility tilan 
        /// sille sopivaan tilaan. 
        /// </summary>
        private void MuokkaaAsiakasta_click(object sender, RoutedEventArgs e)
        {
            if (Asiakas != null)
            {
                btnMuokkaaAsiakasta.Visibility = Visibility.Hidden;
                btnLuo_uusi_asiakas.Visibility = Visibility.Hidden;
                btnPoista_Asiakas.Visibility = Visibility.Hidden;

                btnPeruuta.Visibility = Visibility.Visible;
                btnTallenna.Visibility = Visibility.Visible;

                lblAsennetaan_automaattisesti.Visibility = Visibility.Hidden;

                lblAsiakas_ID.Visibility = Visibility.Visible; lblAsiakas_ID.Content = Asiakas.Asiakas_ID.ToString();
                txtNimi.Visibility = Visibility.Visible; txtNimi.Text = Asiakas.Nimi.ToString();
                txtOsoite.Visibility = Visibility.Visible; txtOsoite.Text = Asiakas.Osoite.ToString();

                lblNimi.Visibility = Visibility.Hidden;
                lblOsoite.Visibility = Visibility.Hidden;


                comIDnum.SelectedIndex = -1;
                comNimi.SelectedIndex = -1;

            }
            else
            {

                MessageBox.Show("Valitse ensin asiakas", "Tiedote");
            }

        }

        /// <summary>
        /// Tallentaa Asiakastiedot MariaDB tietokantaan ja päivittää käyttöliittymän tiedot vastaamana nykyistä tietokantaa. 
        /// </summary>
        private void Tallenna_Asiakas_click(object sender, RoutedEventArgs e)
        {

            if (txtNimi.Text == lblNimi.Content.ToString() && txtOsoite.Text == lblOsoite.Content.ToString())
            {
                MessageBox.Show("Et muuttanut asiakkaan tietoja", "tiedoksi");


            }


            else if (txtNimi.Text != string.Empty && txtOsoite.Text != string.Empty)
            {

                var nimi = txtNimi.Text.ToString();
                var osoite = txtOsoite.Text.ToString();
                var asiakas_id = lblAsiakas_ID.Content.ToString();

                var result = MessageBox.Show("Haluatko varmasti tallentaa asiakkaan", "Kysymys", MessageBoxButton.YesNo,
                MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (lblAsennetaan_automaattisesti.Visibility == Visibility.Visible)
                    {
                        Asiakas = new Asiakas();
                        Asiakas.Nimi = nimi;
                        Asiakas.Osoite = osoite;
                        Repo.LuoAsiakas(Asiakas);


                    }
                    else
                    {
                        Repo.MuokkaaAsiakasta(nimi, osoite, asiakas_id);


                    }



                    Alusta_ComBoxes_In_Asiakastiedot();

                    Alusta_GroupAsiakas_Käyttöliittymä();

                }

            }
            else
            {
                MessageBox.Show("Anna asiakkaalle nimi ja osoite");
            }


        }

        /// <summary>
        /// Poistetaan Asiakas käyttöliittymästä MariaDB tietokannasta päivittää käyttöliittymän tiedot vastaamana nykyistä tietokantaa. 
        /// </summary>

        private void Poista_Asiakas_click(object sender, RoutedEventArgs e)
        {

            if (lblAsiakas_ID.Visibility == Visibility.Visible && lblAsiakas_ID.Content.ToString() != "0")
            {

                var result = MessageBox.Show("Haluatko varmasti poistaa asiakkaan?\n\n" +
                    "Tämä poistaa myös asiakkaan kaikki laskut tietokannasta", "Varoitus", MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Repo.PoistaAsiakas(lblAsiakas_ID.Content.ToString());
                   
                }
               

            }
            else
            {
                MessageBox.Show("Ohjelmassa tapahtui virhe", "Virhe");
                
            }
            Alusta_ComBoxes_In_Asiakastiedot();
            Alusta_GroupAsiakas_Käyttöliittymä();
            AlustacomSelaaKaikkiaLaskuja_combobox();

        }

        /// <summary>
        /// Hae tuotteet tietokannasta. 
        /// </summary>
        private void Hae_Tuotteet_click(object sender, RoutedEventArgs e)
        {


            Tuotteet_Tietokannassa tuoteIkkuna = new Tuotteet_Tietokannassa();
            tuoteIkkuna.ShowDialog();



        }

   
        /// <summary>
        /// Aktivoituuu kun Lasku-boxissa sijaitseva combobox sulkeutuu. 
        /// </summary>
        private void comSelaaKaikkiLaskut_closed(object sender, EventArgs e)
        {
            if (comSelaaKaikkiaLaskuja.Text != string.Empty)
            {
                comIDnum.SelectedIndex = -1;
                comNimi.SelectedIndex = -1;

                

                Com_dropdownclosed_käyttöliittymä_toimenpiteet();

                var parts = comSelaaKaikkiaLaskuja.Text.Split(" ");
                var asiakas_id_part = parts[1].Split(":");
                var asiakas_id = asiakas_id_part[1];

                var laskuidpart = parts[0].Split(':');
                var laskuid = laskuidpart[1];

                Lasku = Repo.GetLasku(laskuid);

                Asiakas = Repo.GetAsiakasIDnumerolla(asiakas_id);

                TäytäLabeleihinAsiakkaanTiedot();

               

            }
        }
       
        /// <summary>
        /// Aktivoituu kun painetaan "luo uusi lasku" - nappia, joka sijaitsee Lasku-boxissa.
        /// Avaa uuden käyttöliittymän jossa voidaan luoda uusi lasku
        /// </summary>
        private void Luo_Lasku_click(object sender, RoutedEventArgs e)
        {
            LuoLasku luoLasku = new LuoLasku();

            var returnvalue = luoLasku.ShowDialog();

            if (returnvalue == true)
            {
                Laskut = Repo.GetLaskut();

                Alusta_ComBoxes_In_Asiakastiedot();

                Alusta_GroupAsiakas_Käyttöliittymä();

                AlustacomSelaaKaikkiaLaskuja_combobox();

            }
        }

        /// <summary>
        /// Aktivoituu kun painetaan "luo uusi lasku" - nappia, joka sijaitsee Lasku-boxissa.
        /// Metodi vie comboxiin valitun laskun tiedot uuteen käyttöliittymään, jossa valitun laskun tietoja voi tarkastella tai päivittää. 
        /// </summary>
        private void MuokkaaLaskua_Click(object sender, RoutedEventArgs e)
        {
            if (comSelaaKaikkiaLaskuja.SelectedIndex != -1)
            {
                LuoLasku muokkaaLasku = new LuoLasku(Lasku);
                var result = muokkaaLasku.ShowDialog();
                if (result == true)
                {
                    Laskut = Repo.GetLaskut();

                    Alusta_ComBoxes_In_Asiakastiedot();

                    Alusta_GroupAsiakas_Käyttöliittymä();

                    AlustacomSelaaKaikkiaLaskuja_combobox();

                }


            }
            else
            {
                MessageBox.Show("Valitse lasku jota haluat muokata", "virhe");
            }


        }




        private void KeyDown_txtNimi(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter || e.Key == Key.Down)
            {
                txtOsoite.Focus();


            }


        }

        private void KeyDown_TextOsoite(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                txtNimi.Focus();


            }
            else if (e.Key == Key.Enter)
            {
                Tallenna_Asiakas_click(sender, e);
            }


        }

        
    }



}



          