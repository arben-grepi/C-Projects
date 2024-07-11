using Microsoft.VisualBasic;
using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Harjoitustyö
{
    public class Lasku
    {
        public bool Maksettu { get; set; }
        public float Summa { get; set; }
        public float Työtunnit { get; set; }
        public float Työn_hinta { get; set; }
        public int LaskuID { get; set; }
        public string? Osoite { get; set; }
        public DateTime Pvm { get; set; }
        public DateTime Eräpäivä { get; set; }
        public string? Lisätiedot { get; set; }
        public int Asiakas_ID { get; set; }

        public ObservableCollection<Laskurivi> Laskurivit { get; set; }
        
        public float TuotteidenSumma(ObservableCollection<Laskurivi> laskurivit)
        {
            
            

            float laskurivitYhteensä = 0;
            foreach (var rivi in laskurivit)
            {
                laskurivitYhteensä += rivi.Määrä * rivi.Hinta;

            }
            return laskurivitYhteensä;

        }
        public Lasku()
        {
            Maksettu = false;
            Summa = 0;
            Työtunnit = 0;
            Työn_hinta = 0;
            LaskuID = 0;
            Osoite = string.Empty;
            Lisätiedot = string.Empty;
            Asiakas_ID = 0;
            Pvm = new DateTime();
            Eräpäivä = new DateTime();
            

            Laskurivit = new ObservableCollection<Laskurivi>();
        }

       
    }

    public class Laskurivi
    {
        public Laskurivi() 
        {
            ID = 0;
            Rivi = 0;
            Määrä = 0;
            Hinta = 0;
            LaskuID = 0;
            TuoteID = 0;
            Tuote_nimi = string.Empty;


        }
        public int ID { get; set; }
        public int Rivi { get; set; }
        public float Määrä { get; set; }
        public float Hinta { get; set; }   
        public int LaskuID { get; set; }
        public int TuoteID { get; set; }
        public string Tuote_nimi { get; set; }

       

    }
  

}

