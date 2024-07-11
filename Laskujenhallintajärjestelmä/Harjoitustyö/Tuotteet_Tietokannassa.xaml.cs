using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for Tuotteet_Tietokannassa.xaml
    /// </summary>
    public partial class Tuotteet_Tietokannassa : Window
    {
        public Repository Repository;

        public Tuotteet Tuotteet;

        public int ID;
        
        public Tuotteet_Tietokannassa()
        {
            InitializeComponent();
            ID = 0;
            Repository = new Repository();
            Tuotteet = new Tuotteet();



            AlustaKäyttöliittymä();

        }

        /// <summary>
        /// Alustetaan komponenttien visibilitynäkymät sellaiseen muotoon kun niiden kuuluu olla kun ohjelma avataan. 
        /// Haetaan KaikkiTuotteet-comboxiin tuotetiedot MariaDBstä. Combobox näkyy vasta silloin kun halutaan muokata jotain tiettyä tuotetta
        /// </summary>
        private void AlustaKäyttöliittymä()
        {
            btnLisää_tuote.Visibility = Visibility.Visible;
            btnMuokkaa_Tuote.Visibility = Visibility.Visible;
            btnTallennaMuokattuTuote.Visibility = Visibility.Hidden;
            btnPeruuta.Visibility = Visibility.Hidden;
            btnTallenna_Tuote.Visibility = Visibility.Hidden;

            lblTuotteet.Visibility = Visibility.Hidden; comSelaaTuotteita.Visibility = Visibility.Hidden;
            lblTuote_ID.Visibility = Visibility.Hidden; lblAsennetaan_autom.Visibility = Visibility.Hidden;

            lblNimi.Visibility = Visibility.Hidden; txtNimi.Visibility = Visibility.Hidden;
            lblHinta.Visibility = Visibility.Hidden; txtHinta.Visibility = Visibility.Hidden;

            Tuotteet.Tuotes = Repository.GetTuotteet();

            for (int i = 0; i < Tuotteet.Tuotes.Count; i++)
            {
                comSelaaTuotteita.Items.Add($"ID:{Tuotteet.Tuotes[i].Tuote_ID} Nimi:{Tuotteet.Tuotes[i].Nimi} Hinta:{Tuotteet.Tuotes[i].Hinta}");
            }
            DataContext = Tuotteet;
            
        }
       
        private void Lisää_tuote_click(object sender, RoutedEventArgs e)
        {


            Lisää_tuote_toimenpiteet();

        }

        public void Lisää_tuote_toimenpiteet()
        {
            btnLisää_tuote.Visibility = Visibility.Hidden;
            btnMuokkaa_Tuote.Visibility = Visibility.Hidden;

            btnTallennaMuokattuTuote.Visibility = Visibility.Hidden;
            btnPeruuta.Visibility = Visibility.Visible;
            btnTallenna_Tuote.Visibility = Visibility.Visible;        

            lblTuotteet.Visibility = Visibility.Hidden; comSelaaTuotteita.Visibility = Visibility.Hidden;
            lblTuote_ID.Visibility = Visibility.Visible; lblAsennetaan_autom.Visibility = Visibility.Visible;

            lblNimi.Visibility = Visibility.Visible; txtNimi.Visibility = Visibility.Visible;
            lblHinta.Visibility = Visibility.Visible; txtHinta.Visibility = Visibility.Visible;

        }

        private void Tallenna_Tuote_click(object sender, RoutedEventArgs e)
        {
            var tuote = new Tuote();
            tuote.Nimi = txtNimi.Text;
            bool ok = float.TryParse(txtHinta.Text, out float hinta);
            if (ok)
            {
                tuote.Hinta = float.Parse(txtHinta.Text);

                var result = MessageBox.Show("Lisätäänkö uusi tuote tietokantaan?", "Kysymys", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {


                    Repository.LisääTuote(tuote);

                    DialogResult = true;


                }

            }
            else
            {
                MessageBox.Show("VIRHE", "Virhe");

            }

            
        }

        private void TallennaMuokattuTuote_click(object sender, RoutedEventArgs e)
        {

            if (comSelaaTuotteita.SelectedIndex != -1)
            {
                var tuote = new Tuote();
                if (txtNimi.Text != string.Empty)
                {
                    tuote.Nimi = txtNimi.Text;
                    bool ok = float.TryParse(txtHinta.Text, out float hinta);

                    if (ok)
                    {
                        tuote.Hinta = float.Parse(txtHinta.Text);
                        var parts = comSelaaTuotteita.Text.Split(' ');
                        var idparts = parts[0].Split(":");
                        var tuoteid = int.Parse(idparts[1]);
                        tuote.Tuote_ID = tuoteid;

                        var result = MessageBox.Show("Lisätäänkö muokattu tuote tietokantaan?", "Kysymys", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {


                            Repository.MuokkaaTuote(tuote);

                            DialogResult = true;


                        }

                    }
                    else
                    {
                        MessageBox.Show("Virheellinen hinta", "Virhe");

                    }



                }
                else
                {
                    MessageBox.Show("Nimi ei voi olla tyhjä", "Virhe");
                    AlustaKäyttöliittymä();

                }
               


            }
            else 
            {
                MessageBox.Show("Valitse ensin tuote, jota \nhaluat muokata alasvetopalkista", "Virhe");

                AlustaKäyttöliittymä();

            }
            

        }

        private void btnPeruuta_click(object sender, RoutedEventArgs e)
        {

            Peruuta_toimenpiteet();

        }

        private void Peruuta_toimenpiteet()
        {
            AlustaKäyttöliittymä();
           
        }

      
        private void Muokkaa_Tuote_click(object sender, RoutedEventArgs e)
        {

            Muokkaa_Tuote_Toimenpiteet();        

        }
 
        private void Muokkaa_Tuote_Toimenpiteet()
        {
            btnLisää_tuote.Visibility = Visibility.Hidden;
            btnMuokkaa_Tuote.Visibility = Visibility.Hidden;

            btnTallennaMuokattuTuote.Visibility = Visibility.Visible;
            btnPeruuta.Visibility = Visibility.Visible;
            btnTallenna_Tuote.Visibility = Visibility.Hidden;

            lblTuotteet.Visibility = Visibility.Visible; comSelaaTuotteita.Visibility = Visibility.Visible;
            lblTuote_ID.Visibility = Visibility.Hidden; lblAsennetaan_autom.Visibility = Visibility.Hidden;

            lblNimi.Visibility = Visibility.Visible; txtNimi.Visibility = Visibility.Visible;
            lblHinta.Visibility = Visibility.Visible; txtHinta.Visibility = Visibility.Visible;

        }
   
        private void dropDownClosed(object sender, EventArgs e)
        {
            if (comSelaaTuotteita.SelectedIndex > -1)
            {
                var parts = comSelaaTuotteita.Text.Split(' ');
                var idparts = parts[0].Split(":");
                ID = int.Parse(idparts[1]);

                var nimiparts = parts[1].Split(":");
                var nimi = nimiparts[1];

                var hintaparts = parts[2].Split(":");
                var hinta = hintaparts[1];

                txtNimi.Text = nimi;
                txtHinta.Text = hinta;
            }


        }



        /// KeyDown metodit Mahdollistaa helpon liikkummisen tekstilaatikosta toiseen entteriä painamalla. 
        private void txtNimiKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter || e.Key == Key.Down)
            {
                txtHinta.Focus();

            }

        }
        private void txtHintaKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                txtNimi.Focus();


            }
            else if (e.Key == Key.Enter && comSelaaTuotteita.Visibility == Visibility.Hidden)
            {
                Tallenna_Tuote_click(sender, e);
            }
            else if (e.Key == Key.Enter && comSelaaTuotteita.Visibility == Visibility.Visible)
            {
                TallennaMuokattuTuote_click(sender, e);
            }


        }

    }


}
