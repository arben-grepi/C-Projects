using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Harjoitustyö
{
    /// <summary>
    /// Interaction logic for LuoLasku.xaml
    /// </summary>
    public partial class LuoLasku : Window
    {
        public Asiakas Asiakas;

        public ObservableCollection<Asiakas> Asiakkaat;

        public Lasku Lasku { get; set; }

        public  Laskuttaja Laskuttaja { get; set; }

        public Repository repo { get; set; }

           
        /// <summary>
        /// Tuodaan lasku parametrilla jos Asiakastiedot käyttöliittymässä halutaan tarkastella tai muokata tiettyä laskua.  
        /// </summary>
        /// <param name="lasku">Asiakastiedot käyttöjärjestelmästä valittu lasku</param>
        public LuoLasku(Lasku lasku) : this()

        {
            //Asiakastietoja ei voi enään päivittää, joten piilotetaan siihen liittyvät toiminnot
            lblValitseAsiakas.Visibility = Visibility.Hidden;
            comSelaaAsiakkaita.Visibility = Visibility.Hidden;
            lblValitseAsiakasEnnenDatagrid.Visibility = Visibility.Hidden;
            btnPoistaLasku.Visibility = Visibility.Visible;

            //Lasketaan laskurivien summa + (työtunnit * työnhinta)
            var tuotteidenSumma = Lasku.TuotteidenSumma(lasku.Laskurivit);
            lasku.Summa = tuotteidenSumma + (lasku.Työn_hinta * lasku.Työtunnit);
         
            this.DataContext = lasku;
            Lasku = lasku;

            //täytetään asiakastiedot labeleihin.
            Asiakas = repo.GetAsiakasIDnumerolla(Lasku.Asiakas_ID.ToString());
            lblAsiakkaanNimi.Content = Asiakas.Nimi;
            lblAsiakkaanOsoite.Content = Asiakas.Osoite;






        }
        public LuoLasku()
        {

            InitializeComponent();
            repo = new Repository();
            Asiakas = new Asiakas();
            Asiakkaat = new ObservableCollection<Asiakas>();
            Lasku = new Lasku();
            Laskuttaja = new Laskuttaja();

            HaeLaskuttaja();

            Asiakkaat = repo.GetAsiakkaat();

            TäytäcomSelaaAsiakkaita();       

            lblLaskun_tehty.Content = DateOnly.FromDateTime(DateTime.Now);
            Eräpäivä.SelectedDate = DateTime.Now;

            comTuotteet.ItemsSource = repo.GetTuotteet();

            btnPoistaLasku.Visibility = Visibility.Collapsed;
      
        }

        /// <summary>
        /// Haetaan laskuttajan tiedot tietokannasta
        /// </summary>pw
        private void HaeLaskuttaja()
        {
            Laskuttaja = repo.GetLaskuttaja();
            lblLaskuttaja.Content = Laskuttaja.Nimi;
            Lasku.Osoite = Laskuttaja.Osoite;

        }

        /// <summary>
        /// Tallennetaan lasku MariaDB tietokantaan ja suljetaan käyttöliittymä
        /// </summary>
        private void btnTallennalasku_click(object sender, RoutedEventArgs e)
        {

            Lasku = (Lasku)this.DataContext;

            //lähdetään oletuksesta että tuotteiden "Määrä" kohdassa ei lue nolla. 
            bool Tuotteiden_määrä_ei_ole_nolla = true;


            foreach (var rivi in Lasku.Laskurivit)
            {
                //jos jonkun tuotteen kohdalla lukee luku 0, niin sanotaan että boolean arvo on false
                if (rivi.Määrä == 0)
                {
                    Tuotteiden_määrä_ei_ole_nolla = false;

                }

            }
            //tänne pästään vain jos jokaisen tuotteen kohdalla lukee jokin muu luku kuin 0
            if (Tuotteiden_määrä_ei_ole_nolla)
            {
                if (lblAsiakkaanNimi.Content == string.Empty)
                {
                    MessageBox.Show("Valitse asiakas, jolle lasku luodaan", "Virhe");
                }
                else if (Lasku.Laskurivit.Count == 0)
                {
                    MessageBox.Show("Et ole lisännyt laskulle yhtään tuotetta", "Virhe");

                }
                else
                {


                    repo.TallennaLasku(Lasku);
                    var asiakasid = Lasku.Asiakas_ID.ToString();
                    var laskut = repo.GetLaskut();
                    Lasku.LaskuID = laskut[laskut.Count - 1].LaskuID;
                    repo.TallennaLaskunRivit(Lasku);


                    MessageBox.Show("Lasku Tallennettu", "Tiedoksi");
                    DialogResult = true;
                }


            }
            else
            {
                MessageBox.Show("Tuotteiden määrä ei voi olla", "virhe");
            }




        }

        /// <summary>
        /// Poistetaan Lasku ja palataan takaisn Asiakastiedot käyttöliittymään.
        /// </summary>
        private void PoistaLasku_click(object sender, RoutedEventArgs e)
        {
           

            var result = MessageBox.Show("Haluatko varmasti POISTAA laskun ", "Kysymys", MessageBoxButton.YesNo,
             MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Lasku = (Lasku)this.DataContext;              
                repo.PoistaKokoLasku(Lasku);
                
                MessageBox.Show("Lasku Poistettu", "Tiedoksi");
                DialogResult = true;
            }
           

        }

        /// <summary>
        /// Täytetäään combobox Asiakastiedoilla
        /// </summary>
        private void TäytäcomSelaaAsiakkaita()
        {

            for (int i = 0; i < Asiakkaat.Count; i++)
            {
                comSelaaAsiakkaita.Items.Add($"AsiakasID:{Asiakkaat[i].Asiakas_ID} Nimi:{Asiakkaat[i].Nimi}");


            }

        }
        
        /// <summary>
        /// Täytetään labeleihin Asiakastiedot kun asiakas on valittu comboboxista
        /// </summary>
        private void Selaa_Asiakkata_closed(object sender, EventArgs e)
        {
            
            if (comSelaaAsiakkaita.SelectedIndex != -1)
            {
                lblValitseAsiakasEnnenDatagrid.Visibility = Visibility.Hidden;


                var parts = comSelaaAsiakkaita.Text.Split(' ');
                var idpart = parts[0].Split(":");
                lblID.Content = idpart[1];
                var asiakas_id = idpart[1];

                Asiakas = repo.GetAsiakasIDnumerolla(asiakas_id);

                Lasku.Asiakas_ID = Asiakas.Asiakas_ID;
                lblAsiakkaanNimi.Content = Asiakas.Nimi;
                lblAsiakkaanOsoite.Content = Asiakas.Osoite;

                this.DataContext = Lasku;
                Eräpäivä.SelectedDate = DateTime.Now;
            }
            

        }

        

    }
}
